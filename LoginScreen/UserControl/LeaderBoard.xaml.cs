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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoginScreen
{
    /// <summary>
    /// Interaktionslogik für LeaderBoard.xaml
    /// </summary>
    public partial class LeaderBoard : UserControl
    {
        public LeaderBoard()
        {
            InitializeComponent();
            LoadLeaderboardTestData();
        }

   
        private void LoadLeaderboardTestData()
        {
            Random random = new Random();

            List<TestData> TestDatas = new List<TestData>
        {
            new TestData { NickName = "Ismail", Score = (random.Next(9000, 10000)).ToString(), Rank = "1" },
            new TestData { NickName = "SayMyName", Score = (random.Next(8000, 9500)).ToString(), Rank = "2" },
            new TestData { NickName = "HereWeGo", Score = (random.Next(7000, 7800)).ToString(), Rank = "3" },
            new TestData { NickName = "TheOneWhoKnocks", Score = (random.Next(4000, 5000)).ToString(), Rank = "4" },
            new TestData { NickName = "LordOfTheOs", Score = (random.Next(3500, 4000)).ToString(), Rank = "5" },
            new TestData { NickName = "Web-Slinger", Score = (random.Next(3000, 3500)).ToString(), Rank = "6" },
            new TestData { NickName = "MrBean", Score = (random.Next(2500, 3000)).ToString(), Rank = "7" },
            new TestData { NickName = "ManofCopper", Score = (random.Next(2000, 2500)).ToString(), Rank = "8" },
            new TestData { NickName = "DarkPhoenix", Score = (random.Next(1500, 2000)).ToString(), Rank = "9" },
            new TestData { NickName = "WalkingBread", Score = (random.Next(1000, 1500)).ToString(), Rank = "10" },
            new TestData { NickName = "Witcher", Score = (random.Next(900, 1000)).ToString(), Rank = "11" },
            new TestData { NickName = "ThePunishment", Score = (random.Next(800, 900)).ToString(), Rank = "12" },
            new TestData { NickName = "MandalorianSpeaker", Score = (random.Next(700, 800)).ToString(), Rank = "13" },
            new TestData { NickName = "ParadoxParadox", Score = (random.Next(600, 700)).ToString(), Rank = "14" },
            new TestData { NickName = "ArkhamKnight", Score = (random.Next(500, 600)).ToString(), Rank = "15" },
            new TestData { NickName = "SpringSoldier", Score = (random.Next(400, 500)).ToString(), Rank = "16" },
            new TestData { NickName = "FirstGuardian", Score = (random.Next(300, 400)).ToString(), Rank = "17" },
            new TestData { NickName = "BlueHood", Score = (random.Next(200, 300)).ToString(), Rank = "18" },
            new TestData { NickName = "SpookyGhostofTsushima", Score = (random.Next(100, 200)).ToString(), Rank = "19" },
            new TestData { NickName = "WatchingHour", Score = (random.Next(50, 100)).ToString(), Rank = "20" },
            new TestData { NickName = "LastofThem", Score = (random.Next(40, 50)).ToString(), Rank = "21" },
            new TestData { NickName = "GodOfWar", Score = (random.Next(30, 40)).ToString(), Rank = "22" },
            new TestData { NickName = "ObiWanOrbi", Score = (random.Next(20, 30)).ToString(), Rank = "23" },
            new TestData { NickName = "D2RR", Score = (random.Next(10, 20)).ToString(), Rank = "24" },
            new TestData { NickName = "DarkVader", Score = (random.Next(5, 10)).ToString(), Rank = "25" },
            new TestData { NickName = "Spongebobe", Score = (random.Next(4, 5)).ToString(), Rank = "26" },
            new TestData { NickName = "ScribbleBob", Score = (random.Next(3, 4)).ToString(), Rank = "27" },
            new TestData { NickName = "SomeOne", Score = (random.Next(2, 3)).ToString(), Rank = "28" },
            new TestData { NickName = "NoOne", Score = (random.Next(1, 2)).ToString(), Rank = "29" },
            new TestData { NickName = "Messi", Score = (random.Next(0, 1)).ToString(), Rank = "30" }
        };
            LeaderBoardView.ItemsSource = TestDatas;
        }
    }
}
