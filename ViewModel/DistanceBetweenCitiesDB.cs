using ModelDates;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace ViewModel
{
    public class DistanceBetweenCitiesDB : BaseDB
    {
        public override BaseEntity NewEntity() => new DistanceBetweenCities();

        public DistanceBetweenCitiesList SelectAll()
        {
            // Using the base.Select(sql) pattern with a local command
            return new DistanceBetweenCitiesList(base.Select("SELECT * FROM DistanceBetweenCities"));
        }

        protected override BaseEntity CreateModel(BaseEntity entity, OleDbDataReader reader)
        {
            DistanceBetweenCities d = entity as DistanceBetweenCities;
            if (d != null)
            {
                if (reader["City1"] != DBNull.Value)
                    d.City1 = CityDB.SelectById(Convert.ToInt32(reader["City1"]));

                if (reader["City2"] != DBNull.Value)
                    d.City2 = CityDB.SelectById(Convert.ToInt32(reader["City2"]));

                if (reader["KM"] != DBNull.Value)
                    d.DistanceKm = Convert.ToDouble(reader["KM"]);

                if (reader["ID"] != DBNull.Value)
                    d.Id = Convert.ToInt32(reader["ID"]);
            }
            return d;
        }

        public static DistanceBetweenCities SelectById(int id)
        {
            using (DistanceBetweenCitiesDB db = new DistanceBetweenCitiesDB())
            {
                return db.SelectAll().Find(item => item.Id == id);
            }
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            DistanceBetweenCities c = entity as DistanceBetweenCities;
            if (c == null) return;

            cmd.CommandText = "DELETE FROM DistanceBetweenCities WHERE ID = ?";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("?", c.Id);
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            DistanceBetweenCities p = entity as DistanceBetweenCities;
            if (p == null) return;

            cmd.CommandText = "INSERT INTO DistanceBetweenCities (City1, City2, KM) VALUES (?, ?, ?)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("?", p.City1.Id);
            cmd.Parameters.AddWithValue("?", p.City2.Id);
            cmd.Parameters.AddWithValue("?", p.DistanceKm);
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            DistanceBetweenCities c = entity as DistanceBetweenCities;
            if (c == null) return;

            cmd.CommandText = "UPDATE DistanceBetweenCities SET City1 = ?, City2 = ?, KM = ? WHERE ID = ?";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("?", c.City1.Id);
            cmd.Parameters.AddWithValue("?", c.City2.Id);
            cmd.Parameters.AddWithValue("?", c.DistanceKm);
            cmd.Parameters.AddWithValue("?", c.Id);
        }
    }
}