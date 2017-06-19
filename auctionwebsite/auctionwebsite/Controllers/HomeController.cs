﻿using System;
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

namespace auctionwebsite.Controllers.Admin
{
    public class HomeController : Controller
    {
        private AuctionContext db = new AuctionContext();

        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult LoadHomePriceProduct()
        {
            var product = db.Products.Include(s=>s.FileDetails).Include(s => s.User).Where(p=>p.ProductStatus==1).OrderByDescending(p=>p.ProductPrice).Take(4);
            return PartialView("PriceHomePartial", product);
        }
        public PartialViewResult LoadHomeBiddingProduct()
        {
            var product = db.Products.Include(s => s.FileDetails).Include(s => s.User).Include(s => s.Biddings).Where(p => p.ProductStatus == 1).OrderByDescending(p=>p.Biddings.Count()).Take(4);
            if(product.Count()==0)
            {
                product = db.Products.Include(s => s.FileDetails).Include(s => s.User).Where(p => p.ProductStatus == 1).OrderByDescending(p => p.ProductPrice).Take(4);
            }
            return PartialView("BiddingHomePartial", product);
        }
        public PartialViewResult LoadHomeDateProduct()
        {
            var product = db.Products.Include(s => s.FileDetails).Include(s => s.User).Include(s => s.Biddings).Where(p => p.ProductStatus == 1).OrderBy(p =>p.ProductDateSold).Take(4);
            return PartialView("DateHomePartial", product);
        }
        [HttpPost]
        public JsonResult BidOver(int proid)
        {
            Product product = db.Products.Include(s => s.Biddings).Include(s => s.FileDetails).Include(s => s.User).Include(p => p.Favorites).Include(p => p.Biddings).SingleOrDefault(x => x.ProductID == proid);
            if(product.ProductStatus==3)
            {
                return Json(new { Result = "OK" });
            }
            product.ProductStatus = 3;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new { Result = "OK" });
        }

	}
}