using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Turnierverwaltung.Models;

namespace Turnierverwaltung.Controllers
{
    public class GroupsController : Controller
    {
        private SoccerEntities db = new SoccerEntities();

        // GET: Groups
    

        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var groups = db.Groups.Where(x => x.TournamentFk == id).ToList();
            foreach (var group in @groups)
            {
                foreach (var gClub in @group.Clubs)
                {
                    var homeGames = gClub.Games.Where(g => g.GameTypeEnum == GameType.Quali).ToList();
                    var guestGames = gClub.Games1.Where(g => g.GameTypeEnum == GameType.Quali).ToList();
                    gClub.GamesCount = homeGames.Count + guestGames.Count;
                    gClub.Wins = homeGames.Count(g => g.HomeResult > g.GuestResult) + guestGames.Count(g => g.HomeResult < g.GuestResult);
                    gClub.Draws = homeGames.Count(g => g.HomeResult == g.GuestResult) + guestGames.Count(g => g.HomeResult == g.GuestResult);
                    gClub.Loses = gClub.GamesCount - gClub.Wins - gClub.Draws;
                    gClub.Goals = homeGames.Sum(g => g.HomeResult ?? 0) + guestGames.Sum(g => g.GuestResult ?? 0);
                    gClub.RecievedGoals = homeGames.Sum(g => g.GuestResult ?? 0) + guestGames.Sum(g => g.HomeResult ?? 0);
                    gClub.GoalsDifference = gClub.Goals - gClub.RecievedGoals;
                    gClub.Points = gClub.Wins * 3 + gClub.Draws * 1;
                }
            }
            return View(groups);
        }

        // GET: Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            ViewBag.TournamentFk = new SelectList(db.Tournments, "TournamentPk", "Description");
            return View();
        }

        // POST: Groups/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupPk,Name,TournamentFk")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Groups.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TournamentFk = new SelectList(db.Tournments, "TournamentPk", "Description", group.TournamentFk);
            return View(group);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            ViewBag.TournamentFk = new SelectList(db.Tournments, "TournamentPk", "Description", group.TournamentFk);
            return View(group);
        }

        // POST: Groups/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupPk,Name,TournamentFk")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TournamentFk = new SelectList(db.Tournments, "TournamentPk", "Description", group.TournamentFk);
            return View(group);
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Group group = db.Groups.Find(id);
            db.Groups.Remove(group);
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
