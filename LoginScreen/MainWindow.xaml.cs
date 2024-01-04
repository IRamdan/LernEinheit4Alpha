using LernEinheit4.GameWindow;
using LoginScreen.TicTacToe;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Printing;
using System.Runtime.CompilerServices;
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
        public List<Player> Players = new List<Player>();
        public MainWindow()
        {
            InitializeComponent();
            Players = new List<Player>();

            ShowLoginPlayer1();

        }
        public void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        public void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        public void LoadUserControl(UserControl p_UserControl)
        {
            ContentControl.Content = p_UserControl;
        }

        public void ShowCreatePlayerAccount(object sender, RoutedEventArgs e)
        {
            CreatePlayerAccount CreatePlayerAccount = new CreatePlayerAccount();
            LoadUserControl(CreatePlayerAccount);
        }

        public void ShowLoginPlayer1()
        {
            LoginPlayer1 LoginPlayer1 = new LoginPlayer1();
            LoadUserControl(LoginPlayer1);
        }

        public void ShowLoginPlayer2()
        {
            LoginPlayer2 LoginPlayer2 = new LoginPlayer2(Players);
            LoadUserControl(LoginPlayer2);
        }

    }
}
