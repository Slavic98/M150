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
    public class GamesController : Controller
    {
        private SoccerEntities db = new SoccerEntities();

        // GET: Games
        public ActionResult Index()
        {
            var game = db.Game.Include(g => g.Club).Include(g => g.Club1).Include(g => g.User);
            return View(game.ToList());
        }

        // GET: Games/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Game.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            ViewBag.GuestClubFk = new SelectList(db.Club, "ClubPk", "Name");
            ViewBag.HomeClubFk = new SelectList(db.Club, "ClubPk", "Name");
            ViewBag.UserFk = new SelectList(db.User, "UserPk", "Login");
            return View();
        }

        // POST: Games/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GamePk,HomeClubFk,GuestClubFk,HomeResult,GuestResult,DateTime,Played,UserFk")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Game.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GuestClubFk = new SelectList(db.Club, "ClubPk", "Name", game.GuestClubFk);
            ViewBag.HomeClubFk = new SelectList(db.Club, "ClubPk", "Name", game.HomeClubFk);
            ViewBag.UserFk = new SelectList(db.User, "UserPk", "Login", game.UserFk);
            return View(game);
        }

        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Game.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.GuestClubFk = new SelectList(db.Club, "ClubPk", "Name", game.GuestClubFk);
            ViewBag.HomeClubFk = new SelectList(db.Club, "ClubPk", "Name", game.HomeClubFk);
            ViewBag.UserFk = new SelectList(db.User, "UserPk", "Login", game.UserFk);
            return View(game);
        }

        // POST: Games/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GamePk,HomeClubFk,GuestClubFk,HomeResult,GuestResult,DateTime,Played,UserFk")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GuestClubFk = new SelectList(db.Club, "ClubPk", "Name", game.GuestClubFk);
            ViewBag.HomeClubFk = new SelectList(db.Club, "ClubPk", "Name", game.HomeClubFk);
            ViewBag.UserFk = new SelectList(db.User, "UserPk", "Login", game.UserFk);
            return View(game);
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Game.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Game.Find(id);
            db.Game.Remove(game);
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
