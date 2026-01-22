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
            int x = CityDB.SaveChanges();
            return x;
        }
    }
}   
    

    

