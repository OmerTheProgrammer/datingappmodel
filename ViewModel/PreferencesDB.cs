using ModelDates;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class PreferencesDB:BaseDB
    {
   
        public PreferencesList SelectAll()
        {
            command.CommandText = $"SELECT * FROM Preferences";

            PreferencesList preferencesList = new PreferencesList(base.Select());
            return preferencesList;
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Preferences p = entity as Preferences;
            p.User = UserDB.SelectById((int) reader["UserID"]);
            p.MinAge =int.Parse( reader["AgeMin"].ToString());
            p.MaxAge =int.Parse( reader["AgeMax"].ToString());
            p.PreferredGender = GenderDB.SelectById((int)reader["PreferredGender"]);
            var xx = reader["DistanceMax"].ToString();
            p.MaxDistanceKm =int.Parse( reader["DistanceMax"].ToString()); 
            base.CreateModel(entity);
            return p;

        }
        public override BaseEntity NewEntity()
        {
            return new Preferences();
        }

        static private PreferencesList list = new PreferencesList();
        public static Preferences SelectById(int id)
        {
            PreferencesDB db = new PreferencesDB();
            list = db.SelectAll();

            Preferences p = list.Find(item => item.Id == id);
            return p;
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Preferences c = entity as Preferences;
            if (c != null)
            {
                string sqlStr = $"DELETE FROM Preferences where id=@pid";
                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@pid", c.Id));
            }
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Preferences p = entity as Preferences;
            if (p != null)
            {
                // Removed ID from the list because Access handles AutoNumbers automatically
                string sqlStr = "INSERT INTO Preferences (UserID,PreferredGender, AgeMin, AgeMax, DistanceMax) " +
                                " VALUES (@UserID, @PreferredGender, @AgeMin, @AgeMax, @DistanceMax)";

                cmd.CommandText = sqlStr;
                cmd.Parameters.Clear();

                // Parameters must be in the exact order they appear in the SQL string above
                cmd.Parameters.Add(new OleDbParameter("@UserID", p.User.Id));
                cmd.Parameters.Add(new OleDbParameter("@PreferredGender", p.PreferredGender.Id));
                cmd.Parameters.Add(new OleDbParameter("@AgeMin", p.MinAge));
                cmd.Parameters.Add(new OleDbParameter("@AgeMax", p.MaxAge)); // Ensure this is an Integer
                cmd.Parameters.Add(new OleDbParameter("@DistanceMax", p.MaxDistanceKm)); // Ensure this is an Integer
           
            }
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Preferences c = entity as Preferences;
            if (c != null)
            {
                // Added spaces at the end of lines to prevent "DistanceMax=?WHERE" merging
                string sqlStr = "UPDATE Preferences SET " +
                                "UserID = ?, " +
                                "PreferredGender = ?, " +
                                "AgeMin = ?, " +
                                "AgeMax = ?, " +
                                "DistanceMax = ? " +
                                "WHERE ID = ?";

                cmd.CommandText = sqlStr;
                cmd.Parameters.Clear();

                // IMPORTANT: In OleDb, parameters MUST be added in the exact order 
                // they appear in the SQL string, regardless of the @ name.

                // 1. UserID = ?
                cmd.Parameters.Add("@cUserID", OleDbType.Integer).Value = c.User.Id;

                // 2. PreferredGender = ?
                cmd.Parameters.Add("@cPreferredGender", OleDbType.Integer).Value = c.PreferredGender.Id;

                // 3. AgeMin = ?
                cmd.Parameters.Add("@cAgeMin", OleDbType.Integer).Value = c.MinAge;

                // 4. AgeMax = ?
                cmd.Parameters.Add("@cAgeMax", OleDbType.Integer).Value = c.MaxAge;

                // 5. DistanceMax = ?
                cmd.Parameters.Add("@cDistanceMax", OleDbType.Integer).Value = c.MaxDistanceKm;

                // 6. WHERE ID = ?
                cmd.Parameters.Add("@id", OleDbType.Integer).Value = c.Id;
            }
        }
    }
}
