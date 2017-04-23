using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace auctionwebsite.Models
{
    public class Cateparent
    {
        public int CateparentID { get; set; }
        public string CateparentName { get; set; }
        public virtual ICollection<Cate> cates { get; set; }
    }
}