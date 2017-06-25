using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace auctionwebsite.Models
{
    public class Rate
    {
        public int RateID { get; set; }
        public int UserID { get; set; }
        public int RateStatus { get; set; } // 0 = like , 1  = dislike
        public string RateComment { get; set; }
        public int UserTargetID { get; set; }
        public string Date { get; set; }
        public virtual User User { get; set; }
        public virtual User UserTarget { get; set; }
        public class RateMapping : EntityTypeConfiguration<Rate>
        {
            public RateMapping()
            {
                HasRequired(m => m.User).WithMany(m => m.RateUser).HasForeignKey(m=>m.UserID);
                HasRequired(m => m.UserTarget).WithMany(m => m.RateTargetUser).HasForeignKey(m => m.UserTargetID);
            }
        }

    }
}