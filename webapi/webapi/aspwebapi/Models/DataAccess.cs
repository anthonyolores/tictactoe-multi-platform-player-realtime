using System;  
using System.Collections.Generic;  
using System.Data;  
using System.Data.SqlClient;  
using System.Linq;  
using System.Threading.Tasks;  

namespace aspwebapi.Models
{
	public class DataAccess
	{
		string connectionString = "Data Source=APL-ANTHONY;Initial Catalog=DemoDB;Integrated Security=True";

		        //To Add new employee record    
        public void newGame(string playerName, string gameCode)  
        {  
            using (SqlConnection con = new SqlConnection(connectionString))  
            {  
                SqlCommand cmd = new SqlCommand("newGame", con);  
                cmd.CommandType = CommandType.StoredProcedure;  
  
                cmd.Parameters.AddWithValue("@game_code", playerName);  
                cmd.Parameters.AddWithValue("@player_name", gameCode); 
  
                con.Open();  
                cmd.ExecuteNonQuery();  
                con.Close();  
            }  
        }  
	}
}