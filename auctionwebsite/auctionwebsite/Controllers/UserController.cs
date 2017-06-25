﻿using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using auctionwebsite.Models;
using auctionwebsite.DAL;
using System.Web.Security;
using auctionwebsite.Helpers;
using System;
using CaptchaMvc.HtmlHelpers;  


namespace auctionwebsite.Controllers
{
    public class UserController : Controller
    {
        private AuctionContext db = new AuctionContext();

        // GET: /User/
        [CheckLogin(Permission = 2)]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: /User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: /User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="UserID,UserName,UserPassword,ConfirmPassword,Password,UserLevel,UserEmail,UserFirstName,UserLastName,UserDOB,UserAddress,UserCity")] User user)
        {
            if (this.IsCaptchaValid("Captcha is not valid"))
            {
                if (ModelState.IsValid)
                {
                    user.UserCash = 1000;
                    user.Password = Helpers.Helpers.EncodePasswordMd5(user.UserPassword);
                    db.Users.Add(user);
                    db.SaveChanges();
                    TempData["message"] = "Đăng ký thành công";
                    return RedirectToAction("Login");
                }
            }
            ViewBag.ErrMessage = "Mã captcha không đúng";
            return View(user);
        }

        // GET: /User/Edit/5
        [CheckLogin(Permission = 0)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckLogin(Permission = 0)]
        public ActionResult Edit([Bind(Include = "UserID,OldPassword,UserName,UserPassword,ConfirmPassword,Password,UserEmail,UserLevel,UserFirstName,UserLastName,UserDOB,UserAddress,UserCity")] User user)
        {
            if (ModelState.IsValid)
            {
                if (Helpers.Helpers.EncodePasswordMd5(user.OldPassword)!=user.Password)
                {
                    ViewBag.Errormsg = "Mật khẩu cũ nhập không đúng ";
                    return View(user);
                }
                user.Password = Helpers.Helpers.EncodePasswordMd5(user.UserPassword);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: /User/Delete/5
        [CheckLogin(Permission = 2)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CheckLogin(Permission = 2)]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public JsonResult IsUserExists(string UserName)
        {
            //check if any of the UserName matches the UserName specified in the Parameter using the ANY extension method.  
            return Json(!db.Users.Any(x => x.UserName == UserName), JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsEmailExits(string UserEmail)
        {
            //check if any of the Email matches the Email specified in the Parameter using the ANY extension method.  
            return Json(!db.Users.Any(x => x.UserEmail == UserEmail), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login([Bind(Include = "Username,Password,Remember")]LoginUser model)
        {
            string password = Helpers.Helpers.EncodePasswordMd5(model.Password);
            var user = db.Users.Where(u => u.UserName == model.Username && u.Password == password ).FirstOrDefault();
            if(user!=null)
            {
                if (model.Remember)
                {
                    Response.Cookies["userID"].Value = user.UserID.ToString();
                    Response.Cookies["userID"].Expires = DateTime.Now.AddDays(3);
                }
                Session["isLogin"] = 1;
                Session["user"] = user;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Errormsg = "Tài khoản hoặc mật khẩu không đúng";
                return View();
            }
        }
        [CheckLogin]
        public ActionResult Logout()
        {
            CurrentContext.Destroy();
            return RedirectToAction("Index", "Home");
        }
         [CheckLogin]
         public ActionResult Summary()
         {
             return View();
         }
         [CheckLogin]
         public ActionResult FollowList()
         {
             var User = CurrentContext.GetCurUser();
             var products = db.Products.Where(s => s.Favorites.Any(f => f.UserID == User.UserID));
             //var products = db.Products.Include(p=>p.Favorites).Where(p=>User.UserID.Equals(p.UserID));
             return View(products.ToList());
         }
        [HttpPost] 
        public JsonResult DeleteFavProduct(int id)
         {
             try
             {
                 var User = CurrentContext.GetCurUser();
                 Favorite temp = db.Favorites.Where(x => x.ProductID == id && x.UserID==User.UserID).FirstOrDefault();
                 if (temp == null)
                 {
                     Response.StatusCode = (int)HttpStatusCode.NotFound;
                     return Json(new { Result = "Error" });
                 }
                 db.Favorites.Remove(temp);
                 db.SaveChanges();
                 return Json(new { Result = "OK" });
             }
             catch (Exception ex)
             {
                 return Json(new { Result = "ERROR", Message = ex.Message });
             }
         }
        [CheckLogin]
        public ActionResult BiddingList()
        {
            var User = CurrentContext.GetCurUser();
            var products = db.Biddings.GroupBy(p => p.UserID).Select(t => t.OrderByDescending(p => p.ProductBid).FirstOrDefault()).Where(p=>p.UserID==User.UserID);
            //var products = db.Products.Include(p=>p.Favorites).Where(p=>User.UserID.Equals(p.UserID));
            return View(products.ToList());
        }
        [HttpPost] 
        public ActionResult RateLikeUser(int userid,int targetid,string text,int proid)
        {
            try
            {
                var User = CurrentContext.GetCurUser();
                Rate temp = db.Rates.Where(p => p.UserID == userid && p.UserTargetID == targetid).FirstOrDefault();
                Product product = db.Products.Include(s => s.Biddings).Include(s => s.FileDetails).Include(s => s.User).Include(p => p.Favorites).Include(p => p.Biddings).SingleOrDefault(x => x.ProductID == proid);
                if (temp != null)
                {
                    if(temp.RateStatus==0)
                    {
                        return Json(new { Result = "Exist" });
                    }
                    temp.RateStatus = 0;
                    temp.RateComment = text;
                    temp.Date = DateTime.Now.ToString();
                    db.Entry(temp).State = EntityState.Modified;
                    db.SaveChanges();
                    return PartialView("~/Views/Product/DetailPartial", product);
                }
                else
                {
                    Rate rate = new Rate()
                    {
                        UserID = userid,
                        UserTargetID = targetid,
                        RateStatus = 0,
                        RateComment = text,
                        Date = DateTime.Now.ToString()
                    };
                    db.Rates.Add(rate);
                    db.SaveChanges();
                    return PartialView("~/Views/Product/DetailPartial", product);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        public ActionResult RateDisLikeUser(int userid, int targetid, string text,int proid)
        {
            try
            {
                var User = CurrentContext.GetCurUser();
                Product product = db.Products.Include(s => s.Biddings).Include(s => s.FileDetails).Include(s => s.User).Include(p => p.Favorites).Include(p => p.Biddings).SingleOrDefault(x => x.ProductID == proid);
                Rate temp = db.Rates.Where(p => p.UserID == userid && p.UserTargetID == targetid).FirstOrDefault();
                if (temp != null)
                {
                    if (temp.RateStatus == 1)
                    {
                        return Json(new { Result = "Exist" });
                    }
                    temp.RateStatus = 1;
                    temp.RateComment = text;
                    temp.Date = DateTime.Now.ToString();
                    db.Entry(temp).State = EntityState.Modified;
                    db.SaveChanges();
                    return PartialView("~/Views/Product/DetailPartial", product);
                }
                else
                {
                    Rate rate = new Rate()
                    {
                        UserID = userid,
                        UserTargetID = targetid,
                        RateStatus = 1,
                        RateComment = text,
                        Date = DateTime.Now.ToString()
                    };
                    db.Rates.Add(rate);
                    db.SaveChanges();
                    return PartialView("~/Views/Product/DetailPartial", product);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

    }
}
    