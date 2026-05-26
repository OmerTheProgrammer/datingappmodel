using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ModelDates
{
    public partial class Messages : BaseEntity // Added 'partial' here
    {
        public Matches Match { get; set; }
        public User Sender { get; set; }
        public int MatchId { get; set; }
        public int SenderId { get; set; }
        public string MessageText { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}
