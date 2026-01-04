using ModelDates;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class LikesDB:BaseDB
    {
      

        public LikesList SelectAll()
        {
            command.CommandText = $"SELECT * FROM Likes";

           LikesList likesList = new LikesList(base.Select());
            return likesList;
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Likes l = entity as Likes;
            l.Liker = UserDB.SelectById((int)reader["LikerID"]);
            l.LikedUser = UserDB.SelectById((int)reader["LikedID"]);
            base.CreateModel(entity);
            return l;

        }
        public override BaseEntity NewEntity()
        {
            return new Likes();
        }

        static private LikesList list = new LikesList();
        public static Likes SelectById(int id)
        {
            LikesDB db = new LikesDB();
            list = db.SelectAll();

            Likes l = list.Find(item => item.Id == id);
            return l;
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            throw new NotImplementedException();
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Likes p = entity as Likes;
            if (p != null)
            {
                // Removed ID from the list because Access handles AutoNumbers automatically
                string sqlStr = "INSERT INTO Likes (LikerID, LikedID ) " +
                                " VALUES (@LikerID, @LikedID)";

                cmd.CommandText = sqlStr;
                cmd.Parameters.Clear();

                // Parameters must be in the exact order they appear in the SQL string above
                cmd.Parameters.Add(new OleDbParameter("@LikerID", p.Liker.Id));
                cmd.Parameters.Add(new OleDbParameter("@LikedID", p.LikedUser.Id));
               

            }
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Likes c = entity as Likes;
            if (c != null)
            {
                // Remove the comma after the second '?' and add a space before 'WHERE'
                string sqlStr = "UPDATE Likes SET LikerID=?, LikedID=? " +
                                "WHERE ID=?";

                cmd.CommandText = sqlStr;
                cmd.Parameters.Clear();

                // 1. Text fields
                cmd.Parameters.Add("@cLikerID", OleDbType.Integer).Value = c.Liker.Id;

                // 2. Numeric fields (Ensure these are integers in Access)
                cmd.Parameters.Add("@cLikedID", OleDbType.Integer).Value = c.LikedUser.Id;

                // 9. WHERE ID (Integer)
                cmd.Parameters.Add("@id", OleDbType.Integer).Value = c.Id;
            }
        }
    }
}
