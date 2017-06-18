using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace auctionwebsite.Models
{
    public class Favorite
    {
        public int FavoriteID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }

    }
}