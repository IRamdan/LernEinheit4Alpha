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

namespace LoginScreen
{
    public partial class PlayerStatisticsInputScreen : Window
    {
        public PlayerStatisticsInputScreen()
        {
            InitializeComponent();
        }

        public void ConfirmButton_Click(Object sender, RoutedEventArgs e)
        {
            string EnteredText = InputTextBox.Text;

            if(EnteredText == "Ismail")
            {
                PlayerStatistics PlayerStatistics = new PlayerStatistics();
                this.Close();   
                PlayerStatistics.Show();
            }
            else
            {
                MessageBox.Show("Player not found");
                this.Close();
            }
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
