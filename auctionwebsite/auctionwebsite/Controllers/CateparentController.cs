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
using PagedList;
using auctionwebsite.Helpers;

namespace auctionwebsite.Controllers.Admin
{
    public class CateparentController : Controller
    {
        private AuctionContext db = new AuctionContext();


        public PartialViewResult List()
        {
            var list = db.Cateparents.Include(c=>c.cates).ToList();
            return PartialView("ListPartial", list);
        }
        // GET: /Cateparent/
                [CheckLogin(Permission = 1)]
        public ActionResult Index()
        {
            return View(db.Cateparents.ToList());
        }

        // GET: /Cateparent/Details/5
                [CheckLogin(Permission = 1)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cateparent cateparent = db.Cateparents.Find(id);
            if (cateparent == null)
            {
                return HttpNotFound();
            }
            return View(cateparent);
        }

        // GET: /Cateparent/Create
                [CheckLogin(Permission = 1)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Cateparent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckLogin(Permission = 1)]
        public ActionResult Create([Bind(Include="CateparentID,CateparentName")] Cateparent cateparent)
        {
            if (ModelState.IsValid)
            {
                db.Cateparents.Add(cateparent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cateparent);
        }

        // GET: /Cateparent/Edit/5
                [CheckLogin(Permission = 1)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cateparent cateparent = db.Cateparents.Find(id);
            if (cateparent == null)
            {
                return HttpNotFound();
            }
            return View(cateparent);
        }

        // POST: /Cateparent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckLogin(Permission = 1)]
        public ActionResult Edit([Bind(Include="CateparentID,CateparentName")] Cateparent cateparent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cateparent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cateparent);
        }

        // GET: /Cateparent/Delete/5
                [CheckLogin(Permission = 1)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cateparent cateparent = db.Cateparents.Find(id);
            if (cateparent == null)
            {
                return HttpNotFound();
            }
            return View(cateparent);
        }

        // POST: /Cateparent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CheckLogin(Permission = 1)]
        public ActionResult DeleteConfirmed(int id)
        {
            Cateparent cateparent = db.Cateparents.Find(id);
            db.Cateparents.Remove(cateparent);
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
