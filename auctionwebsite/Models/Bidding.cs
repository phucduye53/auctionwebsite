using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace auctionwebsite.Models
{
    public class Bidding
    {
        public int BiddingID { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public string DateBid { get; set; }
        public int PriceBid { get; set; }
        public int ProductBid { get; set; }
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}