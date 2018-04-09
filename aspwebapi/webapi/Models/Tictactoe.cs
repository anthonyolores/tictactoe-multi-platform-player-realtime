using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models
{
	public class Tictactoe
	{
		Boolean status = false;
		Player [] players = new Player[2];
		string session = "";
		int [,] pattern = new int[8,3] { 
			{1, 2, 3},
			{4, 5, 6},
			{7, 8, 9},
			{1, 4, 7},
			{2, 5, 8},
			{3, 6, 9},
			{1, 5, 9},
			{3, 5, 7}
		};

		public Tictactoe(Player [] players){
			this.players = players;
		}
		
		public void addMove(int playerIndex, int moveIndex){		
			this.players[playerIndex].Moves[this.players[playerIndex].MovesCount] = moveIndex;
			this.players[playerIndex].MovesCount++;
		}

		public Boolean checkWin(int playerIndex){
			
			int movesCnt = this.players[playerIndex].MovesCount;
			int matchCounter = 0;

			if(movesCnt > 2){
				for (int i = 0; i < this.pattern.GetLength(0) && matchCounter < 3; i++){	
					matchCounter = 0;					
					for(int x = 0; x < this.pattern.GetLength(1) && matchCounter < 3; x++){
						for(int z = 0; z < movesCnt; z++){
							if(this.players[playerIndex].Moves[z] == this.pattern[i, x]){
								matchCounter++;
								break;
							}
						}
					}
				}
			
			}

			return matchCounter > 2? true : false;
		}

		public Boolean Status { get => this.status; set => this.status = value; }
		public Player[] Players { get => this.players; set => this.players = value; }
	}
}