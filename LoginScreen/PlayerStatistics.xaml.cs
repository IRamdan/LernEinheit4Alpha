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
    public partial class PlayerStatistics : Window
    {
        public PlayerStatistics()
        {
            InitializeComponent();
            TestPlayer DemoPlayer = new TestPlayer("DemoPlayer", "50", "30", "15", "5", "0", "60%");
            CreatePlayerStatisticsBoard(DemoPlayer);
        }

        public void CreatePlayerStatisticsBoard(TestPlayer p_Player)
        {
            StackPanel PlayerStatisticsPanel = new StackPanel();
            PlayerStatisticsPanel.Orientation = Orientation.Vertical;
            PlayerStatisticsPanel.HorizontalAlignment = HorizontalAlignment.Center;

            PlayerStatisticsPanel.Children.Add(CreateRow("NickName", p_Player));
            PlayerStatisticsPanel.Children.Add(CreateRow("TotalGames", p_Player));
            PlayerStatisticsPanel.Children.Add(CreateRow("Wins", p_Player));
            PlayerStatisticsPanel.Children.Add(CreateRow("Losses", p_Player));
            PlayerStatisticsPanel.Children.Add(CreateRow("Draws", p_Player));
            PlayerStatisticsPanel.Children.Add(CreateRow("UnfinishedGames", p_Player));
            PlayerStatisticsPanel.Children.Add(CreateRow("Successrate", p_Player));

            PlayerStatisticsBoard.Children.Add(PlayerStatisticsPanel);
        }


        public TextBlock CreateRow(string p_RowName, TestPlayer p_Player)
        {
            TextBlock Row = new TextBlock();
            string value = GetPropertyValue(p_RowName, p_Player);
            Row.Text = $"{p_RowName}: {value}";
            Row.HorizontalAlignment = HorizontalAlignment.Center;
            Row.FontSize = 42;
            return Row;
        }

        public string GetPropertyValue(string p_PropertyName, TestPlayer p_Player)
        {
            switch (p_PropertyName)
            {
                case "NickName":
                    return p_Player.NickName;
                case "TotalGames":
                    return p_Player.TotalGames;
                case "Wins":
                    return p_Player.Wins;
                case "Losses":
                    return p_Player.Losses;
                case "Draws":
                    return p_Player.Draws;
                case "UnfinishedGames":
                    return p_Player.UnfinishedGames;
                case "Successrate":
                    return p_Player.Successrate;
                default:
                    return "";
            }
        }

        public void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
