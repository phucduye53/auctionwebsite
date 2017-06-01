using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace auctionwebsite.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Chưa nhập tên sản phẩm")]
        [StringLength(50, ErrorMessage = "Tên sản phẩm chỉ được ít hơn 50 ký tự.")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Chưa nhập giá sản phẩm")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Giá sản phẩm phải là số")]
        public int ProductPrice { get; set; }
        [Required(ErrorMessage = "Chưa nhập bước giá sản phẩm")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Bước giá sản phẩm phải là số")]
        public int ProductTickSize { get; set; }
        public string ProductSoldPrice { get; set; }
        public string ProductDes{ get; set; }
        public DateTime ProductDateSold { get; set; }

        [Required(ErrorMessage = "Chưa chọn danh mục .")]
        public int CateID { get; set; }
        [Required(ErrorMessage = "Chưa chọn danh mục cha.")]
        public int CateparentID { get; set; }
        public int UserUploadID { get; set; }
        public int UserBuyID { get; set; }
        public int ProductPointRequired { get; set; }
        public virtual Cate Cate { get; set; }
        public virtual Cate CateParent { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<FileDetail> FileDetails { get; set; }
    }
}