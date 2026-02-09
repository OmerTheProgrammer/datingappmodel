using ModelDates;
using ViewModel;
using DatesApi;
using interfaceapi;


Apiinter  apiservice = new();
CityList cities = await apiservice.GetAllCities();
Console.WriteLine(cities.Count);

int id = cities.Last().Id;
await apiservice.DeleteACity(id);
Console.WriteLine(cities.Count);

City c1 = new City() { Name = "בית אל" };
await apiservice.InsertACity(c1);

City myCity = cities.First();
myCity.Name = "נתניה";
await apiservice.UpdateACity(myCity);
