using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Turnierverwaltung.Models;

namespace Turnierverwaltung.Controllers
{
    public class GamesController : Controller
    {
        private SoccerEntities db = new SoccerEntities();

        // GET: Games
        public ActionResult Index()
        {
            var game = db.Games.Include(g => g.Club).Include(g => g.Club1).Include(g => g.Referee);
            return View(game.ToList());
        }

        // GET: Games/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            if (db.Games.Any())
            {
                return RedirectToAction("Index");
            }

            var tournamentFk = db.Tournments.FirstOrDefault().TournamentPk; //TODO:
            var groups = db.Groups.Include(g => g.Clubs).Where(g => g.TournamentFk == tournamentFk).ToList();
            var allGames=new List<Game>();
            foreach (var @group in groups)
            {
                allGames.AddRange(GetGroupGames(group));
            }

            db.Games.AddRange(allGames);
            var testu = db.Games.Include(g => g.Club).ToList();
            db.SaveChanges();
            ViewBag.GuestClubFk = new SelectList(db.Clubs, "ClubPk", "Name");
            ViewBag.HomeClubFk = new SelectList(db.Clubs, "ClubPk", "Name");
            ViewBag.RefereeFk = new SelectList(db.Referees, "RefereePk", "Referee");
            return RedirectToAction("Index");
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
                db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GuestClubFk = new SelectList(db.Clubs, "ClubPk", "Name", game.GuestClubFk);
            ViewBag.HomeClubFk = new SelectList(db.Clubs, "ClubPk", "Name", game.HomeClubFk);
            ViewBag.RefereeFk = new SelectList(db.Referees, "RefereePk", "Referee", game.RefereeFk);
            return RedirectToAction("Index");
        }
        public List<Game> GetGroupGames(Group g)
        {
            var clubs = g.Clubs.ToList();
            if (g.Clubs.Count != 4)
                throw new Exception($"Die Gruppe {g.Name} hat {g.Clubs.Count} Vereine, es müssen genau 4 sein!!!");

            //4,1, 1.Tag,
            var game1 = CreateQualiGame(clubs[3].ClubPk, clubs[0].ClubPk, g.Tournment.StartDate.Value, 1);
            //2,3, 1.Tag
            var game2 = CreateQualiGame(clubs[1].ClubPk, clubs[2].ClubPk, g.Tournment.StartDate.Value, 1);

            //1,2, 2.Tag,
            var game3 = CreateQualiGame(clubs[0].ClubPk, clubs[1].ClubPk, g.Tournment.StartDate.Value.AddDays(2), 1);
            //3,4, 2.Tag
            var game4 = CreateQualiGame(clubs[2].ClubPk, clubs[3].ClubPk, g.Tournment.StartDate.Value.AddDays(2), 1);

            //3,1, 3.Tag,
            var game5 = CreateQualiGame(clubs[2].ClubPk, clubs[0].ClubPk, g.Tournment.StartDate.Value.AddDays(4), 1);
            //2,4, 3.Tag
            var game6 = CreateQualiGame(clubs[1].ClubPk, clubs[3].ClubPk, g.Tournment.StartDate.Value.AddDays(4), 1);

            //1,3, 4.Tag,
            var game7 = CreateQualiGame(clubs[0].ClubPk, clubs[2].ClubPk, g.Tournment.StartDate.Value.AddDays(6), 1);
            //4,2, 4.Tag
            var game8 = CreateQualiGame(clubs[3].ClubPk, clubs[1].ClubPk, g.Tournment.StartDate.Value.AddDays(6), 1);

            //3,2, 5.Tag,
            var game9 = CreateQualiGame(clubs[2].ClubPk, clubs[1].ClubPk, g.Tournment.StartDate.Value.AddDays(8), 1);
            //1,4, 5.Tag
            var game10 = CreateQualiGame(clubs[0].ClubPk, clubs[3].ClubPk, g.Tournment.StartDate.Value.AddDays(8), 1);

            //2,1, 6.Tag,
            var game11 = CreateQualiGame(clubs[1].ClubPk, clubs[0].ClubPk, g.Tournment.StartDate.Value.AddDays(10), 1);
            //4,3, 6.Tag
            var game12 = CreateQualiGame(clubs[3].ClubPk, clubs[2].ClubPk, g.Tournment.StartDate.Value.AddDays(10), 1);
            var games=new List<Game> { game1, game2, game3, game4, game5, game6, game7, game8, game9, game10, game11, game12 };
            return games;
        }

        private Game CreateQualiGame(int homeClubFk, int guestClubFk, DateTime dt, int RefereeFk)
        {
            return new Game
            {   //GamePk = null,
                HomeClubFk = homeClubFk,
                GuestClubFk = guestClubFk,
                DateTime = dt,
                Played = false,
                RefereeFk = RefereeFk,
                GameTypeEnum = GameType.Quali
            };
        }

        public int GetQualificationPointsOfClub(Club club, Tournment tournment)
        {
            List<Game> games = new List<Game>();//TODO: how to get games?
            var guestGamesres = games.Where(g => g.GuestClubFk == club.ClubPk).Sum(GetGuestPointsOfTheGame);
            var homeGamesres = games.Where(g => g.GuestClubFk == club.ClubPk).Sum(GetGuestPointsOfTheGame);


            return guestGamesres + homeGamesres;
        }

        //Game
        int GetHomePointsOfTheGame(Game game)
        {
            if (game.Played)
            {
                var res = game.HomeResult.Value.CompareTo(game.GuestResult.Value);

                return res > 0 ? 3 : res == 0 ? 1 : 0;
            }

            return -99;
        }
        int GetGuestPointsOfTheGame(Game game)
        {
            if (game.Played)
            {
                var res = game.HomeResult.Value.CompareTo(game.GuestResult.Value);

                return res > 0 ? 0 : res == 0 ? 1 : 3;
            }

            return 0;
        }

        /// <summary>
        /// Achtelfinale
        /// </summary>
        public List<Club> GetLastSixteen(Tournment t)
        {
            var clubs = new List<Club>();
            foreach (var @group in t.Groups)
            {
                clubs.AddRange(group.Clubs.OrderByDescending(c => c.Points).Take(2));
            }
            return clubs;
        }

        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.GuestClubFk = new SelectList(db.Clubs, "ClubPk", "Name", game.GuestClubFk);
            ViewBag.HomeClubFk = new SelectList(db.Clubs, "ClubPk", "Name", game.HomeClubFk);
            ViewBag.RefereeFk = new SelectList(db.Referees, "RefereePk", "RefereePk", game.RefereeFk);
            return View(game);
        }

        // POST: Games/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GamePk,HomeResult,GuestResult,Played,RefereeFk")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;

                var objectContext = ((IObjectContextAdapter)db).ObjectContext;
                var modifiedEntities = objectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Modified);
                foreach (var entry in modifiedEntities.Where(entity => entity.Entity.GetType() == typeof(Game)))
                {
                    // You need to give Foreign Key Property name
                    // instead of Navigation Property name
                    entry.RejectPropertyChanges("HomeClubFk");
                    entry.RejectPropertyChanges("GuestClubFk");
                    entry.RejectPropertyChanges("DateTime");

                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GuestClubFk = new SelectList(db.Clubs, "ClubPk", "Name", game.GuestClubFk);
            ViewBag.HomeClubFk = new SelectList(db.Clubs, "ClubPk", "Name", game.HomeClubFk);
            ViewBag.RefereeFk = new SelectList(db.Referees, "RefereePk", "Referee", game.RefereeFk);
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
