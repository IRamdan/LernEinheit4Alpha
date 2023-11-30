using LoginScreen;
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
using System.Windows.Shapes;

namespace LernEinheit4.GameWindow
{
    /// <summary>
    /// Interaktionslogik für GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public GameWindow(string p_Username, string p_Password)
        {
            InitializeComponent();
            Username = p_Username;
            Password = p_Password;

            Maincontent.Content = new MainMenu(Username, "Guest");

            this.KeyUp += OpenEscapeMenuOnKeyUp;
        }

        public void LoadControl(UserControl p_Control)
        {
            Maincontent.Content = p_Control;   
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
            escapeMenu.SetMenuContent();
        }

        public void CloseEscapeMenu()
        {
            OverlayGrid.Visibility = Visibility.Collapsed;
        }

    }
}