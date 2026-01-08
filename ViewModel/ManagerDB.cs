using ModelDates;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ManagerDB:UserDB
    {
        public override BaseEntity NewEntity()
        {
            return new Manager();
        }

        public ManagerList SelectAll()
        {
            command.CommandText = $"SELECT  Manager.ID, Manager.Pass, [User].Username, " +
                $" [User].Email, [User].[Password], [User].Gender, [User].DateOfBirth, " +
                $" [User].City, [User].Bio, [User].ProfilePic, [User].CreatedAt, " +
                $" [User].Age FROM  (Manager INNER JOIN  [User] ON Manager.ID = [User].ID)";

            ManagerList managerList = new ManagerList(base.Select());
            return managerList;
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Manager man = entity as Manager;
            man.MangPassword =reader["Pass"].ToString();
            base.CreateModel(entity);
            return man;

        }
        // This method handles the logic of WHICH tables to update
        public override void Update(BaseEntity entity)
        {
            Manager man = entity as Manager;
            if (man != null)
            {
                // 1. Queue the update for the Manager table (this class)
                updated.Add(new ChangeEntity(this.CreateUpdatedSQL, entity));

                // 2. Queue the update for the User table (the base class)
                updated.Add(new ChangeEntity(base.CreateUpdatedSQL, entity));
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

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand command)
        {
            Manager man = entity as Manager;
            if (man != null)
            {
                // The SQL command for your Manager-specific fields
                string sqlStr = "INSERT INTO Manager (Pass, ID) VALUES (@pass, @pid)"; 

                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@pass", man.MangPassword));
                command.Parameters.Add(new OleDbParameter("@pid", man.Id));
            }
        }

        // This method builds the actual SQL string for the Manager table
        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand command)
        {
            Manager man = entity as Manager;
            if (man != null)
            {
                // The SQL command for your Manager-specific fields
                string sqlStr = "UPDATE Manager SET Pass = @pass WHERE ID = @pid";

                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@pass", man.MangPassword));
                command.Parameters.Add(new OleDbParameter("@pid", man.Id));
            }
        }
    }
}
