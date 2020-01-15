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
            foreach (var @group in t.Groups)
            {
               // var games=GetGroupGames(group);//TODO: mit games etwas machen!!!
            }
        }


       
    }
}