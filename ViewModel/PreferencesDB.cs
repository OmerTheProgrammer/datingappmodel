using ModelDates;
using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace ViewModel
{
    public class PreferencesDB : BaseDB
    {
        public PreferencesList SelectAll()
        {
            return new PreferencesList(base.Select("SELECT * FROM Preferences"));
        }

        private bool HasColumn(OleDbDataReader r, string name)
        {
            for (int i = 0; i < r.FieldCount; i++)
                if (string.Equals(r.GetName(i), name, StringComparison.OrdinalIgnoreCase))
                    return true;
            return false;
        }

        protected override BaseEntity CreateModel(BaseEntity entity, OleDbDataReader reader)
        {
            Preferences p = entity as Preferences;

            if (HasColumn(reader, "UserID") && reader["UserID"] != DBNull.Value)
                p.User = UserDB.SelectById(Convert.ToInt32(reader["UserID"]));

            if (HasColumn(reader, "PreferredGender") && reader["PreferredGender"] != DBNull.Value)
                p.PreferredGender = GenderDB.SelectById(Convert.ToInt32(reader["PreferredGender"]));

            p.AgeMin = HasColumn(reader, "AgeMin") && reader["AgeMin"] != DBNull.Value ? Convert.ToInt32(reader["AgeMin"]) : 18;
            p.AgeMax = HasColumn(reader, "AgeMax") && reader["AgeMax"] != DBNull.Value ? Convert.ToInt32(reader["AgeMax"]) : 30;
            p.DistanceMax = HasColumn(reader, "DistanceMax") && reader["DistanceMax"] != DBNull.Value ? Convert.ToInt32(reader["DistanceMax"]) : 100;

            string idName = HasColumn(reader, "id") ? "id" : "ID";
            entity.Id = (HasColumn(reader, idName) && reader[idName] != DBNull.Value) ? Convert.ToInt32(reader[idName]) : 0;

            return p;
        }

        public override BaseEntity NewEntity() => new Preferences();

        public static Preferences SelectById(int id)
        {
            using (PreferencesDB db = new PreferencesDB())
            {
                return db.SelectAll().Find(item => item.Id == id);
            }
        }

        public static Preferences SelectByUserId(int userId)
        {
            using (PreferencesDB db = new PreferencesDB())
            {
                return db.SelectAll().Find(item => item.User != null && item.User.Id == userId);
            }
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Preferences p = entity as Preferences;
            if (p == null) return;

            cmd.CommandText = "DELETE FROM Preferences WHERE ID = @pid";
            cmd.Parameters.Add(new OleDbParameter("@pid", p.Id));
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Preferences p = entity as Preferences;
            if (p == null) return;

            cmd.CommandText = "INSERT INTO Preferences (UserID, PreferredGender, AgeMin, AgeMax, DistanceMax) VALUES (?, ?, ?, ?, ?)";
            cmd.Parameters.AddWithValue("?", p.User.Id);
            cmd.Parameters.AddWithValue("?", p.PreferredGender.Id);
            cmd.Parameters.AddWithValue("?", p.AgeMin);
            cmd.Parameters.AddWithValue("?", p.AgeMax);
            cmd.Parameters.AddWithValue("?", p.DistanceMax);
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Preferences p = entity as Preferences;
            if (p == null) return;

            cmd.CommandText = "UPDATE Preferences SET UserID = ?, PreferredGender = ?, AgeMin = ?, AgeMax = ?, DistanceMax = ? WHERE ID = ?";
            cmd.Parameters.AddWithValue("?", p.User.Id);
            cmd.Parameters.AddWithValue("?", p.PreferredGender.Id);
            cmd.Parameters.AddWithValue("?", p.AgeMin);
            cmd.Parameters.AddWithValue("?", p.AgeMax);
            cmd.Parameters.AddWithValue("?", p.DistanceMax);
            cmd.Parameters.AddWithValue("?", p.Id);
        }
    }
}