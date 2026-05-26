using ModelDates;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace ViewModel
{
    public class MatchesDB : BaseDB
    {
        public override BaseEntity NewEntity() => new Matches();

        public MatchesList SelectAll()
        {
            // Pass the SQL string directly to the safe, local-command base.Select()
            return new MatchesList(base.Select("SELECT * FROM Matches"));
        }

        protected override BaseEntity CreateModel(BaseEntity entity, OleDbDataReader reader)
        {
            Matches m = entity as Matches;
            if (m != null)
            {
                if (reader["User1ID"] != DBNull.Value)
                    m.User1 = UserDB.SelectById(Convert.ToInt32(reader["User1ID"]));

                if (reader["User2ID"] != DBNull.Value)
                    m.User2 = UserDB.SelectById(Convert.ToInt32(reader["User2ID"]));

                if (reader["ID"] != DBNull.Value)
                    m.Id = Convert.ToInt32(reader["ID"]);
            }
            return m;
        }

        public static Matches SelectById(int id)
        {
            using (MatchesDB db = new MatchesDB())
            {
                return db.SelectAll().Find(item => item.Id == id);
            }
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Matches m = entity as Matches;
            if (m == null) return;

            cmd.CommandText = "DELETE FROM Matches WHERE ID = ?";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("?", m.Id);
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Matches m = entity as Matches;
            if (m == null) return;

            cmd.CommandText = "INSERT INTO Matches (User1ID, User2ID) VALUES (?, ?)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("?", m.User1.Id);
            cmd.Parameters.AddWithValue("?", m.User2.Id);
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Matches m = entity as Matches;
            if (m == null) return;

            cmd.CommandText = "UPDATE Matches SET User1ID = ?, User2ID = ? WHERE ID = ?";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("?", m.User1.Id);
            cmd.Parameters.AddWithValue("?", m.User2.Id);
            cmd.Parameters.AddWithValue("?", m.Id);
        }
        public List<Matches> GetMatchesForUser(int userId)
        {
            // Fetches all matches where the user is either User1 or User2
            string sql = "SELECT * FROM Matches WHERE User1ID = ? OR User2ID = ?";
            var allMatches = base.Select(sql, userId, userId);
            return new MatchesList(allMatches);
        }
    }
}