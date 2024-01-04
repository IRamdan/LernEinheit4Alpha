using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginScreen
{
    public class Playerstatistics
    {
        public string NickName { get; set; }
        public string TotalGames { get; set; }
        public string Wins { get; set; }
        public string Losses { get; set; }
        public string Draws { get; set; }
        public string UnfinishedGames { get; set; }
        public string Successrate { get; set; }

        public Playerstatistics(string p_NickName, string p_TotalGames, string p_Wins, string p_Losses, string p_Draws, string p_UnfinishedGames, string p_Successrate)
        {
            NickName = p_NickName;
            TotalGames = p_TotalGames;
            Wins = p_Wins;
            Losses = p_Losses;
            Draws = p_Draws;
            UnfinishedGames = p_UnfinishedGames;
            Successrate = p_Successrate;
        }
    }
}
