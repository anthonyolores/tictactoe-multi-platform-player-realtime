using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models
{
	public class Player
	{
		int playerId;
		string moves;
		string playerName;

		public Player(Int32 playerId, String moves)
		{
			this.PlayerId = playerId;
			this.Moves = moves;
		}

		public String Moves { get => this.moves; set => this.moves = value; }
		public Int32 PlayerId { get => this.playerId; set => this.playerId = value; }
		public String PlayerName { get => this.playerName; set => this.playerName = value; }
	}
}