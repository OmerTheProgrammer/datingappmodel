using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelDates;
using ViewModel;
using System.Net.Http.Json;

namespace interfaceapi
{


    public class Apiinter : DatesInterface
    {

        public HttpClient client;
       
        private readonly string uri = "https://vvc1jv51-5105.euw.devtunnels.ms/";

        public Apiinter()
        {
            client = new HttpClient();
           
        }

        public async Task<CityList> GetAllCities() =>
            await client.GetFromJsonAsync<CityList>(uri + "CitySelector"); //

        public async Task<int> InsertACity(City city) =>
            (await client.PostAsJsonAsync(uri + "InsertACity", city)).IsSuccessStatusCode ? 1 : 0; //

        public async Task<int> UpdateACity(City city) =>
            (await client.PutAsJsonAsync(uri + "UpdateACity", city)).IsSuccessStatusCode ? 1 : 0; //

        public async Task<int> DeleteACity(City city) =>
            (await client.DeleteAsync(uri + "DeleteCity" + city.Id)).IsSuccessStatusCode ? 1 : 0; //

        // --- DISTANCES ---
        public async Task<DistanceBetweenCitiesList> GetAllDistance(int id) =>
            await client.GetFromJsonAsync<DistanceBetweenCitiesList>(uri + "DistanceBetweenCitiesSelector" + id);

        public async Task<int> InsertADistance(DistanceBetweenCities distance) =>
            (await client.PostAsJsonAsync(uri + "DistanceBetweenCities/Insert", distance)).IsSuccessStatusCode ? 1 : 0;

        public async Task<int> UpdateADistance(DistanceBetweenCities distance) =>
            (await client.PutAsJsonAsync(uri + "UpdateADistanceBetweenCities", distance)).IsSuccessStatusCode ? 1 : 0;

        public async Task<int> DeleteADistance(DistanceBetweenCities distance) =>
            (await client.DeleteAsync(uri + "DeleteDistanceBetweenCities" + distance.Id)).IsSuccessStatusCode ? 1 : 0;

        // --- GENDER ---
        public async Task<GenderList> GetAllGender(int id) =>
            await client.GetFromJsonAsync<GenderList>(uri + "GenderSelector" + id);

        public async Task<int> InsertAGender(Gender gender) =>
            (await client.PostAsJsonAsync(uri + "InsertAGender", gender)).IsSuccessStatusCode ? 1 : 0;

        public async Task<int> UpdateAGender(Gender gender) =>
            (await client.PutAsJsonAsync(uri + "UpdateAGender", gender)).IsSuccessStatusCode ? 1 : 0;

        public async Task<int> DeleteAGender(Gender gender) =>
            (await client.DeleteAsync(uri + "DeleteAGender" + gender.Id)).IsSuccessStatusCode ? 1 : 0;

        // --- LIKES ---
        public async Task<LikesList> GetAllLikes(int id) => await client.GetFromJsonAsync<LikesList>(uri + "LikesSelector" + id);
        public async Task<int> InsertALikes(Likes likes) => (await client.PostAsJsonAsync(uri + "InsertALikes", likes)).IsSuccessStatusCode ? 1 : 0;
        public async Task<int> UpdateALikes(Likes likes) => (await client.PutAsJsonAsync(uri + "UpdateALikes", likes)).IsSuccessStatusCode ? 1 : 0;
        public async Task<int> DeleteALikes(Likes likes) => (await client.DeleteAsync(uri + "DeleteLikes" + likes.Id)).IsSuccessStatusCode ? 1 : 0;

        // --- MANAGER ---
        public async Task<ManagerList> GetAllManager(int id) => await client.GetFromJsonAsync<ManagerList>(uri + "ManagerSelector" + id);
        public async Task<int> InsertAManager(Manager manager) => (await client.PostAsJsonAsync(uri + "InsertAManager", manager)).IsSuccessStatusCode ? 1 : 0;
        public async Task<int> UpdateAManager(Manager manager) => (await client.PutAsJsonAsync(uri + "UpdateAManager", manager)).IsSuccessStatusCode ? 1 : 0;
        public async Task<int> DeleteAManager(Manager manager) => (await client.DeleteAsync(uri + "DeleteManager" + manager.Id)).IsSuccessStatusCode ? 1 : 0;

