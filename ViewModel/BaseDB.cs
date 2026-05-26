using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using ModelDates;

namespace ViewModel
{
    public abstract class BaseDB : IDisposable
    {
        protected static string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\datesaccess.accdb;";
        protected static OleDbConnection connection = new OleDbConnection(connectionString);

        public abstract BaseEntity NewEntity();
        protected abstract BaseEntity CreateModel(BaseEntity entity, OleDbDataReader reader);

        // --- The Missing Pieces ---
        public class ChangeEntity
        {
            public BaseEntity Entity { get; set; }
            public delegate void CreateSql(BaseEntity entity, OleDbCommand command);
            public CreateSql CreateSqlAction { get; set; }
            public ChangeEntity(CreateSql createSql, BaseEntity entity) { this.CreateSqlAction = createSql; this.Entity = entity; }
        }

        protected static List<ChangeEntity> inserted = new List<ChangeEntity>();
        protected static List<ChangeEntity> updated = new List<ChangeEntity>();
        protected static List<ChangeEntity> deleted = new List<ChangeEntity>();

        protected abstract void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd);
        protected abstract void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd);
        protected abstract void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd);

        public virtual void Delete(BaseEntity entity) => deleted.Add(new ChangeEntity(CreateDeletedSQL, entity));
        public virtual void Insert(BaseEntity entity) => inserted.Add(new ChangeEntity(CreateInsertdSQL, entity));
        public virtual void Update(BaseEntity entity) => updated.Add(new ChangeEntity(CreateUpdatedSQL, entity));

        public int SaveChanges()
        {
            int records_affected = 0;
            try
            {
                if (connection.State != ConnectionState.Open) connection.Open();
                using (OleDbTransaction trans = connection.BeginTransaction())
                using (OleDbCommand cmd = connection.CreateCommand())
                {
                    cmd.Transaction = trans;
                    System.Diagnostics.Debug.WriteLine("--- SENDING DATABASE INSERT ---");
                    foreach (OleDbParameter param in cmd.Parameters)
                    {
                        System.Diagnostics.Debug.WriteLine($"Parameter Value: {param.Value} | Type: {param.Value?.GetType().Name ?? "NULL"}");
                    }
                    foreach (var item in inserted) { cmd.Parameters.Clear(); item.CreateSqlAction(item.Entity, cmd); records_affected += cmd.ExecuteNonQuery(); }
                    foreach (var item in updated) { cmd.Parameters.Clear(); item.CreateSqlAction(item.Entity, cmd); records_affected += cmd.ExecuteNonQuery(); }
                    foreach (var item in deleted) { cmd.Parameters.Clear(); item.CreateSqlAction(item.Entity, cmd); records_affected += cmd.ExecuteNonQuery(); }
                    trans.Commit();
                }
            }
            finally { inserted.Clear(); updated.Clear(); deleted.Clear(); }
            return records_affected;
        }

        // --- The Select logic ---
        protected List<BaseEntity> Select(string sql)
        {
            List<BaseEntity> list = new List<BaseEntity>();
            if (connection.State != ConnectionState.Open) connection.Open();
            using (OleDbCommand cmd = new OleDbCommand(sql, connection))
            using (OleDbDataReader localReader = cmd.ExecuteReader())
            {
                while (localReader.Read())
                {
                    BaseEntity entity = NewEntity();
                    list.Add(CreateModel(entity, localReader));
                }
            }
            return list;
        }
        // Add this new overload to BaseDB.cs
        public List<BaseEntity> Select(string sql, params object[] parameters)
        {
            List<BaseEntity> list = new List<BaseEntity>();
            using (OleDbCommand cmd = new OleDbCommand(sql, connection))
            {
                // Add the parameters to the command
                foreach (var param in parameters)
                {
                    cmd.Parameters.AddWithValue("?", param);
                }

                if (connection.State != ConnectionState.Open) connection.Open();

                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Assuming NewEntity() and CreateModel() are already in BaseDB
                        BaseEntity entity = NewEntity();
                        list.Add(CreateModel(entity, reader));
                    }
                }
            }
            return list;
        }

        public void Dispose() { /* Standard Dispose */ }
    }
}