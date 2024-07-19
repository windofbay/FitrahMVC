using System;
using System.Collections.Generic;

namespace FitrahDataAccess.Models
{
    public partial class Account
    {
        public Account()
        {
            Histories = new HashSet<History>();
        }

        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<History> Histories { get; set; }
    }
}
