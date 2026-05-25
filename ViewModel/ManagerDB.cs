using ModelDates;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace ViewModel
{
    public class ManagerDB : UserDB
    {
        public override BaseEntity NewEntity() => new Manager();

        public ManagerList SelectAll()
        {
            string sql = "SELECT Manager.ID, Manager.Pass, [User].Username, " +
                " [User].Email, [User].[Password], [User].Gender, [User].DateOfBirth, " +
                " [User].City, [User].Bio, [User].ProfilePic, [User].CreatedAt, " +
                " [User].Age FROM (Manager INNER JOIN [User] ON Manager.ID = [User].ID)";

            return new ManagerList(base.Select(sql));
        }

        protected override BaseEntity CreateModel(BaseEntity entity, OleDbDataReader reader)
        {
            // Populate base User data
            base.CreateModel(entity, reader);

            Manager man = entity as Manager;
            if (man != null && reader["Pass"] != DBNull.Value)
            {
                man.MangPassword = reader["Pass"].ToString();
            }
            return man;
        }

        // We override Update/Insert to use the local command parameter correctly
        public override void Update(BaseEntity entity)
        {
            Manager man = entity as Manager;
            if (man != null)
            {
                updated.Add(new ChangeEntity(base.CreateUpdatedSQL, entity));
                updated.Add(new ChangeEntity(this.CreateUpdatedSQL, entity));
            }
        }

        public override void Insert(BaseEntity entity)
        {
            Manager man = entity as Manager;
            if (man != null)
            {
                inserted.Add(new ChangeEntity(base.CreateInsertdSQL, entity));
                inserted.Add(new ChangeEntity(this.CreateInsertdSQL, entity));
            }
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Manager man = entity as Manager;
            if (man != null)
            {
                cmd.CommandText = "INSERT INTO Manager (Pass, ID) VALUES (?, ?)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("?", man.MangPassword);
                cmd.Parameters.AddWithValue("?", man.Id);
            }
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Manager man = entity as Manager;
            if (man != null)
            {
                cmd.CommandText = "UPDATE Manager SET Pass = ? WHERE ID = ?";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("?", man.MangPassword);
                cmd.Parameters.AddWithValue("?", man.Id);
            }
        }
    }
}