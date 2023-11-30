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
    /// <summary>
    /// Interaktionslogik für TicTacToeUC.xaml
    /// </summary>
    public partial class TicTacToeUC : UserControl
    {
        public string CurrentPlayer;

        public TicTacToeUC(string p_Player1Nickname, string p_Player2Nickname)
        {
            InitializeComponent();
            CurrentPlayer = TicTacToeGameMechanics.GetCurrentPlayer();
            DisplayPlayerName();
            CreateGameBoard();
        }

        public void DisplayPlayerName()
        {

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

            for (int i = 0; i < Rows; i++)
            {
                RowDefinition Row = new RowDefinition();
                GameBoardTicTacToe.RowDefinitions.Add(Row);

                for (int j = 0; j < Columns; j++)
                {
                    ColumnDefinition Column = new ColumnDefinition();
                    GameBoardTicTacToe.ColumnDefinitions.Add(Column);

                    Button Cell = new Button();
                    Cell.Name = $"Button_{i}_{j}";
                    Cell.Content = "";
                    Cell.FontSize = 100;
                    Cell.Style = (Style)FindResource("MaterialDesignRaisedSecondaryLightButton");
                    Cell.HorizontalAlignment = HorizontalAlignment.Center;
                    Cell.VerticalAlignment = VerticalAlignment.Top;
                    Cell.Height = 160;
                    Cell.Width = 160;
                    Cell.Margin = new Thickness(6);
                    Cell.Click += Cell_Click;

                    Grid.SetRow(Cell, i);
                    Grid.SetColumn(Cell, j);
                    GameBoardTicTacToe.Children.Add(Cell);
                }
            }
            MainGrid.Children.Add(GameBoardTicTacToe);
        }

        public void Cell_Click(object sender, RoutedEventArgs e)
        {
            Button ClickedButton = sender as Button;
            string ButtonName = ClickedButton.Name;
            string[] SplitName = ButtonName.Split("_");

            if (ClickedButton.Content == "")
            {
                string Sign = CurrentPlayer;
                ClickedButton.Content = Sign;
                ClickedButton.IsEnabled = false;
                CurrentPlayer = TicTacToeGameMechanics.GetCurrentPlayer();
                Leftside.Children.Clear();
                Rightside.Children.Clear();
                DisplayPlayerName();
            }

            if (SplitName.Length == 3 && SplitName[0] == "Button")
            {
                int Row = int.Parse(SplitName[1]);
                int Column = int.Parse(SplitName[2]);
            }
        }

        public void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            CreateGameBoard();
        }
    }
}
