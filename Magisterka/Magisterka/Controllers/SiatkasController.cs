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
    public class SiatkasController : Controller
    {
        private PlanContext db = new PlanContext();

        // GET: Siatkas
        public ActionResult Index()
        {
            List<SiatkaViewModel> siatkiVM = new List<SiatkaViewModel>();
            foreach (var item in db.Siatki.Include(x => x.Grupa).Include(x => x.Prowadzacy).Include(x => x.Przedmiot).ToList())
            {
                
                SiatkaViewModel siatkaVM = new SiatkaViewModel
                {
                    Grupa = item.Grupa.Nazwa,
                    Prowadzacy = item.Prowadzacy.Nazwisko + " " + item.Prowadzacy.Imie,
                    IloscGodzin = item.IloscGodzin,
                    Przedmiot = item.Przedmiot.Nazwa,
                    SiatkaId = item.SiatkaId
                };

                siatkiVM.Add(siatkaVM);
            }
            return View(siatkiVM);
        }

        // GET: Siatkas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Siatka siatka = db.Siatki.Find(id);
            if (siatka == null)
            {
                return HttpNotFound();
            }
            return View(siatka);
        }

        // GET: Siatkas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Siatkas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SiatkaId,IloscGodzin")] Siatka siatka)
        {
            if (ModelState.IsValid)
            {
                db.Siatki.Add(siatka);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(siatka);
        }

        // GET: Siatkas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Siatka siatka = db.Siatki.Find(id);
            if (siatka == null)
            {
                return HttpNotFound();
            }
            return View(siatka);
        }

        // POST: Siatkas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SiatkaId,IloscGodzin")] Siatka siatka)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siatka).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(siatka);
        }

        // GET: Siatkas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Siatka siatka = db.Siatki.Find(id);
            if (siatka == null)
            {
                return HttpNotFound();
            }
            return View(siatka);
        }

        // POST: Siatkas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Siatka siatka = db.Siatki.Find(id);
            db.Siatki.Remove(siatka);
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
