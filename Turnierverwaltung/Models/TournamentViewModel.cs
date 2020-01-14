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
               // var games=GetGroupGames(group);//TODO: mit games etwas machen!!!
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