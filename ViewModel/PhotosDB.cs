using ModelDates;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class PhotosDB:BaseDB
    {
        

        public PhotosList SelectAll()
        {
            command.CommandText = $"SELECT * FROM Photos";

            PhotosList photosList = new PhotosList(base.Select());
            return photosList;
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Photos ph = entity as Photos;
            ph.User = UserDB.SelectById((int)reader["UserID"]);
            ph.Url =reader["PhotoURL"].ToString();
           
            base.CreateModel(entity);
            return ph;

        }
        public override BaseEntity NewEntity()
        {
            return new Photos();
        }

        static private PhotosList list = new PhotosList();
        public static Photos SelectById(int id)
        {
            PhotosDB db = new PhotosDB();
            list = db.SelectAll();

            Photos ph = list.Find(item => item.Id == id);
            return ph;
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Photos c = entity as Photos;
            if (c != null)
            {
                string sqlStr = $"DELETE FROM Photos where id=@pid";
                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@pid", c.Id));
            }
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Photos p = entity as Photos;
            if (p != null)
            {
                // Removed ID from the list because Access handles AutoNumbers automatically
                string sqlStr = "INSERT INTO Photos (UserID, PhotoURL) " +
                                " VALUES (@UserID, @PhotoURL)";

                cmd.CommandText = sqlStr;
                cmd.Parameters.Clear();

                // Parameters must be in the exact order they appear in the SQL string above
                cmd.Parameters.Add(new OleDbParameter("@UserID", p.User.Id));
                cmd.Parameters.Add(new OleDbParameter("@PhotoURL", p.Url));
             
            }
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Photos c = entity as Photos;
            if (c != null)
            {
                // Remove the comma after the second '?' and add a space before 'WHERE'
                string sqlStr = "UPDATE Photos SET UserID=?, PhotoURL=?" +
                                "WHERE ID=?";

                cmd.CommandText = sqlStr;
                cmd.Parameters.Clear();

                // 1. Text fields
                cmd.Parameters.Add("@cUserID", OleDbType.Integer).Value = c.User.Id;

                // 2. Numeric fields (Ensure these are integers in Access)
                cmd.Parameters.Add("@cPhotoID", OleDbType.VarWChar).Value = c.Url;

                // 9. WHERE ID (Integer)
                cmd.Parameters.Add("@id", OleDbType.Integer).Value = c.Id;
            }
        }
    }
}
