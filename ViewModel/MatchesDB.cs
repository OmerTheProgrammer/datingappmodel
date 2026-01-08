using ModelDates;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class MatchesDB:BaseDB
    {
       

        public MatchesList SelectAll()
        {
            command.CommandText = $"SELECT * FROM Matches";

            MatchesList matchesList = new MatchesList(base.Select());
            return matchesList;
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Matches m = entity as Matches;
            m.User1 = UserDB.SelectById((int)reader["User1ID"]);
            m.User2 = UserDB.SelectById((int)reader["User2ID"]);

            base.CreateModel(entity);
            return m;

        }
        public override BaseEntity NewEntity()
        {
            return new Matches();
        }

        static private MatchesList list = new MatchesList();
        public static Matches SelectById(int id)
        {
            MatchesDB db = new MatchesDB();
            list = db.SelectAll();

            Matches m = list.Find(item => item.Id == id);
            return m;
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Matches c = entity as Matches;
            if (c != null)
            {
                string sqlStr = $"DELETE FROM Matches where id=@pid";
                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@pid", c.Id));
            }
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Matches p = entity as Matches;
            if (p != null)
            {
                // Removed ID from the list because Access handles AutoNumbers automatically
                string sqlStr = "INSERT INTO Matches (User1ID, User2ID ) " +
                                " VALUES (@User1ID, @User2ID)";

                cmd.CommandText = sqlStr;
                cmd.Parameters.Clear();

                // Parameters must be in the exact order they appear in the SQL string above
                cmd.Parameters.Add(new OleDbParameter("@User1ID", p.User1.Id));
                cmd.Parameters.Add(new OleDbParameter("@User2ID", p.User2.Id));
            }
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Matches c = entity as Matches;
            if (c != null)
            {
                // Remove the comma after the second '?' and add a space before 'WHERE'
                string sqlStr = "UPDATE Matches SET User1ID=?, User2ID=? " +
                                "WHERE ID=?";

                cmd.CommandText = sqlStr;
                cmd.Parameters.Clear();

                // 1. Text fields
                cmd.Parameters.Add("@cUser1ID", OleDbType.Integer).Value = c.User1.Id;

                // 2. Numeric fields (Ensure these are integers in Access)
                cmd.Parameters.Add("@cUser2ID", OleDbType.Integer).Value = c.User2.Id;

                // 9. WHERE ID (Integer)
                cmd.Parameters.Add("@id", OleDbType.Integer).Value = c.Id;
            }
        }
    }
}
