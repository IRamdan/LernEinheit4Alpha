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
        internal Player CurrentPlayer { get; set; }
        internal Player Winner { get; set; }
        internal GameState GameStatus { get; set; }
        internal int Row { get; set; }
        internal int Column { get; set; }
        internal int SelectedRow { get; set; }
        internal int SelectedColumn { get; set; }
        internal int WinCondition { get; set; } = 3;
        internal int RoundCounter = 0;
        //public List<GamePiece> GamePieces = new List<GamePiece>();
        public TicTacToeUC(List<Player> p_Players)
        {
            InitializeComponent();
            Players = p_Players;
            CurrentPlayer = GetStartingPlayer();
            DisplayPlayerName();
            CreateGameBoard();
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

        private void CreateGameBoard()
        {
            const int Rows = 3;
            const int Columns = 3;

            Grid GameBoardTicTacToe = new();

            Rectangle GameBoardBackground = new Rectangle();
            GameBoardBackground.Fill = Brushes.SkyBlue;
            Grid.SetRowSpan(GameBoardBackground, Rows);
            Grid.SetColumnSpan(GameBoardBackground, Columns);
            GameBoardTicTacToe.Children.Add(GameBoardBackground);

            for (int CurrentRow = 0; CurrentRow < Rows; CurrentRow++)
            {
                RowDefinition Row = new RowDefinition();
                GameBoardTicTacToe.RowDefinitions.Add(Row);

                for (int CurrentColumn = 0; CurrentColumn < Columns; CurrentColumn++)
                {
                    ColumnDefinition Column = new ColumnDefinition();
                    GameBoardTicTacToe.ColumnDefinitions.Add(Column);

                    Button Cell = new Button();
                    Cell.Name = $"Button_{CurrentRow}_{CurrentColumn}";
                    Cell.Content = "";
                    Cell.FontSize = 300 / Rows;
                    Cell.Style = (Style)FindResource("MaterialDesignRaisedSecondaryLightButton");
                    Cell.HorizontalAlignment = HorizontalAlignment.Center;
                    Cell.VerticalAlignment = VerticalAlignment.Top;
                    Cell.Height = 500 / Rows;
                    Cell.Width = 500 / Rows;
                    Cell.Margin = new Thickness(4);
                    Cell.Click += Cell_Click;

                    Grid.SetRow(Cell, CurrentRow);
                    Grid.SetColumn(Cell, CurrentColumn);
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
            int Row = Convert.ToInt32(SplitName[1]);
            int Column = Convert.ToInt32(SplitName[2]);

            if (ClickedButton.Content == "")
            {
                string Sign = CurrentPlayer.Sign;
                ClickedButton.Content = Sign;
                ClickedButton.IsEnabled = false;
                Leftside.Children.Clear();
                Rightside.Children.Clear();
                DisplayPlayerName();
                CurrentPlayerDeterminer();


                SelectedRow = Row;
                SelectedColumn = Column;

                IsWinner = CheckForWin(ClickedButton);

                if (IsWinner)
                {
                    MessageBox.Show($"Spieler {CurrentPlayer.Name} hat gewonnen!");
                }
            }

        }

        public void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            CreateGameBoard();
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

        internal bool CheckForWin(Button p_ClickedButton)
        {
            if (CheckHorizontal(p_ClickedButton) || CheckVertical(p_ClickedButton) || CheckDiagonal(p_ClickedButton) || CheckCounterDiagonal(p_ClickedButton))
            {
                MessageBox.Show("Du hast gewonnen");
                return true;
            }
            return false;
        }

        internal bool CheckHorizontal(Button p_ClickedButton)
        {
            int WinConditionCounter = 0;
            int CheckCounter = Math.Max(0, SelectedColumn - WinCondition + 1);
            int MinColumn = Math.Min(Column - 1, SelectedColumn + WinCondition - 1);
            MessageBox.Show($"CheckCounter{CheckCounter}, MaxColumn{MinColumn}, Row{SelectedRow}, Column{SelectedColumn}");
            for (; CheckCounter <= MinColumn; CheckCounter++)
            {
                string CheckedButtonName = $"Button_{SelectedRow}_{CheckCounter}";
                MessageBox.Show("BIN in for schleife");
                Button CheckedButton = MainGrid.FindName(CheckedButtonName) as Button;

                if (CheckedButton != null && CheckedButton.Content.ToString() == p_ClickedButton.Content.ToString())
                {
                    MessageBox.Show("bIN IN IF SCHLEIFE");
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

        internal bool CheckVertical(Button p_ClickedButton)
        {
            int WinConditionCounter = 0;
            for (int CheckCounter = Math.Max(0, SelectedRow - WinCondition + 1); CheckCounter <= Math.Min(Row - 1, SelectedRow + WinCondition - 1); CheckCounter++)
            {
                string CheckedButtonName = $"Button_{SelectedRow}_{CheckCounter}";
                Button CheckedButton = MainGrid.FindName(CheckedButtonName) as Button;

                if (CheckedButton.Content == p_ClickedButton.Content)
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

        internal bool CheckDiagonal(Button p_ClickedButton)
        {
            int WinConditionCounter = 0;
            int RowCheckCounter = SelectedRow;
            int ColCheckCounter = SelectedColumn;

            while (RowCheckCounter > 0 && ColCheckCounter > 0)
            {
                RowCheckCounter--;
                ColCheckCounter--;
            }

            while (RowCheckCounter < Row && ColCheckCounter < Column)
            {
                string CheckedButtonName = $"Button_{RowCheckCounter}_{ColCheckCounter}";
                Button CheckedButton = MainGrid.FindName(CheckedButtonName) as Button;

                if (CheckedButton.Content == p_ClickedButton.Content)
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

        internal bool CheckCounterDiagonal(Button p_ClickedButton)
        {
            int WinConditionCounter = 0;
            int RowCheckCounter = SelectedRow;
            int ColCheckCounter = SelectedColumn;

            while (RowCheckCounter < Row - 1 && ColCheckCounter > 0)
            {
                RowCheckCounter++;
                ColCheckCounter--;
            }

            while (RowCheckCounter >= 0 && ColCheckCounter < Column)
            {
                string CheckedButtonName = $"Button_{RowCheckCounter}_{ColCheckCounter}";
                Button CheckedButton = MainGrid.FindName(CheckedButtonName) as Button;

                if (CheckedButton.Content == p_ClickedButton.Content)
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
