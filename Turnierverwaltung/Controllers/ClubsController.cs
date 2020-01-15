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
    public class ClubsController : Controller
    {
        private SoccerEntities db = new SoccerEntities();

        //public int? SelectedTournamentFk { get; set; } 
        // GET: Clubs
        public ActionResult Index()
        {
            var tournament = GetTournments();
                int? selTournamentFk = ViewBag.SelectedTournamentFk ?? Convert.ToInt32(tournament.FirstOrDefault()?.Value ?? "0");
                ViewBag.SelectedTournamentFk = selTournamentFk;
           var club = db.Clubs.Where(c=>c.Group.TournamentFk== selTournamentFk).OrderBy(a => a.Name);
           ViewBag.TournamentsList = tournament;
            return View(club.ToList());
        }

        private SelectList GetTournments()
        {
            var selectListItems =new List<SelectListItem>();

            foreach (var t in db.Tournments)
            {
                selectListItems.Add( new SelectListItem
                {
                    Value = t.TournamentPk.ToString(),
                    Text = $"{t.Description} {t.StartDate:d}- {t.EndDate:d}"
                });
            }
            return new SelectList(selectListItems, "Value", "Text");
        }

        // GET: Clubs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Club club = db.Clubs.Find(id);
            if (club == null)
            {
                return HttpNotFound();
            }
            return View(club);
        }

        // GET: Clubs/Create
        public ActionResult Create()
        {
            ViewBag.GroupFk = new SelectList(db.Groups, "GroupPk", "Name");
            return View();
        }

        // POST: Clubs/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClubPk,Name,GroupFk")] Club club)
        {
            if (ModelState.IsValid)
            {
                db.Clubs.Add(club);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupFk = new SelectList(db.Groups, "GroupPk", "Name", club.GroupFk);
            return View(club);
        }

        // GET: Clubs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Club club = db.Clubs.Find(id);
            if (club == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupFk = new SelectList(db.Groups, "GroupPk", "Name", club.GroupFk);
            return View(club);
        }

        // POST: Clubs/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClubPk,Name,GroupFk")] Club club)
        {
            if (ModelState.IsValid)
            {
                db.Entry(club).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupFk = new SelectList(db.Groups, "GroupPk", "Name", club.GroupFk);
            return View(club);
        }

        // GET: Clubs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Club club = db.Clubs.Find(id);
            if (club == null)
            {
                return HttpNotFound();
            }
            return View(club);
        }

        // POST: Clubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Club club = db.Clubs.Find(id);
            db.Clubs.Remove(club);
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
