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
    public class ProductController : Controller
    {
        private AuctionContext db = new AuctionContext();

        // GET: /Product/
        [CheckLogin(Permission=0)]
        public ActionResult Index()
        {
            var User = CurrentContext.GetCurUser();
            var products = db.Products.Where(u=>u.UserID==User.UserID);
            return View(products.ToList());
        }

        // GET: /Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Include(s => s.FileDetails).Include(s=>s.User).Include(p=>p.Favorites).SingleOrDefault(x => x.ProductID == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            if(CurrentContext.IsLogged()==true)
            {
                var User = CurrentContext.GetCurUser();
                var temp = 0;
                foreach(var item in product.Favorites)
                {
                    if(item.UserID==User.UserID)
                    {
                        temp++;
                    }
                }
                if(temp==0)
                {
                    ViewBag.Count = temp;
                }
            }
            return View(product);
        }

        // GET: /Product/Create
        [CheckLogin(Permission = 0)]
        public ActionResult Create()
        {
            ViewBag.CateParentID = new SelectList(db.Cateparents, "CateparentID", "CateparentName");
            return View();
        }

        // POST: /Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckLogin(Permission = 0)]
        public ActionResult Create([Bind(Include = "ProductID,ProductSoldInstantPrice,ProductName,ProductPrice,ProductSoldPrice,ProductDateSold,ProductTickSize,ProductDes,CateID,CateparentID,UserID,ProductPointRequired")] Product product, HttpPostedFileBase fileMainInput, List<HttpPostedFileBase> fileSubInput)
        {
            if(fileMainInput == null)
            {
                ViewBag.Errormsg = "Chưa tải hình ảnh của sản phẩm ";
            }
            if (ModelState.IsValid)
            {
                //handle main file
                product.ProductPicName = Path.GetFileName(fileMainInput.FileName);
                product.ProductPicExtension = Path.GetExtension(product.ProductPicName);
                db.Entry(product).State = EntityState.Added;
                db.SaveChanges();
                var path = Path.Combine(Server.MapPath("~/Img/"),product.ProductID.ToString()+"_"+product.ProductPicName);
                fileMainInput.SaveAs(path);
                foreach (HttpPostedFileBase file in fileSubInput)
                {
                    if (file != null)
                    {
                        FileDetail fileDetail = new FileDetail()
                        {
                            FileName = Path.GetFileName(file.FileName),
                            Extension = Path.GetExtension(Path.GetFileName(file.FileName)),
                            ProductID = product.ProductID
                        };
                        db.Entry(fileDetail).State = EntityState.Added;
                        db.SaveChanges();
                        var pathSub = Path.Combine(Server.MapPath("~/Img/"), fileDetail.ProductID + "_" + fileDetail.FileDetailID + "_" + fileDetail.FileName);
                        file.SaveAs(pathSub);


                    }
                }
                return RedirectToAction("Index");
            }

            ViewBag.CateParentID = new SelectList(db.Cateparents, "CateparentID", "CateparentName");
            return View(product);
        }

        // GET: /Product/Edit/5
             [CheckLogin(Permission = 0)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Include(s => s.FileDetails).SingleOrDefault(x => x.ProductID == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            Cate temp = db.Cates.Where(s => s.CateID == product.CateID).FirstOrDefault();
            Cateparent temp2 = db.Cateparents.Where(s => s.CateparentID == product.CateparentID).FirstOrDefault();
            ViewBag.Cateparent = temp.CateName;
            ViewBag.CateName = temp2.CateparentName;
            return View(product);
        }

        // POST: /Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckLogin(Permission = 0)]
        public ActionResult Edit([Bind(Include = "ProductID,ProductSoldInstantPrice,ProductName,ProductPrice,ProductSoldPrice,ProductDateSold,ProductTickSize,ProductDes,CateID,CateparentID,UserID,ProductPointRequired")] Product product, List<HttpPostedFileBase> fileSubInput)
        {
            if (ModelState.IsValid)
                {
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    Product temp = db.Products.Include(s => s.FileDetails).SingleOrDefault(x => x.ProductID == product.ProductID);
                    var count =  temp.FileDetails.Count();
                    var countSub = 0;
                    foreach (HttpPostedFileBase file in fileSubInput)
                    {
                        countSub++;
                    }
                    if (count+countSub>=4)
                    {
                        ViewBag.Message = "Ảnh phụ không được quá 3 tấm";
                        return View(product);

                    }
                    foreach (HttpPostedFileBase file in fileSubInput)
                    {
                        if (file != null)
                        {
                            FileDetail fileDetail = new FileDetail()
                            {
                                FileName = Path.GetFileName(file.FileName),
                                Extension = Path.GetExtension(Path.GetFileName(file.FileName)),
                                ProductID = product.ProductID
                            };
                            db.Entry(fileDetail).State = EntityState.Added;
                            db.SaveChanges();
                            var pathSub = Path.Combine(Server.MapPath("~/Img/"), fileDetail.ProductID + "_" + fileDetail.FileDetailID + "_" + fileDetail.FileName);
                            file.SaveAs(pathSub);
                         }
                     }
                        return RedirectToAction("Index");
                    }

                 ViewBag.CateParentID = new SelectList(db.Cateparents, "CateparentID", "CateparentName");
                 return View(product);
         }

        // GET: /Product/Delete/5
        [CheckLogin(Permission = 0)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CheckLogin(Permission = 0)]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
        [CheckLogin(Permission = 0)]
        public JsonResult GetCates(int id) 
        {
            List<Cate> Cates = new List<Cate>();
            Cates = (from s in db.Cates
                         where s.CateparentID==id
                         select s).ToList();
            return Json(new SelectList(Cates, "CateID", "CateName"));
        }
        [HttpPost]
        [CheckLogin(Permission = 0)]
        public JsonResult DeleteFile(int id)
        {
            try
            {
                FileDetail fileDetail = db.FileDetails.Where(x=>x.FileDetailID==id).FirstOrDefault();
                if (fileDetail == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }
                //Remove from database
                var pathSub = Path.Combine(Server.MapPath("~/Img/"), fileDetail.ProductID + "_" + fileDetail.FileDetailID + "_" + fileDetail.FileName);
                if (System.IO.File.Exists(pathSub))
                {
                    System.IO.File.Delete(pathSub);
                }
                db.FileDetails.Remove(fileDetail);
                db.SaveChanges();

                //Delete file from the file system
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        [CheckLogin(Permission = 0)]
        public JsonResult Favorite(int userid, int proid)
        {
            try
            {
                var temp = db.Favorites.Where(x => x.UserID == userid).Where(u => u.ProductID == proid).ToList();
                if(temp.Count>0)
                {
                    return Json(new { Result = "Exist" });
                }
                Favorite fav = new Favorite()
                {
                    UserID=userid,
                    ProductID=proid
                };
                db.Entry(fav).State = EntityState.Added;
                db.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        [CheckLogin(Permission = 0)]
        public ActionResult Bidding(int userid, int proid, int price)
        {
            Product product = db.Products.Include(s => s.Biddings).Include(s => s.FileDetails).Include(s => s.User).Include(p => p.Favorites).Include(p => p.Biddings).SingleOrDefault(x => x.ProductID == proid);
            Bidding tempbid = db.Biddings.Where(p => p.PriceBid == product.ProductPrice && p.ProductID == proid).FirstOrDefault();
            var tempprice = product.ProductPrice;
            product.ProductPrice = product.ProductPrice + product.ProductTickSize;
            Bidding bid = new Bidding()
            {
                UserID = userid,
                ProductID = proid,
                PriceBid = price,
                ProductBid = product.ProductPrice,
                DateBid = DateTime.Now.ToString()
            };
            db.Entry(product).State = EntityState.Modified;
            db.Entry(bid).State = EntityState.Added;

            db.SaveChanges();
            //Email for Buyer
            try
            {
                string content = System.IO.File.ReadAllText(Server.MapPath("~/MailTemplate/Bidding/Buyer.html"));
                User tempuser = db.Users.Where(s => s.UserID == userid).FirstOrDefault();
                string toBuyerEmail = tempuser.UserEmail;

                content = content.Replace("{{ProductName}}", product.ProductName);
                content = content.Replace("{{ProductID}}", product.ProductID.ToString());
                content = content.Replace("{{UserName}}", product.User.UserName);
                content = content.Replace("{{ProductPrice}}", bid.PriceBid.ToString());
                content = content.Replace("{{ProductRealPrice}}", product.ProductPrice.ToString());
                if (tempuser == null)
                {

                }
                else
                {
                    MailHelper.SendMail(toBuyerEmail, "Đấu giá sản phẩm - Người mua", content);
                }


                //Email for Seller
                string content2 = System.IO.File.ReadAllText(Server.MapPath("~/MailTemplate/Bidding/Seller.html"));
                User tempuser2 = db.Users.Where(s => s.UserID == product.User.UserID).FirstOrDefault();
                string toSellerEmail = tempuser2.UserEmail;

                content2 = content2.Replace("{{ProductName}}", product.ProductName);
                content2 = content2.Replace("{{ProductID}}", product.ProductID.ToString());
                content2 = content2.Replace("{{UserName}}", product.User.UserName);
                content2 = content2.Replace("{{ProductPrice}}", bid.PriceBid.ToString());
                content2 = content2.Replace("{{UserBuyName}}", bid.User.UserName.ToString());

                MailHelper.SendMail(toSellerEmail, "Đấu giá sản phẩm - Người bán", content2);


                //Email for Buyer - Highest Price
                if (tempbid != null && tempbid.UserID != tempuser.UserID )
                {
                    string content3 = System.IO.File.ReadAllText(Server.MapPath("~/MailTemplate/Bidding/LastHigestBuyer.html"));
                    User tempuser3 = db.Users.Where(s => s.UserID == tempbid.UserID).FirstOrDefault();
                    string toBuyerHighestEmail = tempuser3.UserEmail;
                    content2 = content2.Replace("{{ProductName}}", product.ProductName);
                    content2 = content2.Replace("{{ProductID}}", product.ProductID.ToString());
                    content2 = content2.Replace("{{UserName}}", product.User.UserName);
                    content2 = content2.Replace("{{ProductPrice}}", bid.PriceBid.ToString());
                    content2 = content2.Replace("{{ProductLastPrice}}", tempbid.PriceBid.ToString());
                    MailHelper.SendMail(toBuyerHighestEmail, "Đấu giá sản phẩm - Người giữ giá cao ", content3);
                }

            }
            catch (Exception ex)
            {
                TempData["message"] = ex;
                return RedirectToAction("Index", "Home");
            }


            return PartialView("DetailPartial", product);

            //}
            //catch (Exception ex)
            //{
            //    return Json(new { Result = "ERROR", Message = ex.Message });
            //}
        }
        public PartialViewResult LoadProduct(int? id)
        {
            Product product = db.Products.Include(s => s.Biddings).Include(s => s.FileDetails).Include(s => s.User).Include(p => p.Favorites).Include(p => p.Biddings).SingleOrDefault(x => x.ProductID == id);
            return PartialView("DetailPartial", product);
        }
        [HttpPost]

        public PartialViewResult KickUser(int userid, int proid)
        {
            var bidding = db.Biddings.Where(x => x.ProductID == proid && x.UserID == userid).ToList();
            Product product = db.Products.Include(s => s.Biddings).Include(s => s.FileDetails).Include(s => s.User).Include(p => p.Favorites).Include(p => p.Biddings).SingleOrDefault(x => x.ProductID == proid);
            foreach (var item in bidding)
            {
                item.BidStatus = 1;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }
            int max = db.Biddings.Where(p=>p.BidStatus==0 && p.ProductID==proid).Max(x=>x.ProductBid);
            if(max < product.ProductPrice)
            {
                product.ProductPrice = max;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            }
            string content = System.IO.File.ReadAllText(Server.MapPath("~/MailTemplate/Kick/KickUser.html"));
            User tempuser = db.Users.Where(s => s.UserID == userid).FirstOrDefault();
            string toSeller = tempuser.UserEmail;
            content = content.Replace("{{ProductName}}", product.ProductName);
            content = content.Replace("{{ProductID}}", product.ProductID.ToString());
            content = content.Replace("{{UserName}}", product.User.UserName);
            MailHelper.SendMail(toSeller, "Đấu giá sản phẩm - Người mua", content);
            return PartialView("DetailPartial", product);
        }
        [HttpPost]
        public JsonResult BidOver(int proid)
        {
            Product product = db.Products.Include(s => s.Biddings).Include(s => s.FileDetails).Include(s => s.User).Include(p => p.Favorites).Include(p => p.Biddings).SingleOrDefault(x => x.ProductID == proid);
            if (product.ProductStatus == 3)
            {
                return Json(new { Result = "OK" });
            }
            product.ProductStatus = 3;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            // Handle
            var bidtemp = db.Biddings.Where(p => p.ProductID == proid).ToList();
            if (bidtemp.Count() == 0)
            {
                string content = System.IO.File.ReadAllText(Server.MapPath("~/MailTemplate/BidOver/Seller.html"));
                User tempuser = db.Users.Where(s => s.UserID == product.UserID).FirstOrDefault();
                string toSellerEmail = tempuser.UserEmail;
                content = content.Replace("{{ProductName}}", product.ProductName);
                content = content.Replace("{{ProductID}}", product.ProductID.ToString());
                content = content.Replace("{{UserName}}", product.User.UserName);
                if (tempuser == null)
                {

                }
                else
                {
                    MailHelper.SendMail(toSellerEmail, "Đấu giá sản phẩm - Người mua", content);
                }
            }
            else
            {
                Bidding highest = db.Biddings.Where(s => s.ProductID == proid && product.ProductPrice == s.ProductBid).OrderByDescending(s => s.ProductBid).FirstOrDefault();
                User tempBuyer = db.Users.Where(s => s.UserID == product.UserID).FirstOrDefault();
                User tempSeller = db.Users.Where(s => s.UserID == highest.UserID).FirstOrDefault();
                // Seller
                string content = System.IO.File.ReadAllText(Server.MapPath("~/MailTemplate/BidOver2/Seller.html"));
                string toSellerEmail = tempBuyer.UserEmail;
                content = content.Replace("{{ProductName}}", product.ProductName);
                content = content.Replace("{{ProductID}}", product.ProductID.ToString());
                content = content.Replace("{{UserName}}", product.User.UserName);
                content = content.Replace("{{UserBuyName}}", tempSeller.UserName);
                content = content.Replace("{{ProductPrice}}", product.ProductPrice.ToString());
                content = content.Replace("{{UserSellerEmail}}", product.User.UserEmail);

                //buyer
                string content2 = System.IO.File.ReadAllText(Server.MapPath("~/MailTemplate/BidOver2/Buyer.html"));

                string toBuyerEmail = tempSeller.UserEmail;
                content = content.Replace("{{ProductName}}", product.ProductName);
                content = content.Replace("{{ProductID}}", product.ProductID.ToString());
                content = content.Replace("{{UserName}}", product.User.UserName);
                content = content.Replace("{{UserBuyName}}", tempSeller.UserName);
                content = content.Replace("{{ProductPrice}}", product.ProductPrice.ToString());
                content = content.Replace("{{UserBuyerEmail}}", tempBuyer.UserEmail);
                if (tempSeller == null || tempSeller == null)
                {

                }
                else
                {
                    MailHelper.SendMail(toSellerEmail, "Đấu giá sản phẩm - Người bán", content);
                    MailHelper.SendMail(toBuyerEmail, "Đấu giá sản phẩm - Người mua", content2);

                }
            }


            return Json(new { Result = "OK" });
        }
    }
}
