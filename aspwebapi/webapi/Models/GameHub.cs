using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace webapi.Models
{
	public class GameHub : Hub
	{
		public void  SendMessage(bool gamewon, string moves)
        {
            //this.Clients.All.NewMessage(Cl_Name, Cl_Message);
			this.Clients.All.broadcastMessage(gamewon, moves);
        }
	}
}