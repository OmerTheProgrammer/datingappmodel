using ModelDates;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace ViewModel
{
    public class MessagesDB : BaseDB
    {
        public override BaseEntity NewEntity() => new Messages();

        public MessagesList SelectAll()
        {
            // Pass SQL string directly to trigger local command execution
            return new MessagesList(base.Select("SELECT * FROM Messages"));
        }

        protected override BaseEntity CreateModel(BaseEntity entity, OleDbDataReader reader)
        {
            ModelDates.Messages me = entity as ModelDates.Messages;
            if (me != null)
            {
                if (reader["MatchID"] != DBNull.Value)
                    me.Match = MatchesDB.SelectById(Convert.ToInt32(reader["MatchID"]));

                if (reader["SenderID"] != DBNull.Value)
                    me.Sender = UserDB.SelectById(Convert.ToInt32(reader["SenderID"]));

                if (reader["MessageText"] != DBNull.Value)
                    me.MessageText = reader["MessageText"].ToString();

                if (reader["SentAt"] != DBNull.Value)
                    me.SentAt = Convert.ToDateTime(reader["SentAt"]);

                if (reader["ID"] != DBNull.Value)
                    me.Id = Convert.ToInt32(reader["ID"]);
            }
            return me;
        }

        public static Messages SelectById(int id)
        {
            using (MessagesDB db = new MessagesDB())
            {
                return db.SelectAll().Find(item => item.Id == id);
            }
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Messages m = entity as Messages;
            if (m == null) return;

            cmd.CommandText = "DELETE FROM Messages WHERE ID = ?";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("?", m.Id);
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Messages m = entity as Messages;
            if (m == null) return;

            cmd.CommandText = "INSERT INTO Messages (MatchID, SenderID, MessageText, SentAt) VALUES (?, ?, ?, ?)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("?", m.Match.Id);
            cmd.Parameters.AddWithValue("?", m.Sender.Id);
            cmd.Parameters.AddWithValue("?", m.MessageText);
            cmd.Parameters.AddWithValue("?", m.SentAt);
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Messages m = entity as Messages;
            if (m == null) return;

            cmd.CommandText = "UPDATE Messages SET MatchID = ?, SenderID = ?, MessageText = ?, SentAt = ? WHERE ID = ?";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("?", m.Match.Id);
            cmd.Parameters.AddWithValue("?", m.Sender.Id);
            cmd.Parameters.AddWithValue("?", m.MessageText);
            cmd.Parameters.AddWithValue("?", m.SentAt);
            cmd.Parameters.AddWithValue("?", m.Id);
        }
        public List<Messages> GetMessagesByMatchId(int matchId)
        {
            // Fetches only the messages belonging to a specific conversation
            string sql = "SELECT * FROM Messages WHERE MatchID = ? ORDER BY SentAt ASC";

            // You will need to use the parameterized Select overload we created earlier
            var messages = base.Select(sql, matchId);
            return new MessagesList(messages);
        }
    }
}