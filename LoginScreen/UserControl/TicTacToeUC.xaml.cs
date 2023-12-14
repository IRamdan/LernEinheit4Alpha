using LoginScreen.TicTacToe;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoginScreen
{
    public partial class TicTacToeUC : UserControl
    {
        internal List<Player> Players { get; set; }
        internal Dictionary<string, Button> ButtonDictionary = new Dictionary<string, Button>();
        internal Player CurrentPlayer { get; set; }
        internal Player Winner { get; set; }
        internal GameState GameStatus { get; set; }
        internal int Row = 3;
        internal int Column = 3;
        internal int SelectedRow { get; set; }
        internal int SelectedColumn { get; set; }
        internal int WinCondition { get; set; } = 3;
        internal int RoundCounter { get; set; } = 0;
        //public List<GamePiece> GamePieces = new List<GamePiece>();
        public TicTacToeUC(List<Player> p_Players)
        {
            InitializeComponent();
            Players = p_Players;
            CurrentPlayer = GetStartingPlayer();
            DisplayPlayerName();
            CreateGameBoard(Row, Column);
        }

        public void DisplayPlayerName()
        {

            Leftside.Children.Add(CreateTextBlock("Player 1:", CurrentPlayer.Sign == Players[0].Sign));
            Leftside.Children.Add(CreateTextBlock(Players[0].Name, CurrentPlayer.Sign == Players[0].Sign));

            Rightside.Children.Add(CreateTextBlock("Player 2:", CurrentPlayer.Sign == Players[1].Sign));
            Rightside.Children.Add(CreateTextBlock(Players[1].Name, CurrentPlayer.Sign == Players[1].Sign));
        }

        private TextBlock CreateTextBlock(string text, bool isCurrentPlayer)
        {
            var textBlock = new TextBlock
            {
                Text = text,
                FontSize = 64,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = isCurrentPlayer ? Brushes.White : Brushes.LightGray
            };
            return textBlock;
        }

        private void CreateGameBoard(int p_Rows, int p_Columns)
        {

            Grid GameBoardTicTacToe = new();

            Rectangle GameBoardBackground = new Rectangle();
            GameBoardBackground.Fill = Brushes.SkyBlue;
            Grid.SetRowSpan(GameBoardBackground, p_Rows);
            Grid.SetColumnSpan(GameBoardBackground, p_Columns);
            GameBoardTicTacToe.Children.Add(GameBoardBackground);

            for (int CurrentRow = 0; CurrentRow < p_Rows; CurrentRow++)
            {
                RowDefinition Row = new RowDefinition();
                GameBoardTicTacToe.RowDefinitions.Add(Row);

                for (int CurrentColumn = 0; CurrentColumn < p_Columns; CurrentColumn++)
                {
                    ColumnDefinition Column = new ColumnDefinition();
                    GameBoardTicTacToe.ColumnDefinitions.Add(Column);

                    Button Cell = new Button();
                    Cell.Name = $"Button_{CurrentRow}_{CurrentColumn}";
                    Cell.Content = "";
                    Cell.FontSize = 300 / p_Rows;
                    Cell.Style = (Style)FindResource("MaterialDesignRaisedSecondaryLightButton");
                    Cell.HorizontalAlignment = HorizontalAlignment.Center;
                    Cell.VerticalAlignment = VerticalAlignment.Top;
                    Cell.Height = 500 / p_Rows;
                    Cell.Width = 500 / p_Rows;
                    Cell.Margin = new Thickness(4);
                    Cell.Click += Cell_Click;

                    Grid.SetRow(Cell, CurrentRow);
                    Grid.SetColumn(Cell, CurrentColumn);
                    ButtonDictionary.Add(Cell.Name, Cell);
                    GameBoardTicTacToe.Children.Add(Cell);
                }
            }
            MainGrid.Children.Add(GameBoardTicTacToe);
        }

        public void Cell_Click(object sender, RoutedEventArgs e)
        {
            bool IsWinner;
            Button ClickedButton = sender as Button;
            string ButtonName = ClickedButton.Name;
            string[] SplitName = ButtonName.Split("_");
            SelectedRow = Convert.ToInt32(SplitName[1]);
            SelectedColumn = Convert.ToInt32(SplitName[2]);

            if (ClickedButton.Content == "")
            {
                string Sign = CurrentPlayer.Sign;
                ClickedButton.Content = Sign;
                ClickedButton.IsEnabled = false;
                Leftside.Children.Clear();
                Rightside.Children.Clear();
                IsWinner = CheckForWin(ButtonName);
                RoundCounter++;

                if (IsWinner)
                {
                    MessageBox.Show($"Spieler {CurrentPlayer.Name} hat gewonnen!");
                    MainGrid.Children.Clear();
                    ButtonDictionary.Clear();
                    CreateGameBoard(Row, Column);
                    RoundCounter = 0;
                }
                else if (RoundCounter == (Row *  Column))
                {
                    MessageBox.Show($"Unentschieden");
                    MainGrid.Children.Clear();
                    ButtonDictionary.Clear();
                    CreateGameBoard(Row, Column);
                    RoundCounter = 0;
                }

                CurrentPlayerDeterminer();
                DisplayPlayerName();
            }

        }

        public void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            ButtonDictionary.Clear();
            CreateGameBoard(Row,Column);
            RoundCounter = 1;
        }



        internal Player GetStartingPlayer()
        {
            Random PlayerRandomizer = new Random();
            return Players[PlayerRandomizer.Next(Players.Count)];
        }

        internal void CurrentPlayerDeterminer()
        {
            if (CurrentPlayer == null)
            {
                CurrentPlayer = GetStartingPlayer();
            }
            else
            {
                int NextPlayerIndex = Players.IndexOf(CurrentPlayer) + 1;

                if (NextPlayerIndex > Players.Count - 1)
                    NextPlayerIndex = 0;
                CurrentPlayer = Players[NextPlayerIndex];
            }
        }

        internal bool CheckForWin(string p_ClickedButton)
        {
            if (CheckHorizontal(p_ClickedButton) || CheckVertical(p_ClickedButton) || CheckDiagonal(p_ClickedButton) || CheckCounterDiagonal(p_ClickedButton))
            {
                return true;
            }
            return false;
        }

        internal bool CheckHorizontal(string p_ClickedButton)
        {
            int WinConditionCounter = 0;
            string[] SplitedButtonName = p_ClickedButton.Split('_');

            
            for (int CheckCounter = Math.Max(0, SelectedColumn - WinCondition + 1);CheckCounter <= Math.Min(Column - 1, SelectedColumn + WinCondition - 1); CheckCounter++)
            {
                string CheckedButtonName = $"{SplitedButtonName[0]}_{SplitedButtonName[1]}_{CheckCounter}";
                
                ButtonDictionary.TryGetValue(CheckedButtonName, out Button button);

                if (button.Content == CurrentPlayer.Sign)
                {
                    WinConditionCounter++;
                    if (WinConditionCounter == WinCondition)
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

        internal bool CheckVertical(string p_ClickedButton)
        {
            int WinConditionCounter = 0;
            string[] SplitedButtonName = p_ClickedButton.Split('_');

            for (int CheckCounter = Math.Max(0, SelectedRow - WinCondition + 1); CheckCounter <= Math.Min(Row - 1, SelectedRow + WinCondition - 1); CheckCounter++)
            {
                string CheckedButtonName = $"{SplitedButtonName[0]}_{CheckCounter}_{SplitedButtonName[2]}";
                ButtonDictionary.TryGetValue(CheckedButtonName, out Button button);

                if (button.Content == CurrentPlayer.Sign)
                {
                    WinConditionCounter++;
                    if (WinConditionCounter == WinCondition)
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

        internal bool CheckDiagonal(string p_ClickedButton)
        {
            int WinConditionCounter = 0;
            string[] SplitedButtonName = p_ClickedButton.Split('_');
            int RowCheckCounter = SelectedRow;
            int ColCheckCounter = SelectedColumn;

            while (RowCheckCounter > 0 && ColCheckCounter > 0)
            {
                RowCheckCounter--;
                ColCheckCounter--;
            }

            while (RowCheckCounter < Row && ColCheckCounter < Column)
            {
                string CheckedButtonName = $"{SplitedButtonName[0]}_{RowCheckCounter}_{ColCheckCounter}";
                ButtonDictionary.TryGetValue(CheckedButtonName, out Button button);

                if (button.Content == CurrentPlayer.Sign)
                {
                    WinConditionCounter++;
                    if (WinConditionCounter == WinCondition)
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

        internal bool CheckCounterDiagonal(string p_ClickedButton)
        {
            int WinConditionCounter = 0;
            string[] SplitedButtonName = p_ClickedButton.Split('_');
            int RowCheckCounter = SelectedRow;
            int ColCheckCounter = SelectedColumn;

            while (RowCheckCounter < Row - 1 && ColCheckCounter > 0)
            {
                RowCheckCounter++;
                ColCheckCounter--;
            }

            while (RowCheckCounter >= 0 && ColCheckCounter < Column)
            {
                string CheckedButtonName = $"{SplitedButtonName[0]}_{RowCheckCounter}_{ColCheckCounter}";
                ButtonDictionary.TryGetValue(CheckedButtonName, out Button CheckedButton);

                if (CheckedButton.Content == CurrentPlayer.Sign)
                {
                    WinConditionCounter++;
                    if (WinConditionCounter == WinCondition)
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
