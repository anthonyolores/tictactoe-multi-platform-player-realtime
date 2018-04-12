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

		public void CreateGame(string player_name)  
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
            }  
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

		public static string RandomString(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			var random = new Random();
			return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
		}
        
	}
}