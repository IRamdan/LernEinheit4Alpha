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
    public partial class CreatePlayerAccount : UserControl
    {
        public CreatePlayerAccount()
        {
            InitializeComponent();
        }

        public void CreatePlayerAccount_Click(Object sender, RoutedEventArgs e)
        {
            string Nickname = NicknameTextBox.Text;
            string Password = PasswordBox.Password;
        }
    }
}
