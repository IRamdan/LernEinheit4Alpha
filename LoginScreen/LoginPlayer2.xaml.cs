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

    public partial class LoginPlayer2 : UserControl
    {

        List<Player> Players = new List<Player>();

        public LoginPlayer2(List<Player> p_Players)
        {
            InitializeComponent();
            Players = p_Players;
        }
        public void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string Nickname = NicknameTextBox.Text;
            string Password = PasswordBox.Password;
            Player Player2 = new Player() { Name = Nickname, Sign = "O" };
            bool PlayerLoggedIn = Database.LoginPlayerAccount(Nickname, Password);

            if (PlayerLoggedIn == false)
            {
                MessageBox.Show("Login failed. Check password and username.");
            }
            else
            {
                Players[1] = Player2;  

                GameWindow CurrentGameWindow = Application.Current.Windows.OfType<GameWindow>().FirstOrDefault();
                CurrentGameWindow.Players = Players;

                MainMenu MainMenu = new MainMenu(Players);
                MainMenu.DisplayNickName(Players[0].Name, Players[1].Name);
                MainMenu.SecondPlayerLoginButton.IsEnabled = false;

                CurrentGameWindow.Maincontent.Content = MainMenu;

                MainWindow Mainwindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                Mainwindow.Visibility = Visibility.Hidden;
            }
        }

        public void SignUpButton_Click(Object Sender, RoutedEventArgs e)
        {
            CreatePlayerAccount CreatePlayerAccount = new CreatePlayerAccount();
            MainWindow MainWindow = Application.Current.MainWindow as MainWindow;

            if (MainWindow != null)
            {
                MainWindow.ContentControl.Content = CreatePlayerAccount;
            }
        }
    }
}
