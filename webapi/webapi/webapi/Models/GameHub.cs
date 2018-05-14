using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace webapi.Models
{
	public class GameHub : Hub
	{
		public void  SendMessage(GetGameBoard opponentBoard)
        {
			this.Clients.All.opponentMove(opponentBoard);
        }

		public static void  JoinGame(JoinPlayer jp)
        {
			var hubContext = GlobalHost.ConnectionManager.GetHubContext<GameHub>();
			hubContext.Clients.All.playerJoinGame(jp);
        }

		
		public void  OpponentWon(Boolean won)
        {
			//var hubContext = GlobalHost.ConnectionManager.GetHubContext<GameHub>();
			//hubContext.Clients.All.opponentHasWon(won);

				this.Clients.All.opponentHasWon(won);
        }
	}
}