using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace auctionwebsite.Models
{
    public class FileDetail
    {
        public int FileDetailID { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
    }
}