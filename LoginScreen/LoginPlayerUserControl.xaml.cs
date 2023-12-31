﻿using LernEinheit4.GameWindow;
using LoginScreen.TicTacToe;
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
   
    public partial class LoginPlayerUserControl : UserControl
    {

        List<Player> Players = new List<Player>();

        public LoginPlayerUserControl()
        {
            InitializeComponent();
        }
        public Player InitiatePlayer(int p_PlayerIdent, string p_Name, string p_Sign)
        {
            Player Player = new Player();
            Player.PlayerIdent = p_PlayerIdent;
            Player.Name = p_Name;
            Player.Sign = p_Sign;
            return Player;

        }

        public void LoginButton_Click(Object sender, RoutedEventArgs e)
        {
            Players.Add(InitiatePlayer(1, "Ismail", "X"));
            Players.Add(InitiatePlayer(2, "Guest", "O"));
            GameWindow gameWindow = new GameWindow(Players);
            gameWindow.Show();

        }

        public void SignUpButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}