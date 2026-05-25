using ModelDates;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace ViewModel
{
    public class LikesDB : BaseDB
    {
        public override BaseEntity NewEntity() => new Likes();

        public LikesList SelectAll()
        {
            // Pass the SQL directly to base.Select(sql) to trigger the local-command flow
            return new LikesList(base.Select("SELECT * FROM Likes"));
        }

        protected override BaseEntity CreateModel(BaseEntity entity, OleDbDataReader reader)
        {
            Likes l = entity as Likes;
            if (l != null)
            {
                if (reader["LikerID"] != DBNull.Value)
                    l.Liker = UserDB.SelectById(Convert.ToInt32(reader["LikerID"]));

                if (reader["LikedID"] != DBNull.Value)
                    l.LikedUser = UserDB.SelectById(Convert.ToInt32(reader["LikedID"]));

                if (reader["ID"] != DBNull.Value)
                    l.Id = Convert.ToInt32(reader["ID"]);
            }
            return l;
        }

        public static Likes SelectById(int id)
        {
            using (LikesDB db = new LikesDB())
            {
                return db.SelectAll().Find(item => item.Id == id);
            }
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Likes l = entity as Likes;
            if (l == null) return;

            cmd.CommandText = "DELETE FROM Likes WHERE ID = ?";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("?", l.Id);
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Likes l = entity as Likes;
            if (l == null) return;

            cmd.CommandText = "INSERT INTO Likes (LikerID, LikedID) VALUES (?, ?)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("?", l.Liker.Id);
            cmd.Parameters.AddWithValue("?", l.LikedUser.Id);
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Likes l = entity as Likes;
            if (l == null) return;

            cmd.CommandText = "UPDATE Likes SET LikerID = ?, LikedID = ? WHERE ID = ?";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("?", l.Liker.Id);
            cmd.Parameters.AddWithValue("?", l.LikedUser.Id);
            cmd.Parameters.AddWithValue("?", l.Id);
        }
    }
}