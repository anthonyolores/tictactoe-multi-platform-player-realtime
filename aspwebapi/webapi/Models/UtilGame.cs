using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models
{
	public class UtilGame
	{
		public static int [,] pattern = new int[8,3] { 
			{1, 2, 3},
			{4, 5, 6},
			{7, 8, 9},
			{1, 4, 7},
			{2, 5, 8},
			{3, 6, 9},
			{1, 5, 9},
			{3, 5, 7}
		};

		public static Boolean checkWin(string moves){
			
			string[] movesArr = moves.Split(new string[] {","}, StringSplitOptions.None);
			int movesCnt = movesArr.Length;
			int matchCounter = 0;

			if(movesCnt > 2){
				for (int i = 0; i < pattern.GetLength(0) && matchCounter < 3; i++){	
					matchCounter = 0;					
					for(int x = 0; x < pattern.GetLength(1) && matchCounter < 3; x++){
						for(int z = 0; z < movesCnt; z++){
							if(Convert.ToInt32(movesArr[z]) == pattern[i, x]){
								matchCounter++;
								break;
							}
						}
					}
				}
			
			}

			return matchCounter > 2? true : false;
		}
	}
}