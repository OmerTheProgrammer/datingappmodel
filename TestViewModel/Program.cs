
using ModelDates;
using System;
using System.Buffers;
using System.ComponentModel.DataAnnotations;
using ViewModel;
namespace TestViewModel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CityDB cdb = new();
            CityList cityList = cdb.SelectAll();
            foreach (City c in cityList)
                Console.WriteLine(c.Name);

            GenderDB gdb = new();
            GenderList genderList = gdb.SelectAll();
            foreach (Gender g in genderList)
                Console.WriteLine(g.Name);

            UserDB udb = new();
            UserList userList = udb.SelectAll();
            foreach (User u in userList)
            {
                Console.WriteLine(u.Username + " : " + u.Gender);
                Console.WriteLine(u.Password +
               " : " + u.Email + " : " + u.Age + " : " + u.Bio + " : " + u.City +
               " : " + u.CreatedAt + " : " + u.DateOfBirth);
            }
            Console.BackgroundColor = ConsoleColor.Red;

            DistanceBetweenCitiesDB ddb = new();
            DistanceBetweenCitiesList distancebetweencitiesList = ddb.SelectAll();
            foreach (DistanceBetweenCities d in distancebetweencitiesList)
                Console.WriteLine(d.City1 + " : " + d.City2 + " : " + d.DistanceKm);

            Console.BackgroundColor = ConsoleColor.Yellow;
            PreferencesDB pdb = new();
            PreferencesList preferencesList = pdb.SelectAll();
            foreach (Preferences p in preferencesList)
                Console.WriteLine(p.User + " : " + p.MinAge + " : " + p.MaxAge + " : " + p.PreferredGender
                    + " : " + p.MaxDistanceKm);

            LikesDB ldb = new();
            LikesList likesList = ldb.SelectAll();
            foreach (Likes l in likesList)
                Console.WriteLine(l.Liker + "  :  " + l.LikedUser);

            PhotosDB phdb = new();
            PhotosList photosList = phdb.SelectAll();
            foreach (Photos ph in photosList)
                Console.WriteLine(ph.User + " : " + ph.Url);

            MatchesDB mdb = new();
            MatchesList matchesList = mdb.SelectAll();
            foreach (Matches m in matchesList)
                Console.WriteLine(m.User1 + "  :  " + m.User2);

            ManagerDB mandb = new();
            ManagerList managerList = mandb.SelectAll();
            foreach (Manager man in managerList)
                Console.WriteLine(man.Password +
               " : " + man.MangPassword + " : " + man.Email + " : " + man.Age +
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
            userToUpdate.Username = "Mazgan111";
            userToUpdate.Password = "4747";
            userToUpdate.CreatedAt = DateTime.Now;
            userToUpdate.DateOfBirth = DateTime.Now;
            userToUpdate.Gender = gList.Last();
            udb.Update(userToUpdate);
            x = udb.SaveChanges();
            Console.WriteLine(System.IO.Path.GetFullPath("YourDatabaseName.accdb"));
            Console.WriteLine($"{x} rows were updated");
            uList = udb.SelectAll();
            foreach (User u in uList)
                Console.WriteLine(u.Username);

            ddb = new();
            DistanceBetweenCitiesList dList = ddb.SelectAll();
            foreach (DistanceBetweenCities d in dList)
                Console.WriteLine(d.City1);

            DistanceBetweenCities distanceToUpdate = dList[0];
            distanceToUpdate.City1 = cList.First();
            distanceToUpdate.City2 = cList.Last();
            distanceToUpdate.DistanceKm = 85;

            ddb.Update(distanceToUpdate);
            x = ddb.SaveChanges();
       
            Console.WriteLine($"{x} rows were updated");
            dList = ddb.SelectAll();
            foreach (DistanceBetweenCities d in dList)
            {
                Console.WriteLine(d.City1+" " + d.City2 + " " + d.DistanceKm);
               
            }

            ldb = new();
            LikesList lList = ldb.SelectAll();
            foreach (Likes l in lList)
                Console.WriteLine(l.LikedUser);

            Likes likesToUpdate = lList[0];
            likesToUpdate.Liker = uList.First();
            likesToUpdate.LikedUser = uList.Last();
           

            ldb.Update(likesToUpdate);
            x = ldb.SaveChanges();

            Console.WriteLine($"{x} rows were updated");
            lList = ldb.SelectAll();
            foreach (Likes l in lList)
            {
                Console.WriteLine(l.Liker + " " + l.LikedUser);

            }

            mdb = new();
            MatchesList mList = mdb.SelectAll();
            foreach (Matches m in mList)
                Console.WriteLine(m.User1);

            Matches matchesToUpdate = mList[0];
            matchesToUpdate.User1 = uList.First();
            matchesToUpdate.User2 = uList.Last();


            mdb.Update(matchesToUpdate);
            x = mdb.SaveChanges();

            Console.WriteLine($"{x} rows were updated");
            mList = mdb.SelectAll();
            foreach (Matches m in mList)
            {
                Console.WriteLine(m.User1 + " " + m.User2);

            }

            medb = new();
            MessagesList messageList = medb.SelectAll();
            foreach (Messages m in messageList)
                Console.WriteLine(m.MessageText);

            Messages messageToUpdate = messageList[0];
            messageToUpdate.MessageText ="heh lol kek cheburek";
            


            medb.Update(messageToUpdate);
            x = mdb.SaveChanges();

            Console.WriteLine($"{x} rows were updated");
            messageList = medb.SelectAll();
            foreach (Messages m in messageList)
            {
                Console.WriteLine(m.Sender + " " + m.MessageText);

            }

            phdb = new();
            PhotosList phList = phdb.SelectAll();
            foreach (Photos ph in phList)
                Console.WriteLine(ph.Url);

            Photos photosToUpdate = phList[0];
            photosToUpdate.User = uList[0];
            photosToUpdate.Url = "cat2.jpg";



            phdb.Update(photosToUpdate);
            x = phdb.SaveChanges();

            Console.WriteLine($"{x} rows were updated");
            phList = phdb.SelectAll();
            foreach (Photos ph in phList)
            {
                Console.WriteLine(ph.User+" "+ph.Url);

            }

            pdb = new();
            PreferencesList pList = pdb.SelectAll();
            foreach (Preferences p in pList)
                Console.WriteLine(p.User);

            Preferences preferenceToUpdate = pList[0];
            preferenceToUpdate.User = uList[0];
            preferenceToUpdate.PreferredGender=gList[2];
            preferenceToUpdate.MinAge = 25;
            preferenceToUpdate.MaxAge = 48;
            preferenceToUpdate.MaxDistanceKm = 245;



            pdb.Update(preferenceToUpdate);
            x = pdb.SaveChanges();

            Console.WriteLine($"{x} rows were updated");
            pList = pdb.SelectAll();
            foreach (Preferences p in pList)
            {
                Console.WriteLine(p.User + " " + p.PreferredGender+" "+ p.MinAge+" "+p.MaxAge+" "+p.MaxDistanceKm);

            }

            mandb = new();
            ManagerList manList = mandb.SelectAll();
            foreach (Manager m in manList)
                Console.WriteLine(m.Username);

            Manager managerToUpdate = manList[0];
            managerToUpdate.City = cList.Last();
            managerToUpdate.Email ="matanishungry@gmail.com";
            managerToUpdate.Age = 67;
            managerToUpdate.Bio ="likes to go fishing!";
            managerToUpdate.CreatedAt =DateTime.Now;
            managerToUpdate.DateOfBirth= DateTime.Now;
            managerToUpdate.MangPassword = "i like chicken nuggies";
            managerToUpdate.Username = "EmoKid";
            managerToUpdate.Gender=gList.Last();



            mandb.Update(managerToUpdate);
            x = mandb.SaveChanges();

            Console.WriteLine($"{x} rows were updated");
            manList = mandb.SelectAll();
            foreach (Manager m in manList)
            {
                Console.WriteLine( m.City + " " + m.Email + " " + m.Gender + " " + m.Age+" "+m.Bio+" "
                    +m.CreatedAt+" "+m.CreatedAt+" "+ m.DateOfBirth+" "+ m.MangPassword);

            }

            Console.BackgroundColor = ConsoleColor.Green;

            City c1 = new City()
            {
                Name = "Kiryat Shmona"
            };
            cdb.Insert(c1);
            x = cdb.SaveChanges();
            Console.WriteLine($"city added {x}  rows" + " : " + c1.Name);

            Gender g1 = new Gender()
            {
                Name = "Genderfluid"
            };
            gdb.Insert(g1);
            x = gdb.SaveChanges();
            Console.WriteLine($"gender added {x}  rows" + " : " + g1.Name);

            Random rand = new Random();
            User u1 = new User()
            {
                Age = 20,
                City = cList[2],
                Bio = "im new",
                CreatedAt = DateTime.Now,
                DateOfBirth = DateTime.Now.AddYears(rand.Next(10, 30)),
                Email = "cat1234@gmail.com",
                Gender = gList[0],
                Password = "lalala",
                Username = "johnny11111"
            };
            udb.Insert(u1);
            x=udb.SaveChanges();
            Console.WriteLine($"user added {x}  rows"+" : "+u1.Username);

            DistanceBetweenCities d1 = new DistanceBetweenCities()
            {
                City1 = cList.Last(),
                City2 = cList[0],
                DistanceKm = 195
            };
            ddb.Insert(d1);
            x = ddb.SaveChanges();
            Console.WriteLine($"distance between cities added {x}  rows" + " : " + d1.DistanceKm);

            Likes l1 = new Likes()
            {
                Liker = uList[1],
                LikedUser = uList.Last(),
                
            };
            ldb.Insert(l1);
            x = ldb.SaveChanges();
            Console.WriteLine($"likes added {x}  rows" + " : " + l1.Liker+" liked "+ l1.LikedUser);

            Matches m1 = new Matches()
            {
                User1 = uList[1],
                User2 = uList.Last(),

            };
            mdb.Insert(m1);
            x = mdb.SaveChanges();
            Console.WriteLine($"matches added {x}  rows" + " : " + m1.User1 + " and " + m1.User2
                +" are a match!");

            Random rand1 = new Random();
            Messages me1 = new Messages()
            {
                 Match=mList.Last(),
                 Sender=uList.Last(),
                 MessageText="HELLO PRETTY",
                SentAt = DateTime.Now.AddMinutes(rand1.Next(10, 30)),
                
            };
            medb.Insert(me1);
            x = medb.SaveChanges();
            Console.WriteLine($"messages added {x}  rows" + " : " + me1.MessageText);

            Photos ph1 = new Photos()
            {
                User = uList[2],
                 Url="cat2.jpg",

            };
            phdb.Insert(ph1);
            x = phdb.SaveChanges();
            Console.WriteLine($"photos added {x}  rows" + " : " + ph1.User.Username + " added " + ph1.Url);

            Preferences p1 = new Preferences()
            {
                User = uList.Last(),
                PreferredGender = gList[2],
                MinAge = 19,
                MaxAge=69,
                 MaxDistanceKm=196,

            };
            pdb.Insert(p1);
            x = pdb.SaveChanges();
            Console.WriteLine($"preferences added {x}  rows" + " : " + p1.User.Username + " prefernces are:  " + p1.PreferredGender+ " "
                +p1.MaxDistanceKm+" "+ p1.MinAge+" "+p1.MaxAge);
        }
    }
}
