using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDates
{
    public class Preferences : BaseEntity
    {
        // FIX: Made nullable so API model validation doesn't block incoming registrations
        public User? User { get; set; }

        public int AgeMin { get; set; }
        public int AgeMax { get; set; }

        // FIX: Made nullable so it accepts just passing the Gender ID cleanly
        public Gender? PreferredGender { get; set; }

        public int DistanceMax { get; set; }
    }
}
