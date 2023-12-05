using LernEinheit4.GameWindow;
using LoginScreen.TicTacToe;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal List<Player> Players { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Players = new List<Player>();
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void MinimizeButton_Click(Object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        public Player InitiatePlayer(int p_PlayerIdent, string p_Name, string p_Sign )
        {
            Player Player = new Player();
            Player.PlayerIdent = p_PlayerIdent;
            Player.Name = p_Name;
            Player.Sign = p_Sign;
            return Player;

        }

        public void LoginButton_Click(Object sender, RoutedEventArgs e)
        {
            string Username = NicknameTextBox.Text;
            string Password = PasswordBox.Password;

            if (Username == "Ismail" && Password == "1234")
            {
                Players.Add(InitiatePlayer(1,"Ismail","X"));
                Players.Add(InitiatePlayer(2, "Guest", "O"));
                GameWindow gameWindow = new GameWindow(Players);
                gameWindow.Show();
                this.Close();
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start {e.Uri.AbsoluteUri}") { CreateNoWindow = true });
                e.Handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to open link: {ex.Message}");
            }
        }



    }
}