        // --- MATCHES ---
        public async Task<MatchesList> GetAllMatches(int id) => await client.GetFromJsonAsync<MatchesList>(uri + "MatchesSelector" + id);
        public async Task<int> InsertAMatches(Matches matches) => (await client.PostAsJsonAsync(uri + "InsertAMatches", matches)).IsSuccessStatusCode ? 1 : 0;
        public async Task<int> UpdateAMatches(Matches matches) => (await client.PutAsJsonAsync(uri + "UpdateAMatches", matches)).IsSuccessStatusCode ? 1 : 0;
        public async Task<int> DeleteAMatches(Matches matches) => (await client.DeleteAsync(uri + "DeleteMatches" + matches.Id)).IsSuccessStatusCode ? 1 : 0;

        // --- MESSAGES ---
        public async Task<MessagesList> GetAllMessages(int id) => await client.GetFromJsonAsync<MessagesList>(uri + "MessagesSelector" + id);
        public async Task<int> InsertAMessages(Messages messages) => (await client.PostAsJsonAsync(uri + "InsertAMessages", messages)).IsSuccessStatusCode ? 1 : 0;
        public async Task<int> UpdateAMessages(Messages messages) => (await client.PutAsJsonAsync(uri + "UpdateAMessages", messages)).IsSuccessStatusCode ? 1 : 0;
        public async Task<int> DeleteAMessages(Messages messages) => (await client.DeleteAsync(uri + "DeleteMessages" + messages.Id)).IsSuccessStatusCode ? 1 : 0;

        // --- PHOTOS ---
        public async Task<PhotosList> GetAllPhotos(int id) => await client.GetFromJsonAsync<PhotosList>(uri + "PhotosSelector" + id);
        public async Task<int> InsertAPhotos(Photos photos) => (await client.PostAsJsonAsync(uri + "InsertAPhotos", photos)).IsSuccessStatusCode ? 1 : 0;
        public async Task<int> UpdateAPhotos(Photos photos) => (await client.PutAsJsonAsync(uri + "UpdateAPhotos", photos)).IsSuccessStatusCode ? 1 : 0;
        public async Task<int> DeleteAPhotos(Photos photos) => (await client.DeleteAsync(uri + "DeletePhotos" + photos.Id)).IsSuccessStatusCode ? 1 : 0;

        // --- PREFERENCES ---
        public async Task<PreferencesList> GetAllPreferences(int id) => await client.GetFromJsonAsync<PreferencesList>(uri + "PreferencesSelector" + id);
        public async Task<int> InsertAPreference(Preferences preferences) => (await client.PostAsJsonAsync(uri + "InsertAPreferences", preferences)).IsSuccessStatusCode ? 1 : 0;
        public async Task<int> UpdateAPreference(Preferences preferences) => (await client.PutAsJsonAsync(uri + "UpdateAPreferences", preferences)).IsSuccessStatusCode ? 1 : 0;
        public async Task<int> DeleteAPrefrence(Preferences preferences) => (await client.DeleteAsync(uri + "DeletePreferences" + preferences.Id)).IsSuccessStatusCode ? 1 : 0;

        // --- USER ---
        public async Task<UserList> GetAllUser(int id) => await client.GetFromJsonAsync<UserList>(uri + "UserSelector" + id);
        public async Task<int> InsertAUser(User user) => (await client.PostAsJsonAsync(uri + "InsertAUser", user)).IsSuccessStatusCode ? 1 : 0;
        public async Task<int> UpdateAUser(User user) => (await client.PutAsJsonAsync(uri + "UpdateAUser", user)).IsSuccessStatusCode ? 1 : 0;
        public async Task<int> DeleteAUser(User user) => (await client.DeleteAsync(uri + "DeleteUser" + user.Id)).IsSuccessStatusCode ? 1 : 0;

        public async Task DeleteACity(int id)
        {
            throw new NotImplementedException();
        }
    }
}
