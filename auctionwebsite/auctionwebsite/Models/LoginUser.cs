using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace auctionwebsite.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage = "Chưa nhập tên tài khoản")]
        [StringLength(16, ErrorMessage = "Tên tài khoản chỉ được ít hơn 16 ký tự.")]
        [RegularExpression("^[-0-9A-Za-z_]{5,15}$", ErrorMessage = "Tên tài khoản không hợp lệ.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Chưa nhập mật khẩu")]
        [NotMapped]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "Mật khẩu chỉ được nhiều hơn 4 và ít hơn 16 ký tự.")]
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}