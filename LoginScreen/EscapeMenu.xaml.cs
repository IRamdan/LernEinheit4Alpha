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
    public partial class EscapeMenu : UserControl
    {
        public delegate void ResumeGameClicked();
        public event ResumeGameClicked OnResumeGameClicked;

        public delegate void BackToMainMenuClicked();
        public event BackToMainMenuClicked OnBackToMainMenuClicked;

        public delegate void ExitGameClicked();
        public event ExitGameClicked OnExitGameClicked;

        public EscapeMenu()
        {
            InitializeComponent();
            SetMenuContent();
        }

        public void SetMenuContent()
        {
            ButtonPanel.Children.Clear();
            if (false)
            {
                AddButton("Resume", OnResumeGameClicked);
                AddButton("Leave Game", OnExitGameClicked);
            }
            else
            {
                AddButton("Resume", OnResumeGameClicked);
                AddButton("Back to Main Menu", OnBackToMainMenuClicked);
                AddButton("Leave Game", OnExitGameClicked);
            }
        }

        private void AddButton(string content, Delegate eventHandler)
        {
            Button button = new Button();
            button.Content = content;
            button.Foreground = Brushes.White;
            button.Height = 50;
            button.Width = 300;
            button.FontSize = 24;
            button.Margin = new Thickness(10);
            button.Click += (sender, e) => eventHandler.DynamicInvoke(); 


            ButtonPanel.Children.Add(button);
        }
    }
}
