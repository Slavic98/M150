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
            var game = db.Game.Include(g => g.Club).Include(g => g.Club1).Include(g => g.Referee);
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
            if (db.Game.Any())
            {
                return View();
            }

            var tournamentFk = db.Tournment.FirstOrDefault().TournamentPk;
            var groups = db.Group.Include(g => g.Club).Where(g => g.TournamentFk == tournamentFk).ToList();
            var allGames=new List<Game>();
            foreach (var @group in groups)
            {
                allGames.AddRange(GetGroupGames(group));
            }

            db.Game.AddRange(allGames);
            var testu = db.Game.Include(g => g.Club).ToList();
            db.SaveChanges();
            ViewBag.GuestClubFk = new SelectList(db.Club, "ClubPk", "Name");
            ViewBag.HomeClubFk = new SelectList(db.Club, "ClubPk", "Name");
            ViewBag.RefereeFk = new SelectList(db.Referee, "RefereePk", "Referee");
            return View();
        }

        // POST: Games/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GamePk,HomeClubFk,GuestClubFk,HomeResult,GuestResult,DateTime,Played,RefereeFk")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Game.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GuestClubFk = new SelectList(db.Club, "ClubPk", "Name", game.GuestClubFk);
            ViewBag.HomeClubFk = new SelectList(db.Club, "ClubPk", "Name", game.HomeClubFk);
            ViewBag.RefereeFk = new SelectList(db.Referee, "RefereePk", "Referee", game.RefereeFk);
            return View(game);
        }
        public List<Game> GetGroupGames(Group g)
        {
            var clubs = g.Club.ToList();
            if (g.Club.Count != 4)
                throw new Exception($"Die Gruppe {g.Name} hat {g.Club.Count} Vereine, es müssen genau 4 sein!!!");

            //4,1, 1.Tag,
            var game1 = CreateGame(clubs[3].ClubPk, clubs[0].ClubPk, g.Tournment.StartDate.Value, 1);
            //2,3, 1.Tag
            var game2 = CreateGame(clubs[1].ClubPk, clubs[2].ClubPk, g.Tournment.StartDate.Value, 1);

            //1,2, 2.Tag,
            var game3 = CreateGame(clubs[0].ClubPk, clubs[1].ClubPk, g.Tournment.StartDate.Value, 1);
            //3,4, 2.Tag
            var game4 = CreateGame(clubs[2].ClubPk, clubs[3].ClubPk, g.Tournment.StartDate.Value, 1);

            //3,1, 3.Tag,
            var game5 = CreateGame(clubs[2].ClubPk, clubs[0].ClubPk, g.Tournment.StartDate.Value, 1);
            //2,4, 3.Tag
            var game6 = CreateGame(clubs[1].ClubPk, clubs[3].ClubPk, g.Tournment.StartDate.Value, 1);

            //1,3, 4.Tag,
            var game7 = CreateGame(clubs[0].ClubPk, clubs[2].ClubPk, g.Tournment.StartDate.Value, 1);
            //4,2, 4.Tag
            var game8 = CreateGame(clubs[3].ClubPk, clubs[1].ClubPk, g.Tournment.StartDate.Value, 1);

            //3,2, 5.Tag,
            var game9 = CreateGame(clubs[2].ClubPk, clubs[1].ClubPk, g.Tournment.StartDate.Value, 1);
            //1,4, 5.Tag
            var game10 = CreateGame(clubs[0].ClubPk, clubs[3].ClubPk, g.Tournment.StartDate.Value, 1);

            //2,1, 6.Tag,
            var game11 = CreateGame(clubs[1].ClubPk, clubs[0].ClubPk, g.Tournment.StartDate.Value, 1);
            //4,3, 6.Tag
            var game12 = CreateGame(clubs[3].ClubPk, clubs[2].ClubPk, g.Tournment.StartDate.Value, 1);
            var games=new List<Game> { game1, game2, game3, game4, game5, game6, game7, game8, game9, game10, game11, game12 };
            return games;
        }

        private Game CreateGame(int homeClubFk, int guestClubFk, DateTime dt, int RefereeFk)
        {
            return new Game
            {   GamePk = null,
                HomeClubFk = homeClubFk,
                GuestClubFk = guestClubFk,
                DateTime = dt,
                Played = false,
                RefereeFk = RefereeFk
            };
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
            ViewBag.RefereeFk = new SelectList(db.Referee, "RefereePk", "Referee", game.RefereeFk);
            return View(game);
        }

        // POST: Games/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GamePk,HomeClubFk,GuestClubFk,HomeResult,GuestResult,DateTime,Played,RefereeFk")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GuestClubFk = new SelectList(db.Club, "ClubPk", "Name", game.GuestClubFk);
            ViewBag.HomeClubFk = new SelectList(db.Club, "ClubPk", "Name", game.HomeClubFk);
            ViewBag.RefereeFk = new SelectList(db.Referee, "RefereePk", "Referee", game.RefereeFk);
            return View(game);
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
