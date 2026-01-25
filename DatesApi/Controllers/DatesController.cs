using Microsoft.AspNetCore.Mvc;
using ModelDates;
using System.Reflection.Metadata.Ecma335;
using ViewModel;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiSelect.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DatesController : ControllerBase
    {
        [HttpGet]
        [ActionName("CitySelector")]
        public CityList SelectAllCities()
        {
            CityDB dB = new CityDB();
            CityList cities = dB.SelectAll();
            return cities;
        }
        [HttpGet]
        [ActionName("DistanceBetweenCitiesSelector")]
        public DistanceBetweenCitiesList SelectAllDistanceCities()
        {
            DistanceBetweenCitiesDB dB = new DistanceBetweenCitiesDB();
            DistanceBetweenCitiesList distance = dB.SelectAll();
            return distance;
        }
        [HttpGet]
        [ActionName("GenderSelector")]

        public GenderList SelectAllGender()
        {
            GenderDB dB = new GenderDB();
            GenderList genders = dB.SelectAll(); 
            return genders;
        }
        [HttpGet]
        [ActionName("LikesSelector")]
        public LikesList SelectAllLikes()
        {
            LikesDB dB = new LikesDB();
            LikesList likes = dB.SelectAll();
            return likes;
        }
        [HttpGet]
        [ActionName("ManagerSelector")]
        public ManagerList SelectAllManager()
        {
            ManagerDB dB = new ManagerDB();
            ManagerList managers = dB.SelectAll();
            return managers;
        }
        [HttpGet]
        [ActionName("MatchesSelector")]
        public MatchesList SelectAllMatches()
        {
            MatchesDB dB = new MatchesDB();
            MatchesList matches = dB.SelectAll();
            return matches;
        }
        [HttpGet]
        [ActionName("MessagesSelector")]
        public MessagesList SelectAllMessages()
        {
            MessagesDB dB = new MessagesDB();
            MessagesList messages = dB.SelectAll();
            return messages;
        }
        [HttpGet]
        [ActionName("PhotosSelector")]
        public PhotosList SelectAllPhotos()
        {
            PhotosDB dB = new PhotosDB();
            PhotosList photos = dB.SelectAll();
            return photos;
        }
        [HttpGet]
        [ActionName("PreferencesSelector")]
        public PreferencesList SelectAllPreferences()
        {
            PreferencesDB dB = new PreferencesDB();
            PreferencesList prefs = dB.SelectAll();
            return prefs;
        }
        [HttpGet]
        [ActionName("UserSelector")]
        public UserList SelectAllUsers()
        { UserDB dB = new UserDB();
        UserList users = dB.SelectAll();
        return users; 
        }
        [HttpPost]
        public int InsertACity([FromBody] City city)
        {
            CityDB db = new CityDB();
            db.Insert(city);
            int x = db.SaveChanges();
            return x;
        }

        [HttpPost]
        public int InsertADistance([FromBody] DistanceBetweenCities distanceBetweenCities)
        {
            DistanceBetweenCitiesDB db = new DistanceBetweenCitiesDB();
            db.Insert(distanceBetweenCities);
            int x = db.SaveChanges();
            return x;
        }

        [HttpPost]
        public int InsertAGender([FromBody] Gender gender)
        {
            GenderDB db = new   GenderDB();
            db.Insert(gender);
            int x = db.SaveChanges();
            return x;
        }
        [HttpPost]
        public int InsertALikes([FromBody] Likes likes)
        {
            LikesDB db = new LikesDB();
            db.Insert(likes);
            int x = db.SaveChanges();
            return x;
        }
        [HttpPost]
        public int InsertAManager([FromBody] Manager manager)
        {
            ManagerDB db = new ManagerDB();
            db.Insert(manager);
            int x = db.SaveChanges();
            return x;
        }
        [HttpPost]
        public int InsertAMatches([FromBody] Matches matches)
        {
            MatchesDB db = new MatchesDB();
            db.Insert(matches);
            int x = db.SaveChanges();
            return x;
        }
        [HttpPost]
        public int InsertAMessages([FromBody] Messages messages)
        {
            MessagesDB db = new MessagesDB();
            db.Insert(messages);
            int x = db.SaveChanges();
            return x;
        }
        [HttpPost]
        public int InsertAPhotos([FromBody] Photos photos)
        {
            PhotosDB db = new PhotosDB();
            db.Insert(photos);
            int x = db.SaveChanges();
            return x;
        }
        [HttpPost]
        public int InsertAPreferences([FromBody] Preferences preferences)
        {
            PreferencesDB db = new PreferencesDB();
            db.Insert(preferences);
            int x = db.SaveChanges();
            return x;
        }
        [HttpPost]
        public int InsertAUser([FromBody] User user)
        {
            UserDB db = new UserDB();
            db.Insert(user);
            int x = db.SaveChanges();
            return x;
        }


        [HttpPut]
        public int UpdateAUser([FromBody] User user)
        {
            UserDB db = new UserDB();
            db.Update(user);
            int x = db.SaveChanges();
            return x;
        }
        [HttpPut]
        public int UpdateACity([FromBody] City city)
        {
            CityDB db = new CityDB();
            db.Update(city);
            int x = db.SaveChanges();
            return x;
        }
        [HttpPut]
        public int UpdateADistanceBetweenCities([FromBody] DistanceBetweenCities distance)
        {
            DistanceBetweenCitiesDB db = new DistanceBetweenCitiesDB();
            db.Update(distance);
            int x = db.SaveChanges();
            return x;
        }
        [HttpPut]
        public int UpdateAGender([FromBody] Gender gender)
        {
            GenderDB db = new GenderDB();
            db.Update(gender);
            int x = db.SaveChanges();
            return x;
        }
        [HttpPut]
        public int UpdateALikes([FromBody] Likes likes)
        {
            LikesDB db = new LikesDB();
            db.Update(likes);
            int x = db.SaveChanges();
            return x;
        }
        [HttpPut]
        public int UpdateAManager([FromBody] Manager manager)
        {
            ManagerDB db = new ManagerDB();
            db.Update(manager);
            int x = db.SaveChanges();
            return x;
        }
        [HttpPut]
        public int UpdateAMatches([FromBody] Matches matches)
        {
            MatchesDB db = new MatchesDB();
            db.Update(matches);
            int x = db.SaveChanges();
            return x;
        }
        [HttpPut]
        public int UpdateAMessages([FromBody] Messages messages)
        {
            MessagesDB db = new MessagesDB();
            db.Update(messages);
            int x = db.SaveChanges();
            return x;
        }
        [HttpPut]
        public int UpdateAPhotos([FromBody] Photos photos)
        {
            PhotosDB db = new PhotosDB();
            db.Update(photos);
            int x = db.SaveChanges();
            return x;
        }
        [HttpPut]
        public int UpdateAPreferences([FromBody] Preferences preferences)
        {
            PreferencesDB db = new PreferencesDB();
            db.Update(preferences);
            int x = db.SaveChanges();
            return x;
        }
        [HttpDelete]
        [ActionName("DeleteCity")]
        public int DeleteCity(int id)
        {
            City city = CityDB.SelectById(id);
            if (city == null)
                return 0;

            CityDB db = new CityDB();
            db.Delete(city);
            return db.SaveChanges();
        }

        [HttpDelete]
        [ActionName("DeleteDistanceBetweenCities")]
        public int DeleteDistanceBetweenCities(int id)
        {
            DistanceBetweenCities distance = DistanceBetweenCitiesDB.SelectById(id);
            if (distance == null)
                return 0;

            DistanceBetweenCitiesDB db = new DistanceBetweenCitiesDB();
            db.Delete(distance);
            return db.SaveChanges();
        }
        [HttpDelete]
        [ActionName("DeleteGender")]
        public int DeleteGender(int id)
        {
            Gender gender = GenderDB.SelectById(id);
            if (gender == null)
                return 0;

            GenderDB db = new GenderDB();
            db.Delete(gender);
            return db.SaveChanges();
        }
        [HttpDelete]
        [ActionName("DeleteLikes")]
        public int DeleteLikes(int id)
        {
            Likes likes = LikesDB.SelectById(id);
            if (likes == null)
                return 0;

            LikesDB db = new LikesDB();
            db.Delete(likes);
            return db.SaveChanges();
        }
        [HttpDelete]
        [ActionName("DeleteManager")]
        public int DeleteManager(int id)
        {
            Manager man = ManagerDB.SelectById(id) as Manager;

            if (man == null)
                return 0;

            ManagerDB db = new ManagerDB();
            db.Delete(man);
            return db.SaveChanges();
        }
        [HttpDelete]
        [ActionName("DeleteMatches")]
        public int DeleteMatches(int id)
        {
            Matches match = MatchesDB.SelectById(id);
            if (match == null)
                return 0;

            MatchesDB db = new MatchesDB();
            db.Delete(match);
            return db.SaveChanges();
        }
        [HttpDelete]
        [ActionName("DeleteMessages")]
        public int DeleteMessages(int id)
        {
            Messages mes = MessagesDB.SelectById(id);
            if (mes == null)
                return 0;

            MessagesDB db = new MessagesDB();
            db.Delete(mes);
            return db.SaveChanges();
        }
        [HttpDelete]
        [ActionName("DeletePhotos")]
        public int DeletePhotos(int id)
        {
            Photos ph = PhotosDB.SelectById(id);
            if (ph == null)
                return 0;

            PhotosDB db = new PhotosDB();
            db.Delete(ph);
            return db.SaveChanges();
        }
        [HttpDelete]
        [ActionName("DeletePreferences")]
        public int DeletePreferences(int id)
        {
            Preferences pref = PreferencesDB.SelectById(id);
            if (pref == null)
                return 0;

            PreferencesDB db = new PreferencesDB();
            db.Delete(pref);
            return db.SaveChanges();
        }
        [HttpDelete]
        [ActionName("DeleteUser")]
        public int DeleteUser(int id)
        {
            User user = UserDB.SelectById(id);
            if (user == null)
                return 0;

            UserDB db = new UserDB();
            db.Delete(user);
            return db.SaveChanges();
        }










    }
}   
    

    

