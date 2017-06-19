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
using PagedList;
using auctionwebsite.Helpers;


namespace auctionwebsite.Controllers
{
    public class SearchController : Controller
    {
        private AuctionContext db = new AuctionContext();

        public PartialViewResult LoadSearchBar()
        {
            ViewBag.CateParentID = new SelectList(db.Cateparents, "CateparentID", "CateparentName");
            return PartialView("SearchBarPartial");
        }
        public ActionResult SearchProduct(int? CateparentID, string SearchText, int? CurrentCateparentID, string CurrentSearchText,int? SortOrder,int? page)
        {
            var products = db.Products.Include(s => s.FileDetails).Include(s => s.Biddings).Include(s => s.User).Where(p=>p.ProductStatus==1);
            // handle search text
            if (SearchText != null)
            {
                page = 1;
            }
            else
            {
                SearchText = CurrentSearchText;
            }
            //handle cateid 
            if (CateparentID == null)
            {
                CateparentID = CurrentCateparentID;
            }
            if (!String.IsNullOrEmpty(SearchText))
            {
                products = products.Where(s => s.ProductName.Contains(SearchText));
                ViewBag.CurrentSearchText = SearchText;
            }
            if (CateparentID != null)
            {
                products = products.Where(s => s.CateparentID == CateparentID);
                ViewBag.CurrentCateparentID = CateparentID;
            }
            switch(SortOrder)
            {
                case 0:
                    break;
                case 1:
                    products = products.OrderBy(s => s.ProductDateSold);
                    break;
                case 2:
                    products = products.OrderByDescending(s => s.ProductDateSold);
                    break;
                case 3:
                    products = products.OrderBy(s => s.ProductPrice);
                    break;
                case 4:
                    products = products.OrderByDescending(s => s.ProductPrice);
                    break;
                default:
                    products = products.OrderBy(s => s.ProductName);
                    break;
            }
            ViewBag.CurrentSort = SortOrder;
             int pageSize = 3;
             int pageNumber = (page ?? 1);
             return View(products.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Cate(int? CateID,int? CurrentCatepID, int? SortOrder, int? page)
        {
            var products = db.Products.Include(s => s.FileDetails).Include(s => s.Biddings).Include(s => s.User).Where(p => p.ProductStatus == 1);
            //handle cateid 
            if (CateID == null)
            {
                CateID = CurrentCatepID;
            }
            if (CateID != null)
            {
                products = products.Where(s => s.CateID == CateID);
                ViewBag.CurrentCateparentID = CateID;
            }
            switch (SortOrder)
            {
                case 0:
                    break;
                case 1:
                    products = products.OrderBy(s => s.ProductDateSold);
                    break;
                case 2:
                    products = products.OrderByDescending(s => s.ProductDateSold);
                    break;
                case 3:
                    products = products.OrderBy(s => s.ProductPrice);
                    break;
                case 4:
                    products = products.OrderByDescending(s => s.ProductPrice);
                    break;
                default:
                    products = products.OrderBy(s => s.ProductName);
                    break;
            }
            ViewBag.CurrentSort = SortOrder;
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }

	}
}