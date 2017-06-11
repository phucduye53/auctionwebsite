
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using auctionwebsite.Models;
using auctionwebsite.DAL;
using System.Web.Security;
using auctionwebsite.Helpers;
using System;
using System.Web;
namespace auctionwebsite.Helpers
{
    public class CurrentContext
    {
        public static bool IsLogged()
        {
            var flag= HttpContext.Current.Session["isLogin"] ;
            if (flag==null||(int)flag==0)
            {
                if (HttpContext.Current.Request.Cookies["userID"] != null)
                {
                    AuctionContext db = new AuctionContext();
                    int userID = Convert.ToInt32(HttpContext.Current.Request.Cookies["userID"].Value);
                    var user = db.Users.Where(u => u.UserID == userID).FirstOrDefault();
                    HttpContext.Current.Session["isLogin"] = 1;
                    HttpContext.Current.Session["user"] = user;
                    return true;
                }
                return false;
            }
            return true;
        }
        public static User GetCurUser()
        {
            return (User)HttpContext.Current.Session["user"];
        }
        public static void Destroy()
        {
            HttpContext.Current.Session["isLogin"] = 0;
            HttpContext.Current.Session["user"] = null;
            HttpContext.Current.Response.Cookies["userID"].Expires = DateTime.Now.AddDays(-1);
        }
    }
}