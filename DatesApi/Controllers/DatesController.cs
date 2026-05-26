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
        [HttpGet("GetMatchesForUser/{userId}")]
        public IActionResult GetMatchesForUser(int userId)
        {
            MatchesDB db = new MatchesDB();
            // You need to add this method 'GetMatchesByUserId' to your MatchesDB class
            // It should return only the matches where User1 == userId OR User2 == userId
            var matches = db.GetMatchesForUser(userId);
            return Ok(matches);
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
        [HttpGet("{userId}")]
        [ActionName("PreferencesSelector")]
        public IActionResult PreferencesSelector(int userId)
        {
            // 1. Instantiate the DB class properly to access non-static methods
            using (var db = new ViewModel.PreferencesDB())
            {
                var allPrefs = db.SelectAll();
                System.Diagnostics.Debug.WriteLine($"Total preferences in DB: {allPrefs.Count}");
            }

            // 2. Call your static lookup method
            var prefs = ViewModel.PreferencesDB.SelectByUserId(userId);

            // 3. Logic check
            if (prefs == null)
            {
                return NotFound($"No preferences found for user {userId}");
            }

            return Ok(prefs); 
        }

        [HttpGet]
        [ActionName("UserSelector")]
        public UserList SelectAllUsers()
        { UserDB dB = new UserDB();
        UserList users = dB.SelectAll();
        return users; 
        }

        [HttpGet("GetUser/{id}")]
        public IActionResult GetUser(int id)
        {
            // Use your existing DB helper class instead of _context
            UserDB db = new UserDB();

            // Call the method that finds a user by ID
            // Note: You might need to check your UserDB.cs to see if 
            // it has a 'SelectById' method. If not, you can filter the list:
            var user = db.SelectAll().FirstOrDefault(u => u.Id == id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        [ActionName("GetDiscoveryFeed")]
        public IActionResult GetDiscoveryFeed([FromBody] DiscoveryFeedDTO discoveryFeedDTO)
        {
            if (discoveryFeedDTO == null || discoveryFeedDTO.Preferences == null)
            {
                return BadRequest($"Invalid request parameters. discoveryFeedDTO:{discoveryFeedDTO},discoveryFeedDTO.Preferences: {discoveryFeedDTO?.Preferences} ");
            }

            try
            {
                ViewModel.UserDB db = new ViewModel.UserDB();
                List<ModelDates.User> discoveryQueue = db.SelectFilteredDiscovery(
                    discoveryFeedDTO.UserId,
                    discoveryFeedDTO.Preferences.PreferredGender.Id,
                    discoveryFeedDTO.Preferences.AgeMin,
                    discoveryFeedDTO.Preferences.AgeMax,
                    discoveryFeedDTO.Preferences.DistanceMax
                );

                return Ok(discoveryQueue);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetMessages/{matchId}")]
        public IActionResult GetMessages(int matchId)
        {
            MessagesDB db = new MessagesDB();
            return Ok(db.GetMessagesByMatchId(matchId));
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
        [HttpPost("LikeUser")]
        public IActionResult LikeUser(int likerId, int likedId)
        {
            LikesDB likesDb = new LikesDB();

            // 1. Add the new like
            Likes newLike = new Likes
            {
                Liker = new User { Id = likerId },
                LikedUser = new User { Id = likedId }
            };
            likesDb.Insert(newLike);
            likesDb.SaveChanges(); // Make sure to save the changes to the database

            // 2. Check for match: Did the 'likedId' person already like 'likerId'?
            bool isMatch = likesDb.HasLiked(likedId, likerId);

            if (isMatch)
            {
                // 3. Add to Matches table
                MatchesDB matchDb = new MatchesDB();
                // Assuming your Matches entity has properties like User1 and User2
                matchDb.Insert(new Matches { User1 = new User { Id = likerId }, User2 = new User { Id = likedId } });
                matchDb.SaveChanges();

                return Ok(new { matchFound = true });
            }

            return Ok(new { matchFound = false });
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
        [HttpPost("SendMessage")]
        public IActionResult SendMessage([FromBody] Messages messages)
        {
            // 1. Set the timestamp here on the server side
            // This ensures consistency instead of relying on the client's clock
            messages.SentAt = DateTime.Now;

            // 2. Perform the insertion
            MessagesDB db = new MessagesDB();
            db.Insert(messages);
            int result = db.SaveChanges();

            // 3. Return a standard response
            if (result > 0)
            {
                return Ok(new { success = true });
            }
            else
            {
                return BadRequest("Failed to send message.");
            }
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
        [HttpPost("DiscoveryFeed")]
        public IActionResult GetDiscoveryFeed([FromBody] User currentUser)
        {
            try
            {
                // Fallback checks in case the incoming tracking object or its relations are null
                if (currentUser == null || currentUser.Preferences == null)
                {
                    return BadRequest("User context preferences payload missing.");
                }

                // FIX CS0029: Drill down past the Gender object directly into its internal ID field property!
                // Assuming your ModelDates.Gender class exposes its primary integer key as .Id or .GenderId.
                // Change '.Id' below if your class utilizes a different identifier key name (e.g., .Id).
                int preferredGenderId = currentUser.Preferences.PreferredGender != null
                    ? currentUser.Preferences.PreferredGender.Id
                    : 3; // Default fallback to 3 (Both) if not set

                int minAge = currentUser.Preferences.AgeMin;
                int maxAge = currentUser.Preferences.AgeMax;
                int maxDistance = currentUser.Preferences.DistanceMax;

                // Call our database selection method with pure integer values
                // Inside DatesController.cs -> GetDiscoveryFeed
                UserDB db = new UserDB();
                List<User> discoveryQueue = db.SelectFilteredDiscovery(
                    currentUser.Id,
                    preferredGenderId,
                    minAge,
                    maxAge,
                    maxDistance
                );

                return Ok(discoveryQueue);
            }
            catch (Exception ex)
            {
                return BadRequest($"Discovery Feed Failed: {ex.Message}");
            }
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
        [HttpDelete("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            // 1. Find the user first
            User user = UserDB.SelectById(id);
            if (user == null)
                return NotFound(new { message = "User not found" });

            // 2. Perform the deletion
            UserDB db = new UserDB();
            db.Delete(user);
            db.SaveChanges(); // Ensure this is called to commit the change

            return Ok(new { message = "User deleted successfully" });
        }










    }
}   
    

    

