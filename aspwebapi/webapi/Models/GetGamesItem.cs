using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models
{
	public class GetGamesItem
	{
		int noPlayers;
		string gameCode;
		string playerName;

		public GetGamesItem(Int32 noPlayers, String gameCode, String playerName)
		{
			this.NoPlayers = noPlayers;
			this.GameCode = gameCode;
			this.PlayerName = playerName;
		}

		public String PlayerName { get => this.playerName; set => this.playerName = value; }
		public Int32 NoPlayers { get => this.noPlayers; set => this.noPlayers = value; }
		public String GameCode { get => this.gameCode; set => this.gameCode = value; }
	}
}