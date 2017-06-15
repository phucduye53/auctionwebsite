using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using auctionwebsite.Models;
using auctionwebsite.DAL;
using auctionwebsite.Helpers;
using System.IO;

namespace auctionwebsite.Controllers
{
    public class AdminController : Controller
    {
        private AuctionContext db = new AuctionContext();
        [CheckLogin(Permission=1)]
        public ActionResult Dashboard()
        {
            return View();
        }
        [CheckLogin(Permission = 1)]
        public ActionResult Product(string searchString, int? page = 1)
        {
            int n = db.Cates.Count();
            if (searchString != null)
            {
                page = 1;
            }

            int pageSize = 10;
            int nPages = n / pageSize;
            int m = n % pageSize;
            if (m > 0)
            {
                nPages++;

            }
            ViewBag.Pages = nPages;
            ViewBag.CurPage = page;


            var product = from s in db.Products
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                product = product.Where(s => s.ProductName.Contains(searchString)
                                        || s.User.UserName.Contains(searchString));
            }
            else
            {
                product = db.Products.Include(c => c.User).Take(pageSize);
            }
            return View(product);
        }
        [HttpPost]
        [CheckLogin(Permission = 1)]
        public JsonResult AcceptProduct(int id)
        {
            try
            {
                Product product = db.Products.Where(x => x.ProductID == id).FirstOrDefault();
                if (product == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }
                //Change from database
                product.ProductStatus = 1;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        [CheckLogin(Permission = 1)]
        public JsonResult RefuseProduct(int id)
        {
            try
            {
                Product product = db.Products.Where(x => x.ProductID == id).FirstOrDefault();
                if (product == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }
                //Change from database
                product.ProductStatus = 3;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [CheckLogin(Permission = 1)]
        public ActionResult UserEdit(int? id)
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

        // POST: /UserEdit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckLogin(Permission = 1)]
        public ActionResult UserEdit([Bind(Include = "UserID,UserName,UserPassword,ConfirmPassword,Password,UserEmail,UserLevel,UserFirstName,UserLastName,UserDOB,UserAddress,UserCity")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = Helpers.Helpers.EncodePasswordMd5(user.UserPassword);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }
    }
}