using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Turnierverwaltung;

namespace Turnierverwaltung.Models
{
    public partial class Club
    {
        public int GamesCount { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Loses { get; set; }
        public int Goals { get; set; }
        public int RecievedGoals { get; set; }
        public int GoalsDifference { get; set; }
        public int Points { get; set; }
    }
}