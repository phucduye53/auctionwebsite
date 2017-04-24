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

namespace auctionwebsite.Controllers.Admin
{
    public class CateController : Controller
    {
        private AuctionContext db = new AuctionContext();

        // GET: /Cate/
        public ActionResult Index(string searchString,int? page=1)
        {
            int n = db.Cates.Count();
            if (searchString != null)
            {
                page = 1;
            }

            int pageSize = 10;
            int nPages = n / pageSize;
            int m = n % pageSize;
            if(m>0)
            {
                nPages++;
            
            }
            ViewBag.Pages = nPages;
            ViewBag.CurPage = page;

            var cates = from s in db.Cates
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
               cates = cates.Where(s => s.CateName.Contains(searchString)
                                       || s.Cateparent.CateparentName.Contains(searchString));
            }
            else
            {
               cates = db.Cates.Include(c => c.Cateparent).OrderBy(c => c.CateID).Skip(10).Take(pageSize);
            }
            return View(cates);
        }

        // GET: /Cate/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cate cate = db.Cates.Find(id);
            if (cate == null)
            {
                return HttpNotFound();
            }
            return View(cate);
        }

        // GET: /Cate/Create
        public ActionResult Create()
        {
            ViewBag.CateparentID = new SelectList(db.Cateparents, "CateparentID", "CateparentName");
            return View();
        }

        // POST: /Cate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CateID,CateName,CateparentID")] Cate cate)
        {
            if (ModelState.IsValid)
            {
                db.Cates.Add(cate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CateparentID = new SelectList(db.Cateparents, "CateparentID", "CateparentName", cate.CateparentID);
            return View(cate);
        }

        // GET: /Cate/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cate cate = db.Cates.Find(id);
            if (cate == null)
            {
                return HttpNotFound();
            }
            ViewBag.CateparentID = new SelectList(db.Cateparents, "CateparentID", "CateparentName", cate.CateparentID);
            return View(cate);
        }

        // POST: /Cate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CateID,CateName,CateparentID")] Cate cate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CateparentID = new SelectList(db.Cateparents, "CateparentID", "CateparentName", cate.CateparentID);
            return View(cate);
        }

        // GET: /Cate/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cate cate = db.Cates.Find(id);
            if (cate == null)
            {
                return HttpNotFound();
            }
            return View(cate);
        }

        // POST: /Cate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cate cate = db.Cates.Find(id);
            db.Cates.Remove(cate);
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
    }
}
