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
        public virtual void Delete(BaseEntity entity)
        {
            if (entity == null) return;

            PreferencesDB prefDB = new PreferencesDB();
            // וודא שבתוך מחלקת Preferences יש מאפיין שנקרא User (או משהו דומה) שמכיל את ה-ID
            var prefList = prefDB.SelectAll().FindAll(x => x.User.Id == entity.Id);
            foreach (Preferences p in prefList) { prefDB.Delete(p); }
            prefDB.SaveChanges();
            PhotosDB pDB = new PhotosDB();
            var pList = pDB.SelectAll().FindAll(x => x.User.Id == entity.Id);
            foreach (Photos p in pList) { pDB.Delete(p); }
            pDB.SaveChanges();

            // 1. מחיקת כל ההודעות - גם כאלו שהמשתמש שלח וגם כאלו שקיבל (דרך המאטצ'ים שלו)
            MessagesDB meDB = new MessagesDB();
            var meList = meDB.SelectAll().FindAll(x =>
                x.Sender.Id == entity.Id ||
                x.Match.User1.Id == entity.Id ||
                x.Match.User2.Id == entity.Id);

            foreach (Messages m in meList) { meDB.Delete(m); }
            meDB.SaveChanges(); // חובה לשמור כאן כדי לשחרר את הנעילה על Matches

            // 2. עכשיו מוחקים מאטצ'ים
            MatchesDB mDB = new MatchesDB();
            var mList = mDB.SelectAll().FindAll(x => x.User1.Id == entity.Id || x.User2.Id == entity.Id);
            foreach (Matches m in mList) { mDB.Delete(m); }
            mDB.SaveChanges();

            // 3. מחיקת לייקים
            LikesDB lDB = new LikesDB();
            var lList = lDB.SelectAll().FindAll(x => x.Liker.Id == entity.Id || x.LikedUser.Id == entity.Id);
            foreach (Likes l in lList) { lDB.Delete(l); }
            lDB.SaveChanges();

            // 4. ורק בסוף - מוסיפים את המשתמש עצמו למחיקה
            BaseEntity reqEntity = this.NewEntity();
            if (entity.GetType() == reqEntity.GetType())
            {
                deleted.Add(new ChangeEntity(this.CreateDeletedSQL, entity));
            }
        }
        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            User c = entity as User;
            if (c != null)
            {
                string sqlStr = $"DELETE FROM [User] where id=@pid";
                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@pid", c.Id));
            }
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            User p = entity as User;
            if (p != null)
            {
                // Removed ID from the list because Access handles AutoNumbers automatically
                string sqlStr = "INSERT INTO [User] (Username, Email, [Password], Gender, DateOfBirth, City, Bio,ProfilePic, CreatedAt, Age) " +
                                " VALUES (@Username, @Email, @Password, @Gender, @DateOfBirth, @City, @Bio,@pPic, @CreatedAt, @Age)";

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
                cmd.Parameters.Add(new OleDbParameter("@pPic", ""));
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
