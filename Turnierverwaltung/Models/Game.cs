//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Turnierverwaltung.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Game
    {
        public int GamePk { get; set; }
        public int HomeClubFk { get; set; }
        public int GuestClubFk { get; set; }
        public Nullable<int> HomeResult { get; set; }
        public Nullable<int> GuestResult { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateTime { get; set; }
        public bool Played { get; set; }
        public Nullable<int> RefereeFk { get; set; }

        public string GameType
        {
            get => GameTypeEnum.ToString();
            set
            {
                var gameTypeEnum = GameTypeEnum;
                Enum.TryParse(value, out gameTypeEnum);
                GameTypeEnum = gameTypeEnum;
            }
        }

        public virtual Club Club { get; set; }
        public virtual Club Club1 { get; set; }
        public virtual Referee Referee { get; set; }
    }
}
