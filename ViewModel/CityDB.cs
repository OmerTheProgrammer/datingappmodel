using ModelDates;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class CityDB:BaseDB
    {
       
           public CityList SelectAll()
        {
            command.CommandText = "SELECT * FROM City";
            CityList cityList = new CityList(base.Select());
            return cityList;
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            City c = entity as City;
            if (c != null)
            {
                c.Name = reader["CityName"].ToString();
            }

            base.CreateModel(entity);
            return entity;
        }

        public override BaseEntity NewEntity()
        {
            return new City();
        }
        static private CityList list = new CityList();
        public static City SelectById(int id)
        {
            CityDB db = new CityDB();
            list = db.SelectAll();
            City g = list.Find(item => item.Id == id);
            return g;
        }
        public virtual void Delete(BaseEntity entity)
        {
            BaseEntity reqEntity = this.NewEntity();
            DistanceBetweenCitiesDB dbcDB=new DistanceBetweenCitiesDB();
           
            var dbcList = (List<DistanceBetweenCities>) dbcDB.SelectAll().FindAll(x => x.City1.Id==entity.Id || x.City2.Id==entity.Id);

            foreach (DistanceBetweenCities d in dbcList)
            {
                dbcDB.Delete(d);
            }
            dbcDB.SaveChanges();
            if (entity != null & entity.GetType() == reqEntity.GetType())
            {
                deleted.Add(new ChangeEntity(this.CreateDeletedSQL, entity));
            }
        }

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            City c = entity as City;
            if (c != null)
            {
                string sqlStr = $"DELETE FROM City where id=@pid";
                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@pid", c.Id));
            }
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            City c = entity as City;
            if (c != null)
            {
                string sqlStr = $"Insert INTO City (CityName) VALUES (@cName)";

                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@cName", c.Name));
            }
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            City c = entity as City;
            if (c != null)
            {
                string sqlStr = $"UPDATE City SET CityName=@cName WHERE ID=@id";

                command.CommandText = sqlStr;
                command.Parameters.Add(new OleDbParameter("@cName", c.Name));
                command.Parameters.Add(new OleDbParameter("@id", c.Id));
            }
        }
    }
}
