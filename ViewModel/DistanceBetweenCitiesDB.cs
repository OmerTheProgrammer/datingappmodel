using ModelDates;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class DistanceBetweenCitiesDB:BaseDB
    {
        

        public DistanceBetweenCitiesList SelectAll()
        {
            command.CommandText = $"SELECT * FROM DistanceBetweenCities";

            DistanceBetweenCitiesList distancebetweencitiesList = new DistanceBetweenCitiesList(base.Select());
            return distancebetweencitiesList;
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            DistanceBetweenCities d = entity as DistanceBetweenCities;
            d.City1 = CityDB.SelectById((int)reader["City1"]);
            d.City2= CityDB.SelectById((int)reader["City2"]);
            d.DistanceKm = double.Parse(reader["KM"].ToString());
            base.CreateModel(entity);
            return d;

        }
        public override BaseEntity NewEntity()
        {
            return new DistanceBetweenCities();
        }

        static private DistanceBetweenCitiesList list = new DistanceBetweenCitiesList();
        public static DistanceBetweenCities SelectById(int id)
        {
            DistanceBetweenCitiesDB db = new DistanceBetweenCitiesDB();
            list = db.SelectAll();

            DistanceBetweenCities d = list.Find(item => item.Id == id);
            return d;
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            throw new NotImplementedException();
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            DistanceBetweenCities p = entity as DistanceBetweenCities;
            if (p != null)
            {
                // Removed ID from the list because Access handles AutoNumbers automatically
                string sqlStr = "INSERT INTO DistanceBetweenCities (City1, City2, KM ) " +
                                " VALUES (@City1, @City2, @KM)";

                cmd.CommandText = sqlStr;
                cmd.Parameters.Clear();

                // Parameters must be in the exact order they appear in the SQL string above
                cmd.Parameters.Add(new OleDbParameter("@City1", p.City1.Id));
                cmd.Parameters.Add(new OleDbParameter("@City2", p.City2.Id));
                cmd.Parameters.Add(new OleDbParameter("@KM", p.DistanceKm));
       
            }
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            DistanceBetweenCities c = entity as DistanceBetweenCities;
            if (c != null)
            {
                // Removed the comma after KM=? and added a space before WHERE
                string sqlStr = "UPDATE DistanceBetweenCities SET City1=?, City2=?, KM=? " +
                                "WHERE ID=?";

                cmd.CommandText = sqlStr;
                cmd.Parameters.Clear();

                // 1. Text fields
                cmd.Parameters.Add("@cCity1", OleDbType.Integer).Value = c.City1.Id;

                // 2. Numeric fields (Ensure these are integers in Access)
                cmd.Parameters.Add("@cCity2", OleDbType.Integer).Value = c.City2.Id;

                // 3. Text fields
                cmd.Parameters.Add("@cKM", OleDbType.Integer).Value = c.DistanceKm;
        

                // 9. WHERE ID (Integer)
                cmd.Parameters.Add("@id", OleDbType.Integer).Value = c.Id;
            }
        }
    }
    
}
