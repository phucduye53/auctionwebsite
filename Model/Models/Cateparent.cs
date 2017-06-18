using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace auctionwebsite.Models
{
    public class Cateparent
    {
        public int CateparentID { get; set; }
        [Required(ErrorMessage = "Chưa nhập tên danh mục")]
        [StringLength(50, ErrorMessage = "Tên danh mục chỉ được ít hơn 50 ký tự.")]
        public string CateparentName { get; set; }
        public virtual ICollection<Cate> cates { get; set; }
    }
}