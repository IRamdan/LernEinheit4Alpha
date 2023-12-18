using LoginScreen.TicTacToe;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LoginScreen
{
    public class TicTacToeGameMechanics
    {
        public class CheckWinParameters
        {
            public Dictionary<string, Button> ButtonDictionary { get; set; }
            public Player CurrentPlayer { get; set; }
            public string ClickedButton { get; set; }
            public int SelectedColumn { get; set; }
            public int SelectedRow { get; set; }
            public int WinCondition { get; set; }
            public int GameFieldRows { get; set; }
            public int GameFieldColumns { get; set; }
        }


        private static int PlayerDeterminer = 0;
        public List<Player> Players = new List<Player>();
        public Player Winner { get; set; }
        internal GameState GameStatus { get; set; }
        internal int RoundCounter { get; set; } = 0;

        internal Player GetStartingPlayer()
        {
            Random PlayerRandomizer = new Random();
            return Players[PlayerRandomizer.Next(Players.Count)];
        }

        internal void CurrentPlayerDeterminer(CheckWinParameters p_Parameters)
        {
            if (p_Parameters.CurrentPlayer == null)
            {
                p_Parameters.CurrentPlayer = GetStartingPlayer();
            }
            else
            {
                int NextPlayerIndex = Players.IndexOf(p_Parameters.CurrentPlayer) + 1;

                if (NextPlayerIndex > Players.Count - 1)
                    NextPlayerIndex = 0;
                p_Parameters.CurrentPlayer = Players[NextPlayerIndex];
            }
        }

        internal static bool CheckForWin(CheckWinParameters p_Parameters)
        {
            if (CheckHorizontal(p_Parameters) || CheckVertical(p_Parameters) || CheckDiagonal(p_Parameters) || CheckCounterDiagonal(p_Parameters))
            {
                return true;
            }
            return false;
        }

        internal static bool CheckHorizontal(CheckWinParameters p_Parameters)
        {
            int WinConditionCounter = 0;
            string[] SplitedButtonName = p_Parameters.ClickedButton.Split('_');
            for (int CheckCounter = Math.Max(0, p_Parameters.SelectedColumn - p_Parameters.WinCondition + 1); CheckCounter <= Math.Min(p_Parameters.GameFieldColumns - 1, p_Parameters.SelectedColumn + p_Parameters.WinCondition - 1); CheckCounter++)
            {
                string CheckedButtonName = $"{SplitedButtonName[0]}_{SplitedButtonName[1]}_{CheckCounter}";
                p_Parameters.ButtonDictionary.TryGetValue(CheckedButtonName, out Button button);

                if (button.Content == p_Parameters.CurrentPlayer.Sign)
                {
                    WinConditionCounter++;
                    if (WinConditionCounter == p_Parameters.WinCondition)
                    {
                        return true;
                    }
                }
                else
                {
                    WinConditionCounter = 0;
                }
            }
            return false;
        }

        internal static bool CheckVertical(CheckWinParameters p_Parameters)
        {
            int WinConditionCounter = 0;
            string[] SplitedButtonName = p_Parameters.ClickedButton.Split('_');

            for (int CheckCounter = Math.Max(0, p_Parameters.SelectedRow - p_Parameters.WinCondition + 1); CheckCounter <= Math.Min(p_Parameters.GameFieldRows - 1, p_Parameters.SelectedRow + p_Parameters.WinCondition - 1); CheckCounter++)
            {
                string CheckedButtonName = $"{SplitedButtonName[0]}_{CheckCounter}_{SplitedButtonName[2]}";
                p_Parameters.ButtonDictionary.TryGetValue(CheckedButtonName, out Button button);

                if (button.Content == p_Parameters.CurrentPlayer.Sign)
                {
                    WinConditionCounter++;
                    if (WinConditionCounter == p_Parameters.WinCondition)
                    {
                        return true;
                    }
                }
                else
                {
                    WinConditionCounter = 0;
                }
            }
            return false;
        }

        internal static bool CheckDiagonal(CheckWinParameters p_Parameters)
        {
            int WinConditionCounter = 0;
            string[] SplitedButtonName = p_Parameters.ClickedButton.Split('_');
            int RowCheckCounter = p_Parameters.SelectedRow;
            int ColCheckCounter = p_Parameters.SelectedColumn;

            while (RowCheckCounter > 0 && ColCheckCounter > 0)
            {
                RowCheckCounter--;
                ColCheckCounter--;
            }

            while (RowCheckCounter < p_Parameters.GameFieldRows && ColCheckCounter < p_Parameters.GameFieldColumns)
            {
                string CheckedButtonName = $"{SplitedButtonName[0]}_{RowCheckCounter}_{ColCheckCounter}";
                p_Parameters.ButtonDictionary.TryGetValue(CheckedButtonName, out Button button);

                if (button.Content == p_Parameters.CurrentPlayer.Sign)
                {
                    WinConditionCounter++;
                    if (WinConditionCounter == p_Parameters.WinCondition)
                    {
                        return true;
                    }
                }
                else
                {
                    WinConditionCounter = 0;
                }

                RowCheckCounter++;
                ColCheckCounter++;
            }
            return false;
        }

        internal static bool CheckCounterDiagonal(CheckWinParameters p_Parameters)
        {
            int WinConditionCounter = 0;
            string[] SplitedButtonName = p_Parameters.ClickedButton.Split('_');
            int RowCheckCounter = p_Parameters.SelectedRow;
            int ColCheckCounter = p_Parameters.SelectedColumn;

            while (RowCheckCounter < p_Parameters.GameFieldRows - 1 && ColCheckCounter > 0)
            {
                RowCheckCounter++;
                ColCheckCounter--;
            }

            while (RowCheckCounter >= 0 && ColCheckCounter < p_Parameters.GameFieldColumns)
            {
                string CheckedButtonName = $"{SplitedButtonName[0]}_{RowCheckCounter}_{ColCheckCounter}";
                p_Parameters.ButtonDictionary.TryGetValue(CheckedButtonName, out Button CheckedButton);

                if (CheckedButton.Content == p_Parameters.CurrentPlayer.Sign)
                {
                    WinConditionCounter++;
                    if (WinConditionCounter == p_Parameters.WinCondition)
                    {
                        return true;
                    }
                }
                else
                {
                    WinConditionCounter = 0;
                }
                RowCheckCounter--;
                ColCheckCounter++;
            }
            return false;
        }

    }
}
