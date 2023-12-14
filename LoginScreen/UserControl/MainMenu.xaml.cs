using LernEinheit4.GameWindow;
using LoginScreen;
using LoginScreen.TicTacToe;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoginScreen
{
    public partial class MainMenu : UserControl
    {
        public List<Player> Players { get; set; }

        public MainMenu(List<Player> p_Players)
        {
            InitializeComponent();
            Players = p_Players;
            DisplayNickName(Players[0].Name, Players[1].Name);
        }

        public void LeaderBoardButton_Click(Object sender, RoutedEventArgs e)
        {
            GameWindow window = Window.GetWindow(this) as GameWindow;
            if (window != null)
            {
                window.LoadControl(new LeaderBoard());
            }
        }

        public void DisplayNickName(string p_NickNamePlayer1, string p_NickNamePlayer2)
        {
            Player1NickName.Text = p_NickNamePlayer1;
            Player2NickName.Text = p_NickNamePlayer2;
        }

        public void PlayerstatisticsButton_Click(Object sender, RoutedEventArgs e)
        {
            PlayerStatisticsInputScreen InputScreen = new PlayerStatisticsInputScreen();
            InputScreen.Show();
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            if (GameButtonsPanel.Height == 0)
            {
                Storyboard expandAnimation = (Storyboard)GameButtonsPanel.Resources["ExpandAnimation"];
                expandAnimation.Begin();
            }
            else
            {
                Storyboard collapseAnimation = (Storyboard)GameButtonsPanel.Resources["CollapseAnimation"];
                collapseAnimation.Begin();
            }
        }

        private void TicTacToe_Click(object sender, RoutedEventArgs e)
        {
            GameWindow window = Window.GetWindow(this) as GameWindow;
            if (window != null)
            {
                window.LoadControl(new TicTacToeUC(Players));
            }
        }

        private void ConnectFour_Click(object sender, RoutedEventArgs e)
        {
            GameWindow window = Window.GetWindow(this) as GameWindow;
            if (window != null)
            {
                window.LoadControl(new ConnectFourUC());
            }
        }

        private void SecondPlayerButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }
    }
}
