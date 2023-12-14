using FontAwesome.WPF;
using LoginScreen.ConectFour;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.Metrics;
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
using System.Windows.Threading;

namespace LoginScreen
{
    //Todo, animation farben und gewinn tictactoe checken 
    public partial class ConnectFourUC : UserControl
    {
        private bool IsAnimating = false;
        public DispatcherTimer Timer;
        public string CurrentPlayer;
        public Dictionary<string, Ellipse> GamePieces = new Dictionary<string, Ellipse>();
        int WinCondition = 4;
        public int Rows = 6;
        public int Columns = 7;
        int FontSize = 80;
        int GamePieceSideLength = 80;
        int SelectionButtonSideLength = 80;
        int SelectedRow { get; set; }
        int SelectedColumn { get; set; }

        public ConnectFourUC()
        {
            InitializeComponent();
            CurrentPlayer = ConnectFourGameMechanics.GetCurrentPlayer();
            DisplayPlayerName();
            CreateGameBoard(Rows, Columns);
        }

        public void DisplayPlayerName()
        {
            Leftside.Children.Clear();
            Rightside.Children.Clear();

            TextBlock TextBlockPlayer1 = new TextBlock();
            TextBlockPlayer1.Text = "Player 1:";
            TextBlockPlayer1.FontSize = 64;
            TextBlockPlayer1.HorizontalAlignment = HorizontalAlignment.Center;
            TextBlockPlayer1.VerticalAlignment = VerticalAlignment.Center;
            TextBlockPlayer1.Foreground = CurrentPlayer == "X" ? Brushes.White : Brushes.LightGray;
            Leftside.Children.Add(TextBlockPlayer1);

            TextBlock TextBlockPlayer1Name = new TextBlock();
            TextBlockPlayer1Name.Text = "Ismail";
            TextBlockPlayer1Name.FontSize = 64;
            TextBlockPlayer1Name.HorizontalAlignment = HorizontalAlignment.Center;
            TextBlockPlayer1Name.VerticalAlignment = VerticalAlignment.Center;
            TextBlockPlayer1Name.Foreground = CurrentPlayer == "X" ? Brushes.White : Brushes.LightGray;
            Leftside.Children.Add(TextBlockPlayer1Name);

            TextBlock TextBlockPlayer2 = new TextBlock();
            TextBlockPlayer2.Text = "Player 2:";
            TextBlockPlayer2.FontSize = 64;
            TextBlockPlayer2.HorizontalAlignment = HorizontalAlignment.Center;
            TextBlockPlayer2.VerticalAlignment = VerticalAlignment.Center;
            TextBlockPlayer2.Foreground = CurrentPlayer == "O" ? Brushes.White : Brushes.LightGray;
            Rightside.Children.Add(TextBlockPlayer2);

            TextBlock TextBlockPlayer2Name = new TextBlock();
            TextBlockPlayer2Name.Text = "Benjo";
            TextBlockPlayer2Name.FontSize = 64;
            TextBlockPlayer2Name.HorizontalAlignment = HorizontalAlignment.Center;
            TextBlockPlayer2Name.VerticalAlignment = VerticalAlignment.Center;
            TextBlockPlayer2Name.Foreground = CurrentPlayer == "O" ? Brushes.White : Brushes.LightGray;
            Rightside.Children.Add(TextBlockPlayer2Name);
        }

