using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using Microsoft.Owin.Security.Provider;

namespace Turnierverwaltung.Models
{
    public class TournamentViewModel
    {
        public void PrepareGroupPhase(Tournment t)
        {
            foreach (var @group in t.Group)
            {
                var games=GetGroupGames(group);//TODO: mit games etwas machen!!!
            }
        }
        /// <summary>
        /// Achtelfinale
        /// </summary>
        public void PrepareLastSixteen(Tournment t)
        {
            foreach (var @group in t.Group)
            {
                
            }
        }

        public int GetQualificationPointsOfClub(Club club, Tournment tournment)
        {
            List<Game> games =new List<Game>();//TODO: how to get games?
            var guestGamesres = games.Where(g => g.GuestClubFk == club.ClubPk).Sum(GetGuestPointsOfTheGame);
            var homeGamesres = games.Where(g => g.GuestClubFk == club.ClubPk).Sum(GetGuestPointsOfTheGame);


            return guestGamesres+homeGamesres;
        }
        public List<Game> GetGroupGames(Group g)
        {
            if (g.Club.Count != 4)
                throw new Exception($"Die Gruppe {g.Name} hat {g.Club.Count} Vereine, es müssen genau 4 sein!!!");

            //4,1, 1.Tag,
            var game1 = CreateGame(g.Club.First().ClubPk, g.Club.Last().ClubPk, new DateTime(), new User().UserPk);
            //2,3, 1.Tag
            var game2 = CreateGame(g.Club.First().ClubPk, g.Club.Last().ClubPk, new DateTime(), new User().UserPk);
            
            //1,2, 2.Tag,
            var game3 = CreateGame(g.Club.First().ClubPk, g.Club.Last().ClubPk, new DateTime(), new User().UserPk);
            //3,4, 2.Tag
            var game4 = CreateGame(g.Club.First().ClubPk, g.Club.Last().ClubPk, new DateTime(), new User().UserPk);

            //3,1, 3.Tag,
            var game5 = CreateGame(g.Club.First().ClubPk, g.Club.Last().ClubPk, new DateTime(), new User().UserPk);
            //2,4, 3.Tag
            var game6 = CreateGame(g.Club.First().ClubPk, g.Club.Last().ClubPk, new DateTime(), new User().UserPk);
            
            //1,3, 4.Tag,
            var game7 = CreateGame(g.Club.First().ClubPk, g.Club.Last().ClubPk, new DateTime(), new User().UserPk);
            //4,2, 4.Tag
            var game8 = CreateGame(g.Club.First().ClubPk, g.Club.Last().ClubPk, new DateTime(), new User().UserPk);

            //3,2, 5.Tag,
            var game9 = CreateGame(g.Club.First().ClubPk, g.Club.Last().ClubPk, new DateTime(), new User().UserPk);
            //1,4, 5.Tag
            var game10 = CreateGame(g.Club.First().ClubPk, g.Club.Last().ClubPk, new DateTime(), new User().UserPk);

            //2,1, 6.Tag,
            var game11 = CreateGame(g.Club.First().ClubPk, g.Club.Last().ClubPk, new DateTime(), new User().UserPk);
            //4,3, 6.Tag
            var game12 = CreateGame(g.Club.First().ClubPk, g.Club.Last().ClubPk, new DateTime(), new User().UserPk);

            return new List<Game> {game1, game2, game3, game4, game5, game6, game7, game8, game9, game10, game11, game12};
        }

        private Game CreateGame(int homeClubFk, int guestClubFk, DateTime dt, int userFk)
        {
            return new Game
            {
                HomeClubFk = homeClubFk,
                GuestClubFk = guestClubFk,
                DateTime =dt,
                Played = false,
                UserFk = userFk
            };
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
    }
}