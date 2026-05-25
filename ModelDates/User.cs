using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDates
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string Profilepic { get; set; } = string.Empty; // Synced!
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Age { get; set; }

        // MAKE SURE THESE HAVE THE '?' MARK:
        public Gender? Gender { get; set; }
        public City? City { get; set; }
        public Preferences? Preferences { get; set; }
    }
}
