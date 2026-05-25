using ModelDates;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace ViewModel
{
    public class CityDB : BaseDB
    {
        public override BaseEntity NewEntity() => new City();

        public CityList SelectAll()
        {
            // Use the base Select with a local command
            return new CityList(base.Select("SELECT * FROM City"));
        }

        protected override BaseEntity CreateModel(BaseEntity entity, OleDbDataReader reader)
        {
            City c = entity as City;
            if (c != null)
            {
                if (reader["CityName"] != DBNull.Value)
                    c.Name = reader["CityName"].ToString();

                if (reader["ID"] != DBNull.Value)
                    c.Id = Convert.ToInt32(reader["ID"]);
            }
            return c;
        }

        public static City SelectById(int id)
        {
            using (CityDB db = new CityDB())
            {
                return db.SelectAll().Find(item => item.Id == id);
            }
        }

        public override void Delete(BaseEntity entity)
        {
            // Use 'using' to ensure the helper DB cleans itself up
            using (DistanceBetweenCitiesDB dbcDB = new DistanceBetweenCitiesDB())
            {
                var dbcList = dbcDB.SelectAll().FindAll(x => x.City1.Id == entity.Id || x.City2.Id == entity.Id);
                foreach (DistanceBetweenCities d in dbcList)
                {
                    dbcDB.Delete(d);
                }
                dbcDB.SaveChanges();
            }
            base.Delete(entity);
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            City c = entity as City;
            if (c == null) return;

            cmd.CommandText = "DELETE FROM City WHERE ID = ?";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("?", c.Id);
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            City c = entity as City;
            if (c == null) return;

            cmd.CommandText = "INSERT INTO City (CityName) VALUES (?)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("?", c.Name);
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            City c = entity as City;
            if (c == null) return;

            cmd.CommandText = "UPDATE City SET CityName = ? WHERE ID = ?";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("?", c.Name);
            cmd.Parameters.AddWithValue("?", c.Id);
        }
    }
}