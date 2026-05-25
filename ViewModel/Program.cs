
using ModelDates;
using System;
using System.Buffers;
using System.ComponentModel.DataAnnotations;
namespace ViewModel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CityDB cdb = new();
            CityList cityList = cdb.SelectAll();
            foreach(City c in cityList)
            Console.WriteLine(c.Name);
            
            GenderDB gdb = new();
            GenderList genderList = gdb.SelectAll();
            foreach (Gender g in genderList)
                Console.WriteLine(g.Name);

            UserDB udb = new();
            UserList userList = udb.SelectAll();
            foreach (User u in userList)
            {
                Console.WriteLine(u.Username+" : "+u.Gender);
                Console.WriteLine(u.Password+
               " : "+ u.Email+" : "+ u.Age+" : "+ u.Bio+" : "+ u.City+
               " : " + u.CreatedAt +" : "+ u.DateOfBirth);
            }
           Console.BackgroundColor = ConsoleColor.Red;

            DistanceBetweenCitiesDB ddb = new();
            DistanceBetweenCitiesList distancebetweencitiesList = ddb.SelectAll();
            foreach (DistanceBetweenCities d in distancebetweencitiesList)
                Console.WriteLine(d.City1+" : "+ d.City2+" : "+d.DistanceKm); 

            Console.BackgroundColor = ConsoleColor.Yellow;
            PreferencesDB pdb = new();
            PreferencesList preferencesList = pdb.SelectAll();
            foreach (Preferences p in preferencesList)
                Console.WriteLine(p.User+" : "+ p.AgeMin+" : "+ p.AgeMax+" : "+ p.PreferredGender
                    +" : "+p.DistanceMax);

            LikesDB ldb = new();
            LikesList likesList = ldb.SelectAll();
            foreach (Likes l in likesList)
                Console.WriteLine(l.Liker+"  :  "+l.LikedUser);

            PhotosDB phdb = new();
            PhotosList photosList = phdb.SelectAll();
            foreach (Photos ph in photosList)
                Console.WriteLine(ph.User+" : "+ ph.Url); 

            MatchesDB mdb = new();
            MatchesList matchesList = mdb.SelectAll();
            foreach (Matches m in matchesList)
                Console.WriteLine(m.User1 + "  :  " + m.User2); 

            ManagerDB mandb = new();
            ManagerList managerList = mandb.SelectAll();
            foreach (Manager man in managerList)
                Console.WriteLine(man.Password +
               " : "+man.MangPassword+" : " + man.Email + " : " + man.Age + 
               " : " + man.Bio + " : " + man.City +
               " : " + man.CreatedAt + " : " + man.DateOfBirth); 

            MessagesDB medb = new();
            MessagesList messagesList = medb.SelectAll();
            foreach (Messages me in messagesList)
                Console.WriteLine($"{me.Match.User1}" +
                    $" & {me.Match.User2} : {me.Sender} : {me.MessageText} : {me.SentAt}"); 


            cdb = new();
            CityList cList = cdb.SelectAll();
            foreach (City c in cList)
                Console.WriteLine(c.Name);

            City cityToUpdate = cList[0];
            cityToUpdate.Name = " נננ";
            cdb.Update(cityToUpdate);
            int x = cdb.SaveChanges();
            Console.WriteLine($"{x} rows were updated");

            gdb = new();
            GenderList gList = gdb.SelectAll();
            foreach (Gender g in gList)
                Console.WriteLine(g.Name);

            Gender genderToUpdate = gList[0];
            genderToUpdate.Name = " נננ";
            gdb.Update(genderToUpdate);
            int xx = gdb.SaveChanges();
            Console.WriteLine($"{x} rows were updated");

            udb = new();
            UserList uList = udb.SelectAll();
            foreach (User u in uList)
                Console.WriteLine(u.Username); 

            User userToUpdate = uList[0];
            userToUpdate.City = cList.First();
            userToUpdate.Age = 100;
            userToUpdate.Bio = "lololollllllllllllllllll";
            userToUpdate.Email = "meow@gmail.com";
            userToUpdate.Username = "Mazgan";
            userToUpdate.Password = "4747";
            userToUpdate.CreatedAt= DateTime.Now;
            userToUpdate.DateOfBirth = DateTime.Now;
            userToUpdate.Gender = gList.Last();
            udb.Update(userToUpdate);
            x = udb.SaveChanges();
            Console.WriteLine($"{x} rows were updated");
            uList = udb.SelectAll();
            foreach (User u in uList)
                Console.WriteLine(u.Username);

        }
    }
}
