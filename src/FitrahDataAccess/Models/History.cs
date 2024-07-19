using System;
using System.Collections.Generic;

namespace FitrahDataAccess.Models
{
    public partial class History
    {
        public string Code { get; set; } = null!;
        public string MuzakkiName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime Date { get; set; }
        public int? Quantity { get; set; }
        public decimal? FitrahMoney { get; set; }
        public decimal? FitrahRice { get; set; }
        public decimal? InfaqMoney { get; set; }
        public decimal? InfaqRice { get; set; }
        public decimal? FidiyaMoney { get; set; }
        public decimal? FidiyaRice { get; set; }
        public decimal? MaalMoney { get; set; }
        public decimal? MaalRice { get; set; }
        public string AmilUsername { get; set; } = null!;
        public string? Note { get; set; }

        public virtual Account AmilUsernameNavigation { get; set; } = null!;
    }
}
