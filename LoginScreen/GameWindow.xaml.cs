using LoginScreen;
using LoginScreen.TicTacToe;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LernEinheit4.GameWindow
{
    /// <summary>
    /// Interaktionslogik für GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public int PlayerCount { get; set; }
        public List<Player> Players { get; set; }
        public UserControl CurrentContent { get; set; }
        private EscapeMenu CurrentEscapeMenu { get; set; }

        public GameWindow(List<Player> p_Players)
        {
            InitializeComponent();
            Players = p_Players;
            LoadMainMenu();
            PlayerCount = Players.Count();
            CurrentEscapeMenu = new EscapeMenu(CurrentContent);
            CurrentEscapeMenu.OnResumeGameClicked += ResumeGameHandler;
            CurrentEscapeMenu.OnBackToMainMenuClicked += BackToMainMenuHandler;
            CurrentEscapeMenu.OnExitGameClicked += ExitGameHandler;

            this.KeyUp += OpenEscapeMenuOnKeyUp;
        }

        private void ResumeGameHandler()
        {
            OverlayGrid.Visibility = Visibility.Hidden;
        }

        private void ExitGameHandler()
        {
            var exitConfirmationWindow = new ExitConfirmationWindow();
            if (exitConfirmationWindow.ShowDialog() == true)
            {
                Application.Current.Shutdown();
            }
        }

        private void BackToMainMenuHandler()
        {
            OverlayGrid.Visibility = Visibility.Hidden;
            LoadMainMenu();
        }

        public void LoadControl(UserControl p_Control)
        {
            Maincontent.Content = p_Control;
            CurrentContent = p_Control;

            CurrentEscapeMenu.SetMenuContent(p_Control);
        }

        public void LoadMainMenu()
        {
            CurrentContent = new MainMenu(Players);
            Maincontent.Content = CurrentContent;

            CurrentEscapeMenu?.SetMenuContent(CurrentContent);
        }

        public void OpenEscapeMenuOnKeyUp(Object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                OpenEscapeMenuOverlay();
            }
        }

        public void OpenEscapeMenuOverlay()
        {
            OverlayGrid.Visibility = Visibility.Visible;
            escapeMenu.Content = CurrentEscapeMenu;
        }
    }
}