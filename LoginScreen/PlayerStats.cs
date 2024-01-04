using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginScreen
{
    internal class PlayerStats
    {
        public string Nickname { get; set; }
        public int TotalGames { get; set; }
        public int TotalWins { get; set; }
        public int TotalLosses { get; set; }
        public int TotalUnfinishedGames { get; set; }
        public int TotalDraws { get; set; }
        public decimal SuccessRate { get; set; }

    }
}
