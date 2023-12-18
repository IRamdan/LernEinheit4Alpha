using LoginScreen.TicTacToe;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace LoginScreen
{
    public class TicTacToeGameMechanics
    {
        public class CheckWinParameters
        {
            public List<Player> p_Players { get; set; }
            public Dictionary<string, Button> p_ButtonDictionary { get; set; }
            public Player p_CurrentPlayer { get; set; }
            public string p_ClickedButton { get; set; }
            public int p_SelectedColumn { get; set; }
            public int p_SelectedRow { get; set; }
            public int p_WinCondition { get; set; }
            public int p_GameFieldRows { get; set; }
            public int p_GameFieldColumns { get; set; }
        }


        public Player Winner { get; set; }
        internal GameState GameStatus { get; set; }
        internal int RoundCounter { get; set; } = 0;

        internal static Player GetStartingPlayer(CheckWinParameters p_Parameters)
        {
            Random PlayerRandomizer = new Random();
            return p_Parameters.p_Players[PlayerRandomizer.Next(p_Parameters.p_Players.Count)];
        }

        internal static Player CurrentPlayerDeterminer(CheckWinParameters p_Parameters)
        {
            if (p_Parameters.p_CurrentPlayer == null)
            {
                p_Parameters.p_CurrentPlayer = GetStartingPlayer(p_Parameters);
            }
            else
            {
                int NextPlayerIndex = p_Parameters.p_Players.IndexOf(p_Parameters.p_CurrentPlayer) + 1;
                                                   
                if (NextPlayerIndex > p_Parameters.p_Players.Count - 1)
                    NextPlayerIndex = 0;
                p_Parameters.p_CurrentPlayer = p_Parameters.p_Players[NextPlayerIndex];
            }
            return p_Parameters.p_CurrentPlayer;
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
            string[] SplitedButtonName = p_Parameters.p_ClickedButton.Split('_');
            for (int CheckCounter = Math.Max(0, p_Parameters.p_SelectedColumn - p_Parameters.p_WinCondition + 1); CheckCounter <= Math.Min(p_Parameters.p_GameFieldColumns - 1, p_Parameters.p_SelectedColumn + p_Parameters.p_WinCondition - 1); CheckCounter++)
            {
                string CheckedButtonName = $"{SplitedButtonName[0]}_{SplitedButtonName[1]}_{CheckCounter}";
                p_Parameters.p_ButtonDictionary.TryGetValue(CheckedButtonName, out Button button);

                if (button.Content == p_Parameters.p_CurrentPlayer.Sign)
                {
                    WinConditionCounter++;
                    if (WinConditionCounter == p_Parameters.p_WinCondition)
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
            string[] SplitedButtonName = p_Parameters.p_ClickedButton.Split('_');

            for (int CheckCounter = Math.Max(0, p_Parameters.p_SelectedColumn - p_Parameters.p_WinCondition + 1); CheckCounter <= Math.Min(p_Parameters.p_GameFieldColumns - 1, p_Parameters.p_SelectedColumn + p_Parameters.p_WinCondition - 1); CheckCounter++)
            {
                string CheckedButtonName = $"{SplitedButtonName[0]}_{CheckCounter}_{SplitedButtonName[2]}";
                p_Parameters.p_ButtonDictionary.TryGetValue(CheckedButtonName, out Button button);

                if (button.Content == p_Parameters.p_CurrentPlayer.Sign)
                {
                    WinConditionCounter++;
                    if (WinConditionCounter == p_Parameters.p_WinCondition)
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
            string[] SplitedButtonName = p_Parameters.p_ClickedButton.Split('_');
            int RowCheckCounter = p_Parameters.p_SelectedRow;
            int ColCheckCounter = p_Parameters.p_SelectedColumn;

            while (RowCheckCounter > 0 && ColCheckCounter > 0)
            {
                RowCheckCounter--;
                ColCheckCounter--;
            }

            while (RowCheckCounter < p_Parameters.p_GameFieldRows && ColCheckCounter < p_Parameters.p_GameFieldColumns)
            {
                string CheckedButtonName = $"{SplitedButtonName[0]}_{RowCheckCounter}_{ColCheckCounter}";
                p_Parameters.p_ButtonDictionary.TryGetValue(CheckedButtonName, out Button button);

                if (button.Content == p_Parameters.p_CurrentPlayer.Sign)
                {
                    WinConditionCounter++;
                    if (WinConditionCounter == p_Parameters.p_WinCondition)
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
            string[] SplitedButtonName = p_Parameters.p_ClickedButton.Split('_');
            int RowCheckCounter = p_Parameters.p_SelectedRow;
            int ColCheckCounter = p_Parameters.p_SelectedColumn;

            while (RowCheckCounter < p_Parameters.p_GameFieldRows - 1 && ColCheckCounter > 0)
            {
                RowCheckCounter++;
                ColCheckCounter--;
            }

            while (RowCheckCounter >= 0 && ColCheckCounter < p_Parameters.p_GameFieldColumns)
            {
                string CheckedButtonName = $"{SplitedButtonName[0]}_{RowCheckCounter}_{ColCheckCounter}";
                p_Parameters.p_ButtonDictionary.TryGetValue(CheckedButtonName, out Button CheckedButton);

                if (CheckedButton.Content == p_Parameters.p_CurrentPlayer.Sign)
                {
                    WinConditionCounter++;
                    if (WinConditionCounter == p_Parameters.p_WinCondition)
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
