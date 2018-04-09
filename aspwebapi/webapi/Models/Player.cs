using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models
{
	public class Player
	{
		int movesCount = 0;
		int [] moves = new int[5];
		string name = "";
		string symbol = "";
		

		public Player()
		{
		}

		public Int32[] Moves { get => this.moves; set => this.moves = value; }
		public Int32 MovesCount { get => this.movesCount; set => this.movesCount = value; }
		public string Name { get => this.name; set => this.name = value; }
		public String Symbol { get => this.symbol; set => this.symbol = value; }
	}
}