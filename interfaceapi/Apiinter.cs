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

        public Apiinter()
        {
            string uri = "http://localhost:5105/api/Dates/";
            client = new HttpClient();
        }

        public async Task<CityList> GetAllCities() =>
            await client.GetFromJsonAsync<CityList>(uri + "/api/Insert/citySelector"); //

        public async Task<int> InsertACity(City city) =>
            (await client.PostAsJsonAsync(uri + "/api/Dates/InsertACity", city)).IsSuccessStatusCode ? 1 : 0; //

        public async Task<int> UpdateACity(City city) =>
            (await client.PutAsJsonAsync(uri + "/api/Dates/UpdateACity", city)).IsSuccessStatusCode ? 1 : 0; //

        public async Task<int> DeleteACity(City city) =>
            (await client.DeleteAsync(uri + "/api/Dates/DeleteCity" + city.Id)).IsSuccessStatusCode ? 1 : 0; //

        // --- DISTANCES ---
        public async Task<DistanceBetweenCitiesList> GetAllDistance(int id) =>
            await client.GetFromJsonAsync<DistanceBetweenCitiesList>(uri + "/api/Dates/DistanceBetweenCitiesSelector" + id);

        public async Task<int> InsertADistance(DistanceBetweenCities distance) =>
            (await client.PostAsJsonAsync(uri + "/api/DistanceBetweenCities/Insert", distance)).IsSuccessStatusCode ? 1 : 0;

        public async Task<int> UpdateADistance(DistanceBetweenCities distance) =>
            (await client.PutAsJsonAsync(uri + "/api/Dates/UpdateADistanceBetweenCities", distance)).IsSuccessStatusCode ? 1 : 0;

        public async Task<int> DeleteADistance(DistanceBetweenCities distance) =>
            (await client.DeleteAsync(uri + "/api/Distances/Delete/" + distance.Id)).IsSuccessStatusCode ? 1 : 0;

        // --- GENDER ---
        public async Task<GenderList> GetAllGender(int id) =>
            await client.GetFromJsonAsync<GenderList>(uri + "/api/Genders/" + id);

        public async Task<int> InsertAGender(Gender gender) =>
            (await client.PostAsJsonAsync(uri + "/api/Genders/Insert", gender)).IsSuccessStatusCode ? 1 : 0;

        public async Task<int> UpdateAGender(Gender gender) =>
            (await client.PutAsJsonAsync(uri + "/api/Genders/Update", gender)).IsSuccessStatusCode ? 1 : 0;

        public async Task<int> DeleteAGender(Gender gender) =>
            (await client.DeleteAsync(uri + "/api/Genders/Delete/" + gender.Id)).IsSuccessStatusCode ? 1 : 0;

        // --- LIKES ---
        public async Task<LikesList> GetAllLikes(int id) => await client.GetFromJsonAsync<LikesList>(uri + "/api/Likes/" + id);
        public async Task<int> InsertALikes(Likes likes) => (await client.PostAsJsonAsync(uri + "/api/Likes", likes)).IsSuccessStatusCode ? 1 : 0;
        public async Task<int> UpdateALikes(Likes likes) => (await client.PutAsJsonAsync(uri + "/api/Likes", likes)).IsSuccessStatusCode ? 1 : 0;
        public async Task<int> DeleteALikes(Likes likes) => (await client.DeleteAsync(uri + "/api/Likes/" + likes.Id)).IsSuccessStatusCode ? 1 : 0;

        // --- MANAGER ---
        public async Task<ManagerList> GetAllManager(int id) => await client.GetFromJsonAsync<ManagerList>(uri + "/api/Managers/" + id);
        public async Task<int> InsertAManager(Manager manager) => (await client.PostAsJsonAsync(uri + "/api/Managers", manager)).IsSuccessStatusCode ? 1 : 0;
        public async Task<int> UpdateAManager(Manager manager) => (await client.PutAsJsonAsync(uri + "/api/Managers", manager)).IsSuccessStatusCode ? 1 : 0;
        public async Task<int> DeleteAManager(Manager manager) => (await client.DeleteAsync(uri + "/api/Managers/" + manager.Id)).IsSuccessStatusCode ? 1 : 0;

        // --- MATCHES ---
        public async Task<MatchesList> GetAllMatches(int id) => await client.GetFromJsonAsync<MatchesList>(uri + "/api/Matches/" + id);
        public async Task<int> InsertAMatches(Matches matches) => (await client.PostAsJsonAsync(uri + "/api/Matches", matches)).IsSuccessStatusCode ? 1 : 0;
        public async Task<int> UpdateAMatches(Matches matches) => (await client.PutAsJsonAsync(uri + "/api/Matches", matches)).IsSuccessStatusCode ? 1 : 0;
        public async Task<int> DeleteAMatches(Matches matches) => (await client.DeleteAsync(uri + "/api/Matches/" + matches.Id)).IsSuccessStatusCode ? 1 : 0;

        // --- MESSAGES ---
        public async Task<MessagesList> GetAllMessages(int id) => await client.GetFromJsonAsync<MessagesList>(uri + "/api/Messages/" + id);
        public async Task<int> InsertAMessages(Messages messages) => (await client.PostAsJsonAsync(uri + "/api/Messages", messages)).IsSuccessStatusCode ? 1 : 0;
        public async Task<int> UpdateAMessages(Messages messages) => (await client.PutAsJsonAsync(uri + "/api/Messages", messages)).IsSuccessStatusCode ? 1 : 0;
        public async Task<int> DeleteAMessages(Messages messages) => (await client.DeleteAsync(uri + "/api/Messages/" + messages.Id)).IsSuccessStatusCode ? 1 : 0;

        // --- PHOTOS ---
        public async Task<PhotosList> GetAllPhotos(int id) => await client.GetFromJsonAsync<PhotosList>(uri + "/api/Photos/" + id);
        public async Task<int> InsertAPhotos(Photos photos) => (await client.PostAsJsonAsync(uri + "/api/Photos", photos)).IsSuccessStatusCode ? 1 : 0;
        public async Task<int> UpdateAPhotos(Photos photos) => (await client.PutAsJsonAsync(uri + "/api/Photos", photos)).IsSuccessStatusCode ? 1 : 0;
        public async Task<int> DeleteAPhotos(Photos photos) => (await client.DeleteAsync(uri + "/api/Photos/" + photos.Id)).IsSuccessStatusCode ? 1 : 0;

        // --- PREFERENCES ---
        public async Task<PreferencesList> GetAllPreferences(int id) => await client.GetFromJsonAsync<PreferencesList>(uri + "/api/Preferences/" + id);
        public async Task<int> InsertAPreference(Preferences preferences) => (await client.PostAsJsonAsync(uri + "/api/Preferences", preferences)).IsSuccessStatusCode ? 1 : 0;
        public async Task<int> UpdateAPreference(Preferences preferences) => (await client.PutAsJsonAsync(uri + "/api/Preferences", preferences)).IsSuccessStatusCode ? 1 : 0;
        public async Task<int> DeleteAPrefrence(Preferences preferences) => (await client.DeleteAsync(uri + "/api/Preferences/" + preferences.Id)).IsSuccessStatusCode ? 1 : 0;

        // --- USER ---
        public async Task<UserList> GetAllUser(int id) => await client.GetFromJsonAsync<UserList>(uri + "/api/Users/" + id);
        public async Task<int> InsertAUser(User user) => (await client.PostAsJsonAsync(uri + "/api/Users", user)).IsSuccessStatusCode ? 1 : 0;
        public async Task<int> UpdateAUser(User user) => (await client.PutAsJsonAsync(uri + "/api/Users", user)).IsSuccessStatusCode ? 1 : 0;
        public async Task<int> DeleteAUser(User user) => (await client.DeleteAsync(uri + "/api/Users/" + user.Id)).IsSuccessStatusCode ? 1 : 0;
    }
}
