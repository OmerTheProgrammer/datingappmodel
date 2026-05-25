using ModelDates;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace ViewModel
{
    public class PhotosDB : BaseDB
    {
        public override BaseEntity NewEntity() => new Photos();

        public PhotosList SelectAll()
        {
            // Use base.Select with a local command to ensure thread safety
            return new PhotosList(base.Select("SELECT * FROM Photos"));
        }

        protected override BaseEntity CreateModel(BaseEntity entity, OleDbDataReader reader)
        {
            Photos ph = entity as Photos;
            if (ph != null)
            {
                if (reader["UserID"] != DBNull.Value)
                    ph.User = UserDB.SelectById(Convert.ToInt32(reader["UserID"]));

                if (reader["PhotoURL"] != DBNull.Value)
                    ph.Url = reader["PhotoURL"].ToString();

                if (reader["ID"] != DBNull.Value)
                    ph.Id = Convert.ToInt32(reader["ID"]);
            }
            return ph;
        }

        public static Photos SelectById(int id)
        {
            using (PhotosDB db = new PhotosDB())
            {
                return db.SelectAll().Find(item => item.Id == id);
            }
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Photos ph = entity as Photos;
            if (ph == null) return;

            cmd.CommandText = "DELETE FROM Photos WHERE ID = ?";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("?", ph.Id);
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Photos ph = entity as Photos;
            if (ph == null) return;

            cmd.CommandText = "INSERT INTO Photos (UserID, PhotoURL) VALUES (?, ?)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("?", ph.User.Id);
            cmd.Parameters.AddWithValue("?", ph.Url);
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Photos ph = entity as Photos;
            if (ph == null) return;

            cmd.CommandText = "UPDATE Photos SET UserID = ?, PhotoURL = ? WHERE ID = ?";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("?", ph.User.Id);
            cmd.Parameters.AddWithValue("?", ph.Url);
            cmd.Parameters.AddWithValue("?", ph.Id);
        }
    }
}