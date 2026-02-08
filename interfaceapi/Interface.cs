
using ModelDates;
using ViewModel;

namespace interfaceapi
{
    public interface DatesInterface
    {
        public Task<CityList> GetAllCities();

        public Task<int> InsertACity(City city);

        public Task<int> UpdateACity(City city);

        public Task<int> DeleteACity(City city);

        public Task<DistanceBetweenCitiesList> GetAllDistance(int id);

        public Task<int> InsertADistance(DistanceBetweenCities distance);

        public Task<int> UpdateADistance(DistanceBetweenCities distance);

        public Task<int> DeleteADistance(DistanceBetweenCities distance);

        public Task<GenderList> GetAllGender(int id);

        public Task<int> InsertAGender(Gender gender);

        public Task<int> UpdateAGender(Gender gender);

        public Task<int> DeleteAGender(Gender gender);

        public Task<LikesList> GetAllLikes(int id);

        public Task<int> InsertALikes(Likes likes);

        public Task<int> UpdateALikes(Likes likes);

        public Task<int> DeleteALikes(Likes likes);

        public Task<ManagerList> GetAllManager(int id);

        public Task<int> InsertAManager(Manager manager);

        public Task<int> UpdateAManager(Manager manager);

        public Task<int> DeleteAManager(Manager manager);

        public Task<MatchesList> GetAllMatches(int id);

        public Task<int> InsertAMatches(Matches matches);

        public Task<int> UpdateAMatches(Matches matches);

        public Task<int> DeleteAMatches(Matches matches);

        public Task<MessagesList> GetAllMessages(int id);

        public Task<int> InsertAMessages(Messages messages);

        public Task<int> UpdateAMessages(Messages messages);

        public Task<int> DeleteAMessages(Messages messages);

        public Task<PhotosList> GetAllPhotos(int id);

        public Task<int> InsertAPhotos(Photos photos);

        public Task<int> UpdateAPhotos(Photos photos);

        public Task<int> DeleteAPhotos(Photos photos);

        public Task<PreferencesList> GetAllPreferences(int id);

        public Task<int> InsertAPreference(Preferences preferences);

        public Task<int> UpdateAPreference(Preferences preferences);

        public Task<int> DeleteAPrefrence(Preferences preferences);

        public Task<UserList> GetAllUser(int id);

        public Task<int> InsertAUser(User user);

        public Task<int> UpdateAUser(User user);

        public Task<int> DeleteAUser(User user);
    }
}