        private void CreateGameBoard(int p_Row, int p_Column)
        {

            for (int CurrentRow = 0; CurrentRow <= p_Row; CurrentRow++)
            {
                RowDefinition Row = new();
                MainGrid.RowDefinitions.Add(Row);
            }

            for (int CurrentColumn = 0; CurrentColumn < p_Column; CurrentColumn++)
            {
                ColumnDefinition Column = new();
                MainGrid.ColumnDefinitions.Add(Column);
            }

            Rectangle GameBoardBackground = new();
            GameBoardBackground.Fill = Brushes.RoyalBlue;
            Grid.SetRow(GameBoardBackground, 1);
            Grid.SetRowSpan(GameBoardBackground, p_Row);
            Grid.SetColumnSpan(GameBoardBackground, p_Column);
            MainGrid.Children.Add(GameBoardBackground);

            for (int CurrentRow = 0; CurrentRow <= p_Row; CurrentRow++)
            {
                for (int CurrentColumn = 0; CurrentColumn < p_Column; CurrentColumn++)
                {
                    if (CurrentRow == 0)
                    {
                        Button SelectionButton = new();
                        SelectionButton.Name = $"SelectionButton_{CurrentRow}_{CurrentColumn}";
                        SelectionButton.Content = new ImageAwesome { Icon = FontAwesomeIcon.LongArrowDown, Height = 50, };
                        SelectionButton.FontSize = FontSize;
                        SelectionButton.Height = SelectionButtonSideLength;
                        SelectionButton.Width = SelectionButtonSideLength;
                        SelectionButton.Margin = new Thickness(10);
                        SelectionButton.Background = Brushes.Transparent;
                        SelectionButton.BorderBrush = Brushes.Transparent;
                        SelectionButton.HorizontalAlignment = HorizontalAlignment.Center;
                        SelectionButton.VerticalAlignment = VerticalAlignment.Center;
                        SelectionButton.Click += SelectionButton_Click;
                        SelectionButton.MouseEnter += SelectionButton_MouseEnter;
                        SelectionButton.MouseLeave += SelectionButton_MouseLeave;

                        Grid.SetRow(SelectionButton, CurrentRow);
                        Grid.SetColumn(SelectionButton, CurrentColumn);
                        MainGrid.Children.Add(SelectionButton);
                    }
                    else
                    {
                        Ellipse GamePiece = new();
                        GamePiece.Name = $"GamePiece_{CurrentRow}_{CurrentColumn}";
                        GamePiece.Width = GamePieceSideLength;
                        GamePiece.Height = GamePieceSideLength;
                        GamePiece.Margin = new Thickness(5);
                        GamePiece.Fill = Brushes.SkyBlue;

                        Grid.SetRow(GamePiece, CurrentRow);
                        Grid.SetColumn(GamePiece, CurrentColumn);
                        GamePieces.Add(GamePiece.Name, GamePiece);
                        MainGrid.Children.Add(GamePiece);
                    }
                }
            }
        }
        private void SelectionButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Button Selectionbutton = sender as Button;
            Selectionbutton.Background = Brushes.Transparent;
        }

