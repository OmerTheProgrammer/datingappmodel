using ModelDates;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class UserDB:BaseDB
    {
       

        public UserList SelectAll()
        {
            command.CommandText = $"SELECT * FROM [User]";

            UserList userList = new UserList(base.Select());
            return userList;
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            User u = entity as User;
            u.Username = reader["Username"].ToString();
            u.Email = reader["Email"].ToString();
            u.Password = reader["Password"].ToString();
            u.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
            u.Bio = reader["Bio"].ToString();
            u.Gender= GenderDB.SelectById((int)reader["Gender"]);
            u.City= CityDB.SelectById((int)reader["City"]);

            u.CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()) ;
            u.Age = int.Parse(reader["Age"].ToString());
            base.CreateModel(entity);
            return u;
            
        }
        public override BaseEntity NewEntity()
        {
            return new User();
        }

        static private UserList list = new UserList();
        public static   User SelectById(int id)
        {
            UserDB db = new UserDB();
            list = db.SelectAll();

            User u = list.Find(item => item.Id == id);
            return u;
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            throw new NotImplementedException();
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            User p = entity as User;
            if (p != null)
            {
                // Removed ID from the list because Access handles AutoNumbers automatically
                string sqlStr = "INSERT INTO [User] (Username, Email, [Password], Gender, DateOfBirth, City, Bio, CreatedAt, Age) " +
                                " VALUES (@Username, @Email, @Password, @Gender, @DateOfBirth, @City, @Bio, @CreatedAt, @Age)";

                cmd.CommandText = sqlStr;
                cmd.Parameters.Clear();

                // Parameters must be in the exact order they appear in the SQL string above
                cmd.Parameters.Add(new OleDbParameter("@Username", p.Username));
                cmd.Parameters.Add(new OleDbParameter("@Email", p.Email));
                cmd.Parameters.Add(new OleDbParameter("@Password", p.Password));
                cmd.Parameters.Add(new OleDbParameter("@Gender", p.Gender.Id)); // Ensure this is an Integer
                OleDbParameter dateParam = new OleDbParameter("@DateOfBirth", OleDbType.DBDate);
                dateParam.Value = p.DateOfBirth;
                cmd.Parameters.Add(dateParam);
                
                
                 cmd.Parameters.Add(new OleDbParameter("@City", p.City.Id)); // Ensure this is an Integer
                cmd.Parameters.Add(new OleDbParameter("@Bio", p.Bio));
                OleDbParameter dateParam1 = new OleDbParameter("@CreatedAt", OleDbType.DBDate);
                dateParam1.Value = p.CreatedAt;
                cmd.Parameters.Add(dateParam1);
                //  cmd.Parameters.Add(new OleDbParameter("@CreatedAt", p.CreatedAt));
                cmd.Parameters.Add(new OleDbParameter("@Age", p.Age));
            }
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            User c = entity as User;
            if (c != null)
            {
                string sqlStr = "UPDATE [User] SET Username=?, City=?, Email=?, [Password]=?, " +
                                "Gender=?, DateOfBirth=?, Bio=?, Age=?, CreatedAt=? " +
                                "WHERE ID=?";

                cmd.CommandText = sqlStr;
                cmd.Parameters.Clear();

                // 1. Text fields
                cmd.Parameters.Add("@cUsername", OleDbType.VarWChar).Value = c.Username;

                // 2. Numeric fields (Ensure these are integers in Access)
                cmd.Parameters.Add("@ccode", OleDbType.Integer).Value = c.City.Id;

                // 3. Text fields
                cmd.Parameters.Add("@cEmail", OleDbType.VarWChar).Value = c.Email;
                cmd.Parameters.Add("@cPassword", OleDbType.VarWChar).Value = c.Password;

                // 4. Numeric/Gender field
                cmd.Parameters.Add("@cGender", OleDbType.Integer).Value = c.Gender.Id;

                // 5. Date fields (CRITICAL: Must be OleDbType.Date)
                cmd.Parameters.Add("@cDateOfBirth", OleDbType.Date).Value = c.DateOfBirth;

                // 6. Memo/Long Text field
                cmd.Parameters.Add("@cBio", OleDbType.VarWChar).Value = (object)c.Bio ?? DBNull.Value;

                // 7. Age (Integer)
                cmd.Parameters.Add("@cAge", OleDbType.Integer).Value = c.Age;

                // 8. CreatedAt (Date)
                cmd.Parameters.Add("@cCreatedAt", OleDbType.Date).Value = c.CreatedAt;

                // 9. WHERE ID (Integer)
                cmd.Parameters.Add("@id", OleDbType.Integer).Value = c.Id;
            }
        }
    }
}
