using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Magisterka.Models;

namespace Magisterka.Controllers
{
    public class GrupasController : Controller
    {
        private PlanContext db = new PlanContext();

        // GET: Grupas
        public ActionResult Index()
        {
            return View(db.Grupy.ToList());
        }

        // GET: Grupas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupa grupa = db.Grupy.Find(id);
            if (grupa == null)
            {
                return HttpNotFound();
            }
            return View(grupa);
        }

        // GET: Grupas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Grupas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GrupaId,Nazwa,Skrot,Liczebnosc")] Grupa grupa)
        {
            if (ModelState.IsValid)
            {
                db.Grupy.Add(grupa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(grupa);
        }

        // GET: Grupas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupa grupa = db.Grupy.Find(id);
            if (grupa == null)
            {
                return HttpNotFound();
            }
            return View(grupa);
        }

        // POST: Grupas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GrupaId,Nazwa,Skrot,Liczebnosc")] Grupa grupa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grupa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(grupa);
        }

        // GET: Grupas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupa grupa = db.Grupy.Find(id);
            if (grupa == null)
            {
                return HttpNotFound();
            }
            return View(grupa);
        }

        // POST: Grupas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Grupa grupa = db.Grupy.Find(id);
            db.Grupy.Remove(grupa);
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
