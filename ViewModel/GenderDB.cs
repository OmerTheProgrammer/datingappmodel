using ModelDates;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace ViewModel
{
    public class GenderDB : BaseDB
    {
        public override BaseEntity NewEntity() => new Gender();

        public GenderList SelectAll()
        {
            // We pass the SQL directly, allowing BaseDB to handle the local command
            return new GenderList(base.Select("SELECT * FROM Gender"));
        }

        protected override BaseEntity CreateModel(BaseEntity entity, OleDbDataReader reader)
        {
            Gender g = entity as Gender;
            if (g != null)
            {
                if (reader["GenderName"] != DBNull.Value)
                    g.Name = reader["GenderName"].ToString();

                if (reader["ID"] != DBNull.Value)
                    g.Id = Convert.ToInt32(reader["ID"]);
            }
            return g;
        }

        public static Gender SelectById(int id)
        {
            using (GenderDB db = new GenderDB())
            {
                return db.SelectAll().Find(item => item.Id == id);
            }
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Gender g = entity as Gender;
            if (g == null) return;

            cmd.CommandText = "DELETE FROM Gender WHERE ID = ?";
            cmd.Parameters.AddWithValue("?", g.Id);
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Gender g = entity as Gender;
            if (g == null) return;

            cmd.CommandText = "INSERT INTO Gender (GenderName) VALUES (?)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("?", g.Name);
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Gender g = entity as Gender;
            if (g == null) return;

            cmd.CommandText = "UPDATE Gender SET GenderName = ? WHERE ID = ?";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("?", g.Name);
            cmd.Parameters.AddWithValue("?", g.Id);
        }
    }
}