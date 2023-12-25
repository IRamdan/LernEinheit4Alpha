using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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

        public EscapeMenu(UserControl p_CurrentUsercontrol)
        {
            InitializeComponent();
            SetMenuContent(p_CurrentUsercontrol);
        }

        public void SetMenuContent(UserControl p_CurrentUsercontrol)
        {
            ButtonPanel.Children.Clear();

            if (p_CurrentUsercontrol.GetType() == typeof(MainMenu))
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

        private void AddButton(string p_Content, Delegate p_EventHandler)
        {
            Button EscapeButton = new Button();
            EscapeButton.Content = p_Content;
            EscapeButton.Style = (Style)FindResource("MainMenuButton");

            EscapeButton.Click += (sender, e) =>
            {
                if (p_Content == "Resume" && OnResumeGameClicked != null)
                {
                    OnResumeGameClicked.Invoke();
                }
                else if (p_Content == "Back to Main Menu" && OnBackToMainMenuClicked != null)
                {
                    OnBackToMainMenuClicked.Invoke();
                }
                else if (p_Content == "Leave Game" && OnExitGameClicked != null)
                {
                    OnExitGameClicked.Invoke();
                }
            };

            ButtonPanel.Children.Add(EscapeButton);
        }
    }
}
