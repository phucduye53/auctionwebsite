using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace auctionwebsite.Models
{
    public class Cate
    {
        public int CateID { get; set; }
        public string CateName { get; set; }
        public int CateparentID { get; set; }
        public virtual Cateparent Cateparent { get; set; }
    }
}