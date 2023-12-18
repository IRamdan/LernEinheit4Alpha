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


        public List<Player> Players { get; set; }
        public Dictionary<string, Button> ButtonDictionary = new Dictionary<string, Button>();
        public Player CurrentPlayer { get; set; }
        public Player Winner { get; set; }
        internal GameState GameStatus { get; set; }
        public int GameFieldRows = 3;
        public int GameFieldColumns = 3;
        internal int SelectedRow { get; set; }
        internal int SelectedColumn { get; set; }
        internal int WinCondition { get; set; } = 3;
        internal int RoundCounter { get; set; } = 0;

        TicTacToeGameMechanics.CheckWinParameters Parameters = new TicTacToeGameMechanics.CheckWinParameters();

        public TicTacToeUC(List<Player> p_Players)
        {
            InitializeComponent();
            Players = p_Players;
            Parameters.p_Players = Players;
            CurrentPlayer = TicTacToeGameMechanics.GetStartingPlayer(Parameters);
            DisplayPlayerName();
            CreateGameBoard(GameFieldRows, GameFieldColumns);
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
            Button ClickedButton = sender as Button;
            string ButtonName = ClickedButton.Name;
            string[] SplitName = ButtonName.Split("_");
            SelectedRow = Convert.ToInt32(SplitName[1]);
            SelectedColumn = Convert.ToInt32(SplitName[2]);

            if (ClickedButton.Content == "")
            {
                Parameters.p_ButtonDictionary = ButtonDictionary;
                Parameters.p_CurrentPlayer = CurrentPlayer;
                Parameters.p_ClickedButton = ButtonName;
                Parameters.p_SelectedColumn = SelectedColumn;
                Parameters.p_SelectedRow = SelectedRow;
                Parameters.p_WinCondition = WinCondition;
                Parameters.p_GameFieldRows = GameFieldRows;
                Parameters.p_GameFieldColumns = GameFieldColumns;

                string Sign = CurrentPlayer.Sign;
                ClickedButton.Content = Sign;
                ClickedButton.IsEnabled = false;
                Leftside.Children.Clear();
                Rightside.Children.Clear();
                bool IsWinner = TicTacToeGameMechanics.CheckForWin(Parameters);
                RoundCounter++;

                if (IsWinner)
                {
                    MessageBox.Show($"Spieler {CurrentPlayer.Name} hat gewonnen!");
                    MainGrid.Children.Clear();
                    ButtonDictionary.Clear();
                    CreateGameBoard(GameFieldRows, GameFieldColumns);
                    RoundCounter = 0;
                }
                else if (RoundCounter == (GameFieldRows * GameFieldColumns))
                {
                    MessageBox.Show($"Unentschieden");
                    MainGrid.Children.Clear();
                    ButtonDictionary.Clear();
                    CreateGameBoard(GameFieldRows, GameFieldColumns);
                    RoundCounter = 0;
                }

                CurrentPlayer = TicTacToeGameMechanics.CurrentPlayerDeterminer(Parameters);
                DisplayPlayerName();
            }

        }

        public void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            ButtonDictionary.Clear();
            CreateGameBoard(GameFieldRows, GameFieldColumns);
            CurrentPlayer = TicTacToeGameMechanics.GetStartingPlayer(Parameters);
            RoundCounter = 0;
        }
    }
}
