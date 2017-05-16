using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace auctionwebsite.Models
{
    public class User
    {
        public int UserID { get; set; }
        [Required(ErrorMessage = "Chưa nhập tên tài khoản")]
        [StringLength(16, ErrorMessage = "Tên tài khoản chỉ được ít hơn 16 ký tự.")]
        [RegularExpression("^[-0-9A-Za-z_]{5,15}$", ErrorMessage = "Tên tài khoản không hợp lệ.")]
        [System.Web.Mvc.Remote("IsUserExists", "User", ErrorMessage = "Tài khoản đã tồn tại.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Chưa nhập mật khẩu")]
        [NotMapped]
        [StringLength(16,MinimumLength = 4, ErrorMessage = "Mật khẩu chỉ được nhiều hơn 4 và ít hơn 16 ký tự.")]
        public string UserPassword { get; set; }
        [NotMapped]
        [Compare("UserPassword", ErrorMessage = "Mật khẩu nhập lại không đúng")]
        [Required(ErrorMessage = "Chưa nhập lại mật khẩu")]
        public string ConfirmPassword { get; set; }
        public string Password { get; set; }
        [Required]
        public int UserLevel { get; set; }
        [System.Web.Mvc.Remote("IsEmailExits", "User", ErrorMessage = "Email đã tồn tại.")]

        [Required(ErrorMessage = "Chưa nhập Email")]
        [EmailAddress(ErrorMessage = "Nhập không đúng địa chỉ Email")]
        public string UserEmail { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserDOB { get; set; }
        public string UserAddress { get; set; }
        public string UserCity { get; set; }

    }
}