        private void SelectionButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Button Selectionbutton = sender as Button;
            Selectionbutton.Background = Brushes.White;
        }

        public void SelectionButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsAnimating)
                return;

            bool IsWinner = false;
            Button ClickedButton = sender as Button;
            string ButtonName = ClickedButton.Name;
            string[] SplitButtonName = ButtonName.Split("_");
            int Column = int.Parse(SplitButtonName[2]);

            for (int CurrentRow = Rows; CurrentRow >= 1; CurrentRow--)
            {
                string GamePieceName = $"GamePiece_{CurrentRow}_{Column}";
                if (GamePieces.TryGetValue(GamePieceName, out Ellipse GamePiece))
                {
                    if (GamePiece.Fill == Brushes.SkyBlue)
                    {
                        if (CurrentPlayer == "X")
                        {
                            IsAnimating = true;
                            AnimatePlacement(GamePiece, Column, CurrentRow, CurrentPlayer);
                            //IsWinner = CheckForWin(GamePieceName);

                            if (IsWinner)
                            {
                                MessageBox.Show($"Spieler {CurrentPlayer} hat gewonnen!");
                            }
                            break;
                        }
                        else if (CurrentPlayer == "O")
                        {
                            IsAnimating = true;
                            AnimatePlacement(GamePiece, Column, CurrentRow, CurrentPlayer);
                            //IsWinner = CheckForWin(ButtonName);

                            if (IsWinner)
                            {
                                MessageBox.Show($"Spieler {CurrentPlayer} hat gewonnen!");
                            }
                            break;
                        }
                    }
                }
            }
            CurrentPlayer = ConnectFourGameMechanics.GetCurrentPlayer();
            DisplayPlayerName();

        }

        private async void AnimatePlacement(Ellipse p_GamePiece, int p_Column, int p_SelectedRow, string p_CurrentPlayer)
        {

            for (int Row = 1; Row <= p_SelectedRow; Row++)
            {
                double Acceleration = 5;
                double CurrentSpeed = Math.Sqrt(2 * Acceleration * Row );
                double CurrentTime = CurrentSpeed / Acceleration;
                double Speed = Math.Sqrt(2 * Acceleration * p_SelectedRow);
                double Time = Speed / Acceleration;
                int TimeInterval = (int)((Time - CurrentTime) * 100);


                string GamePieceName = $"GamePiece_{Row}_{p_Column}";
                if (GamePieces.TryGetValue(GamePieceName, out Ellipse AnimatedGamePiece))
                {

                    if (AnimatedGamePiece.Fill == Brushes.SkyBlue)
                    {
                        if (p_CurrentPlayer == "X")
                        {
                            AnimatedGamePiece.Fill = Brushes.Red;
                        }
                        else if (p_CurrentPlayer == "O")
                        {
                            AnimatedGamePiece.Fill = Brushes.Yellow;
                        }


                        await Task.Delay(TimeInterval);

                        AnimatedGamePiece.Fill = Brushes.SkyBlue;
                    }
                }
            }

            if (p_CurrentPlayer == "X")
            {
                p_GamePiece.Fill = Brushes.Red;
            }
            else if (p_CurrentPlayer == "O")
            {
                p_GamePiece.Fill = Brushes.Yellow;
            }
            IsAnimating = false;
        }

        //internal bool CheckForWin(string p_ClickedButton)
        //{
        //    if (CheckHorizontal(p_ClickedButton) || CheckVertical(p_ClickedButton) || CheckDiagonal(p_ClickedButton) || CheckCounterDiagonal(p_ClickedButton))
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        internal bool CheckHorizontal(string p_ClickedButton)
        {
            int WinConditionCounter = 0;
            string[] SplitedButtonName = p_ClickedButton.Split('_');
            for (int CheckCounter = Math.Max(0, SelectedColumn - WinCondition + 1); CheckCounter <= Math.Min(Columns - 1, SelectedColumn + WinCondition - 1); CheckCounter++)
            {
                string CheckedButtonName = $"{SplitedButtonName[0]}_{SplitedButtonName[1]}_{CheckCounter}";

                GamePieces.TryGetValue(CheckedButtonName, out Ellipse button);

                if ("X" == CurrentPlayer)
                {
                    button.Fill = Brushes.Red;
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

            for (int CheckCounter = Math.Max(0, SelectedRow - WinCondition + 1); CheckCounter <= Math.Min(Rows - 1, SelectedRow + WinCondition - 1); CheckCounter++)
            {
                string CheckedButtonName = $"{SplitedButtonName[0]}_{CheckCounter}_{SplitedButtonName[2]}";
                GamePieces.TryGetValue(CheckedButtonName, out Ellipse button);

                if (button.Fill == Brushes.Red)
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

            while (RowCheckCounter < Rows && ColCheckCounter < Columns)
            {
                string CheckedButtonName = $"{SplitedButtonName[0]}_{RowCheckCounter}_{ColCheckCounter}";
                GamePieces.TryGetValue(CheckedButtonName, out Ellipse button);

                if (button.Fill == Brushes.Red)
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

            while (RowCheckCounter < Rows - 1 && ColCheckCounter > 0)
            {
                RowCheckCounter++;
                ColCheckCounter--;
            }

            while (RowCheckCounter >= 0 && ColCheckCounter < Columns)
            {
                string CheckedButtonName = $"{SplitedButtonName[0]}_{RowCheckCounter}_{ColCheckCounter}";
                GamePieces.TryGetValue(CheckedButtonName, out Ellipse button);

                if (button.Fill == Brushes.Red)
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

        public void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            GamePieces.Clear();
            CreateGameBoard(Rows, Columns);

        }
    }
}
