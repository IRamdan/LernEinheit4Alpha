using LernEinheit4.GameWindow;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoginScreen
{

    public partial class LoginPlayer1 : UserControl
    {

        List<Player> Players { get; set;} = new List<Player> {};
        MainWindow MainWindow = Application.Current.MainWindow as MainWindow;

        public LoginPlayer1()
        {
            InitializeComponent();
        }
        public Player InitiatePlayer(int p_PlayerIdent, string p_Name, string p_Sign)
        {
            Player Player = new Player();
            Player.PlayerIdent = p_PlayerIdent;
            Player.Name = p_Name;
            Player.Sign = p_Sign;
            return Player;
        }

        public void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string Nickname = NicknameTextBox.Text;
            string Password = PasswordBox.Password;
            Player Player1 = new Player() { Name = Nickname, Sign = "X" };
            Player Guest = new Player() { Name = "Guest", Sign = "O" };
            bool PlayerLoggedIn = Database.LoginPlayerAccount(Nickname, Password);

            if (PlayerLoggedIn == false)
            {
                MessageBox.Show("Anmeldung fehlgeschlagen. Überprüfen Sie Ihren Benutzernamen und Ihr Passwort.");
            }
            else
            {
                Players.Add(Player1);
                Players.Add(Guest);
                GameWindow GameWindow = new GameWindow(Players);
                
                MainWindow.Visibility = Visibility.Hidden;

                GameWindow.Show();

            }
        }

        public void SignUpButton_Click(Object Sender, RoutedEventArgs e)
        {
            CreatePlayerAccount CreatePlayerAccount = new CreatePlayerAccount();
            

            if (MainWindow != null)
            {
                MainWindow.ContentControl.Content = CreatePlayerAccount;
            }
        }
    }
}
