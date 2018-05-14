using System;  
using System.Collections.Generic;  
using System.Data;  
using System.Data.SqlClient;  
using System.Linq;  
using System.Threading.Tasks;  

namespace webapi.Models
{
	public class DataAccess
	{
		string connectionString = "Data Source=APL-ANTHONY;Initial Catalog=DemoDB;Integrated Security=True"; 
		string gameCode = "";

		public String GameCode { get => this.gameCode; set => this.gameCode = value; }

		public IEnumerable<GetGameBoard> CreateGame(string player_name)  
        {  
            using (SqlConnection con = new SqlConnection(this.connectionString))  
            {  
                SqlCommand cmd = new SqlCommand("newGame", con);  
                cmd.CommandType = CommandType.StoredProcedure;  
				this.gameCode = RandomString(5);
                cmd.Parameters.AddWithValue("@player_name", player_name);  
                cmd.Parameters.AddWithValue("@game_code", this.gameCode); 
  
                con.Open();  
                cmd.ExecuteNonQuery();  
                con.Close();  

				return this.GetGameBoards(this.gameCode);
            }  
		}
       //To View all employees details    
        public IEnumerable<Object> GetGames()  
        {  
            List<GetGamesItem> gameList = new List<GetGamesItem>();  
  
            using (SqlConnection con = new SqlConnection(connectionString))  
            {  
                SqlCommand cmd = new SqlCommand("getGames", con);  
                cmd.CommandType = CommandType.StoredProcedure;  
                con.Open();  
                SqlDataReader rdr = cmd.ExecuteReader();  
                while (rdr.Read())  
                {  
					GetGamesItem item = new GetGamesItem(Convert.ToInt32(rdr["no_players"]), rdr["game_code"].ToString(), rdr["player_name"].ToString());
                    gameList.Add(item);  
                }  
                con.Close();  
            }  
            return gameList;  
        } 

		public IEnumerable<GetGameBoard> GetGameBoards(String gameCode)  
        {  
            List<GetGameBoard> gameBoardList = new List<GetGameBoard>();  
  
            using (SqlConnection con = new SqlConnection(connectionString))  
            {  
                SqlCommand cmd = new SqlCommand("getGameBoards", con);  
                cmd.CommandType = CommandType.StoredProcedure;  
				cmd.Parameters.AddWithValue("@game_code", gameCode);  
                con.Open();  
                SqlDataReader rdr = cmd.ExecuteReader();  
                while (rdr.Read())  
                {  
					GetGameBoard gameBoard = new GetGameBoard();
					gameBoard.BoardId = Convert.ToInt32(rdr["board_id"]); 
					gameBoard.PlayerId = Convert.ToInt32(rdr["player_id"]); 
					gameBoard.Moves = rdr["moves"].ToString();
					gameBoard.GameCode = rdr["game_code"].ToString();

					gameBoardList.Add(gameBoard);
                }  
                con.Close();  
            }  
            return gameBoardList;  
        } 

		public void JoinGame(JoinPlayer player)  
        {  
            using (SqlConnection con = new SqlConnection(this.connectionString))  
            {  
                SqlCommand cmd = new SqlCommand("joinGame", con);  
                cmd.CommandType = CommandType.StoredProcedure;  
                cmd.Parameters.AddWithValue("@player_name", player.PlayerName);  
                cmd.Parameters.AddWithValue("@game_code", player.GameCode); 
  
                con.Open();  
                cmd.ExecuteNonQuery();  
                con.Close();  
            }  
		}

		public void MakeMove(Board board)  
        {  
            using (SqlConnection con = new SqlConnection(this.connectionString))  
            {  
                SqlCommand cmd = new SqlCommand("makeMove", con);  
                cmd.CommandType = CommandType.StoredProcedure;  
                cmd.Parameters.AddWithValue("@board_id", board.BoardId);  
                cmd.Parameters.AddWithValue("@moves", board.Moves); 
				cmd.Notification = null;
  
                con.Open();  
                cmd.ExecuteNonQuery();  
                con.Close();  
            }  
		}

		public void AddWinner(string game_code, int player_id)  
        {  
            using (SqlConnection con = new SqlConnection(this.connectionString))  
            {  
                SqlCommand cmd = new SqlCommand("addWinner", con);  
                cmd.CommandType = CommandType.StoredProcedure;  
                cmd.Parameters.AddWithValue("@game_code", game_code);  
                cmd.Parameters.AddWithValue("@player_id", player_id); 
  
                con.Open();  
                cmd.ExecuteNonQuery();  
                con.Close();  
            }  
		}

		public static string RandomString(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			var random = new Random();
			return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
		}
        
	}
}