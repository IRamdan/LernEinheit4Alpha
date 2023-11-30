using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginScreen.ConectFour
{
    class ConnectFourGameMechanics
    {
        private static int PlayerDeterminer = 0;

        public static string GetCurrentPlayer()
        {
            string FirstPlayer = "X";
            string SecondPlayer = "O";
            string CurrentPlayer = "";

            if (PlayerDeterminer == 0)
            {
                CurrentPlayer = FirstPlayer;
                PlayerDeterminer = 1;
            }
            else
            {
                CurrentPlayer = SecondPlayer;
                PlayerDeterminer = 0;
            }

            return CurrentPlayer;
        }

    }
}
