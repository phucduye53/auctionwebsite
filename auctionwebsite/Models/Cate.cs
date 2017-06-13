using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace auctionwebsite.Models
{
    public class Cate
    {
        public int CateID { get; set; }
        [Required(ErrorMessage="Chưa nhập tên danh mục")]
        [StringLength(50, ErrorMessage = "Tên danh mục chỉ được ít hơn 50 ký tự.")]
        public string CateName { get; set; }
        [Required(ErrorMessage="Chưa chọn danh mục cha.")]
        public int CateparentID { get; set; }
        public virtual Cateparent Cateparent { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}