using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models
{
	public class JoinPlayer
	{
		string playerName;
		string gameCode;

		public String PlayerName { get => this.playerName; set => this.playerName = value; }
		public String GameCode { get => this.gameCode; set => this.gameCode = value; }
	}
}