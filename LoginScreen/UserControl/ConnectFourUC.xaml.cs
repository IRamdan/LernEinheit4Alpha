using FontAwesome.WPF;
using LoginScreen.ConectFour;
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
    /// Interaktionslogik für ConnectFourUC.xaml
    /// </summary>
    public partial class ConnectFourUC : UserControl
    {
        public string CurrentPlayer;

        public Dictionary<string, Ellipse> GamePieces = new Dictionary<string, Ellipse>();
        public ConnectFourUC()
        {
            InitializeComponent();
            CurrentPlayer = ConnectFourGameMechanics.GetCurrentPlayer();
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
            const int Rows = 6;
            const int Columns = 7;

            for (int CurrentRow = 0; CurrentRow <= Rows; CurrentRow++)
            {
                RowDefinition Row = new();
                MainGrid.RowDefinitions.Add(Row);
            }

            for (int CurrentColumn = 0; CurrentColumn < Columns; CurrentColumn++)
            {
                ColumnDefinition Column = new();
                MainGrid.ColumnDefinitions.Add(Column);
            }

            Rectangle GameBoardBackground = new();
            GameBoardBackground.Fill = Brushes.RoyalBlue;
            Grid.SetRow(GameBoardBackground, 1);
            Grid.SetRowSpan(GameBoardBackground, Rows);
            Grid.SetColumnSpan(GameBoardBackground, Columns);
            MainGrid.Children.Add(GameBoardBackground);

            for (int CurrentRow = 0; CurrentRow <= Rows; CurrentRow++)
            {
                for (int CurrentColumn = 0; CurrentColumn < Columns; CurrentColumn++)
                {
                    if (CurrentRow == 0)
                    {
                        Button SelectionButton = new();
                        SelectionButton.Name = $"SelectionButton_{CurrentRow}_{CurrentColumn}";
                        SelectionButton.Content = new ImageAwesome { Icon = FontAwesomeIcon.LongArrowDown, Height = 50, };
                        SelectionButton.FontSize = 80;
                        SelectionButton.Height = 60;
                        SelectionButton.Width = 80;
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
                        GamePiece.Width = 80;
                        GamePiece.Height = 80;
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
            Button ClickedButton = sender as Button;
            string ButtonName = ClickedButton.Name;
            string[] SplitButtonName = ButtonName.Split("_");
            int Column = int.Parse(SplitButtonName[2]);

            for (int CurrentRow = 6; CurrentRow >= 1; CurrentRow--)
            {
                string GamePieceName = $"GamePiece_{CurrentRow}_{Column}";
                if (GamePieces.TryGetValue(GamePieceName, out Ellipse GamePiece))
                {

                    if (GamePiece.Fill == Brushes.SkyBlue)
                    {

                        if (CurrentPlayer == "X")
                        {
                            GamePiece.Fill = Brushes.Red;
                            CurrentPlayer = ConnectFourGameMechanics.GetCurrentPlayer();
                            Leftside.Children.Clear();
                            Rightside.Children.Clear();
                            DisplayPlayerName();
                            break;
                        }
                        else if (CurrentPlayer == "O")
                        {
                            GamePiece.Fill = Brushes.Yellow;
                            CurrentPlayer = ConnectFourGameMechanics.GetCurrentPlayer();
                            Leftside.Children.Clear();
                            Rightside.Children.Clear();
                            DisplayPlayerName();
                            break;
                        }
                    }
                }
            }
        }

        public void AnimatePlacement(Button p_ClickedButton)
        {
            string ButtonName = p_ClickedButton.Name;
            string[] SplitButtonName = ButtonName.Split("_");
            int Row = int.Parse(SplitButtonName[1]);
            int Column = int.Parse(SplitButtonName[2]);

            GamePiece AnimatedGamePiece = FindName();
            for (int Counter = 0; Counter < Row; Counter++)
            {
                
            }

        }

        public void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            GamePieces.Clear();
            CreateGameBoard();
        }
    }
}
