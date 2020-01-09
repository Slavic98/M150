using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Turnierverwaltung;

namespace Turnierverwaltung.Controllers
{
    public class TournmentsController : Controller
    {
        private SoccerEntities db = new SoccerEntities();

        // GET: Tournments
        public ActionResult Index()
        {
            return View(db.Tournment.ToList());
        }

        // GET: Tournments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tournment tournment = db.Tournment.Find(id);
            if (tournment == null)
            {
                return HttpNotFound();
            }
            return View(tournment);
        }

        // GET: Tournments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tournments/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TournamentPk,Description,StartDate,EndDate")] Tournment tournment)
        {
            if (ModelState.IsValid)
            {
                db.Tournment.Add(tournment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tournment);
        }

        // GET: Tournments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tournment tournment = db.Tournment.Find(id);
            if (tournment == null)
            {
                return HttpNotFound();
            }
            return View(tournment);
        }

        // POST: Tournments/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TournamentPk,Description,StartDate,EndDate")] Tournment tournment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tournment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tournment);
        }

        // GET: Tournments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tournment tournment = db.Tournment.Find(id);
            if (tournment == null)
            {
                return HttpNotFound();
            }
            return View(tournment);
        }

        // POST: Tournments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tournment tournment = db.Tournment.Find(id);
            db.Tournment.Remove(tournment);
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
