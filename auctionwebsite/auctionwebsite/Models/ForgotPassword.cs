using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace auctionwebsite.Models
{
    public class ForgotPassword
    {
        [Required(ErrorMessage = "Chưa nhập Email")]
        [EmailAddress(ErrorMessage = "Nhập không đúng địa chỉ Email")]
        [System.Web.Mvc.Remote("IsEmailNotExits", "User", ErrorMessage = "Email không tồn tại.")]
        public string UserEmail { get; set; }
    }
}