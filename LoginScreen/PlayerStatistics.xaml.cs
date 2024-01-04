using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace LoginScreen
{
    public partial class PlayerStatistics : Window
    {
        public PlayerStatistics(string p_Nickname)
        {
            InitializeComponent();
            PlayerStats.ItemsSource = Database.GetPlayerGameStats(p_Nickname);
        }

       

        public void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
