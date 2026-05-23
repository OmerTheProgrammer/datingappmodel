using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDates
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string Bio { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

     
        public City City { get; set; }

    
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public override string ToString()
        {
            return $"{Age} {City.Name} {Gender.Name} {Bio}";
        }
        public Preferences Preferences { get; set; } = new Preferences();

    }
}
