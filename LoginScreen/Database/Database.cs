using LoginScreen.TicTacToe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginScreen
{
    public class Database
    {
        internal static string ConnectionString = "Data Source=zub-PC143\\SQLEXPRESS;" + "Initial Catalog=Lerneinheit3;" + "User ID=Peter;" + "Password=75WFjCG8pPq.eGB;";
        private static T ConvertField<T>(object field) => (field == null || field == DBNull.Value) ? default : (T)Convert.ChangeType(field, typeof(T));
        internal static Player CreatePlayerAccount(string p_Nickname, string p_Password)
        {
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();
                using (SqlCommand CreatePlayerAccount = new SqlCommand("CreatePlayerAccount", Connection))
                {
                    CreatePlayerAccount.CommandType = CommandType.StoredProcedure;
                    CreatePlayerAccount.Parameters.AddWithValue("@p_Nickname", p_Nickname);
                    CreatePlayerAccount.Parameters.AddWithValue("@p_Password", p_Password);
                    CreatePlayerAccount.ExecuteNonQuery();
                    Player NewPlayerAccount = new Player()
                    {
                        Name = p_Nickname,
                    };
                    return NewPlayerAccount;
                }
            }
        }
        internal static bool IsNicknameTaken(string p_Nickname)
        {
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();
                using (SqlCommand CheckNickname = new SqlCommand("SELECT 1 FROM Player WHERE Nickname = @Nickname", Connection))
                {
                    CheckNickname.Parameters.AddWithValue("@Nickname", p_Nickname);
                    return CheckNickname.ExecuteScalar() != null;
                }
            }
        }
        internal static string GetPasswordByNickname(string p_Nickname)
        {
            string Password = null;

            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();
                using (SqlCommand GetPassword = new SqlCommand("SELECT Password FROM Player WHERE Nickname = @Nickname", Connection))
                {
                    GetPassword.Parameters.AddWithValue("@Nickname", p_Nickname);

                    using (SqlDataReader Reader = GetPassword.ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                            Password = Reader["Password"].ToString();
                        }
                    }
                }
            }

            return Password;
        }

        public static List<LeaderBoardData> ShowLeaderBoard()
        {
            List<LeaderBoardData> leaderboardDataList = new List<LeaderBoardData>();

            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();
                string ShowLeaderboard = "SELECT * FROM [dbo].[Leaderboard]";

                using (SqlCommand ShowLeaderBoardCommand = new SqlCommand(ShowLeaderboard, Connection))
                {
                    SqlDataReader Reader = ShowLeaderBoardCommand.ExecuteReader();

                    int Place = 0;
                    while (Reader.Read())
                    {
                        string PlayerName = Reader["Nickname"].ToString();
                        int Points = Convert.ToInt32(Reader["Points"]);
                        Place++;

                        leaderboardDataList.Add(new LeaderBoardData
                        {
                            NickName = PlayerName,
                            Score = Points.ToString(),
                            Rank = Place.ToString()
                        });
                    }
                }
            }

            return leaderboardDataList;
        }

        internal static List<PlayerStats> GetPlayerGameStats(string nickname)
        {
            List<PlayerStats> playerStatsList = new List<PlayerStats>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string selectedPlayerGameState = $"SELECT * FROM [dbo].[PlayerGameStats] WHERE Nickname like '{nickname}'";

                using (SqlCommand showPlayerGameStatsCommand = new SqlCommand(selectedPlayerGameState, connection))
                {
                    SqlDataReader reader = showPlayerGameStatsCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        PlayerStats playerStats = new PlayerStats
                        {
                            Nickname = nickname,
                            TotalGames = Convert.ToInt32(reader["TotalGames"]),
                            TotalWins = Convert.ToInt32(reader["TotalWins"]),
                            TotalLosses = Convert.ToInt32(reader["TotalLosses"]),
                            TotalUnfinishedGames = Convert.ToInt32(reader["TotalUnfinishedGames"]),
                            TotalDraws = Convert.ToInt32(reader["TotalDraws"]),
                            SuccessRate = Convert.ToDecimal(reader["SuccessRate"])
                        };

                        playerStatsList.Add(playerStats);
                    }
                }
            }

            return playerStatsList;
        }
        internal static void CreateGameTableEntry(GameType p_GameType, int p_Rows, int p_Columns, int p_WinCondition)
        {
            string GameTypeString = p_GameType.ToString();

            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();
                using (SqlCommand CreateGame = new SqlCommand("SaveGame", Connection))
                {
                    CreateGame.CommandType = CommandType.StoredProcedure;

                    CreateGame.Parameters.AddWithValue("@p_GameType", GameTypeString);
                    CreateGame.Parameters.AddWithValue("@p_Rows", p_Rows);
                    CreateGame.Parameters.AddWithValue("@p_Columns", p_Columns);
                    CreateGame.Parameters.AddWithValue("@p_WinCondition", p_WinCondition);
                    CreateGame.ExecuteNonQuery();

                }
            }
        }
        internal static int CreateMatchTableEntry(int p_GameID)
        {
            int MatchID = -1;

            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();
                using (SqlCommand CreateMatch = new SqlCommand("CreateMatch", Connection))
                {
                    CreateMatch.CommandType = CommandType.StoredProcedure;
                    CreateMatch.Parameters.AddWithValue("@p_GameID", p_GameID);
                    CreateMatch.Parameters.AddWithValue("@p_Winner", "Noch keiner");
                    CreateMatch.Parameters.AddWithValue("@p_Gamestate", "Gestartet");

                    MatchID = Convert.ToInt32(CreateMatch.ExecuteScalar());

                }
            }
            return MatchID;
        }
        internal static bool LoginPlayerAccount(string p_Nickname = null, string p_Password = null)
        {
            string Nickname = p_Nickname;
            string Password = p_Password;
            bool IsPlayerLoggedIn = false;

            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();
                using (SqlCommand Login = new SqlCommand("LoginPlayerAccount", Connection))
                {
                    Login.CommandType = CommandType.StoredProcedure;
                    Login.Parameters.AddWithValue("@nickname", Nickname);
                    Login.Parameters.AddWithValue("@password", Password);

                    object Result = Login.ExecuteScalar();

                    if(Result != null && Result != DBNull.Value && (int)Result == 1)
                    {
                        IsPlayerLoggedIn = true;
                    }
                    
                }
            }
            return IsPlayerLoggedIn;
        }
        internal static void AddPlayerToPlayerMatch(List<Player> p_Players)
        {

            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();
                foreach (Player Player in p_Players)
                {
                    using (SqlCommand AddPlayerToPlayerMatch = new SqlCommand("AddPlayerToPlayerMatch", Connection))
                    {
                        AddPlayerToPlayerMatch.CommandType = CommandType.StoredProcedure;

                        AddPlayerToPlayerMatch.Parameters.AddWithValue("@p_PlayerID", GetIDByValue("Player", "NickName", Player.Name));
                        AddPlayerToPlayerMatch.Parameters.AddWithValue("@p_MatchID", GetLastID("Match"));

                        AddPlayerToPlayerMatch.ExecuteNonQuery();
                    }
                }
            }
        }
        internal static void SaveMove(Player p_Player, int p_Row, int p_Col)
        {
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();
                using (SqlCommand SaveMove = new SqlCommand("SaveMove", Connection))
                {
                    SaveMove.CommandType = CommandType.StoredProcedure;

                    SaveMove.Parameters.AddWithValue("@p_PlayerMatchID", GetIDByValue("PlayerMatch", "PlayerID", p_Player.PlayerIdent));
                    SaveMove.Parameters.AddWithValue("@p_GameID", GetLastID("Game"));
                    SaveMove.Parameters.AddWithValue("@p_Row", p_Row);
                    SaveMove.Parameters.AddWithValue("@p_Col", p_Col);
                    SaveMove.Parameters.AddWithValue("@p_Sign", p_Player.Sign);

                    SaveMove.ExecuteNonQuery();
                }
            }
        }
        internal static void SaveMatch(Player p_Winner, GameState p_GameState)
        {
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();
                using (SqlCommand SaveMatch = new SqlCommand("SaveMatch", Connection))
                {
                    SaveMatch.CommandType = CommandType.StoredProcedure;
                    SaveMatch.Parameters.AddWithValue("@p_MatchID", GetLastID("Match"));
                    SaveMatch.Parameters.AddWithValue("@p_GameID", GetLastID("Game"));

                    if (p_Winner != null)
                    {
                        SaveMatch.Parameters.AddWithValue("@p_Winner", p_Winner.PlayerIdent);
                    }
                    else
                    {
                        SaveMatch.Parameters.AddWithValue("@p_Winner", "Unentschieden");
                    }

                    string GamestateToString = Convert.ToString(p_GameState);
                    SaveMatch.Parameters.AddWithValue("@p_Gamestate", GamestateToString);
                    SaveMatch.Parameters.AddWithValue("@p_IsDraw", GamestateToString == "IsDraw" ? 1 : 0);
                    SaveMatch.Parameters.AddWithValue("@p_IsUnfinished", GamestateToString == "IsRunning" ? 1 : 0);

                    SaveMatch.ExecuteNonQuery();

                }
            }
        }
        internal static int GetIDByValue(string p_TableName, string p_ColumnName, object p_SearchValue)
        {
            if (p_SearchValue == null)
            {
                throw new ArgumentNullException(nameof(p_SearchValue));
            }

            int Id = -1;

            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();
                string GetIdByValueQuery = $"SELECT MAX(Ident) FROM {p_TableName} WHERE {p_ColumnName} = @SearchValue";

                using (SqlCommand GetIdByValueCommand = new SqlCommand(GetIdByValueQuery, Connection))
                {
                    GetIdByValueCommand.Parameters.Add(new SqlParameter("@SearchValue", p_SearchValue));

                    var Result = GetIdByValueCommand.ExecuteScalar();
                    if (Result != DBNull.Value)
                    {
                        Id = Convert.ToInt32(Result);
                    }

                }
            }

            return Id;
        }
        internal static int GetLastID(string tableName)
        {
            int LastID = -1;

            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();
                string Query = $"SELECT MAX(Ident) FROM {tableName}";

                using (SqlCommand SelectLastIdent = new SqlCommand(Query, Connection))
                {
                    var result = SelectLastIdent.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        LastID = Convert.ToInt32(result);
                    }
                }
            }
            return LastID;
        }
    }
}
