using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using aspwebapi.Models;

namespace aspwebapi.Controllers
{
    public class GameController : ApiController
    {
		// POST: api/game/newgame
		public IHttpActionResult NewGame(string playerName)
		{
			if (this.ModelState.IsValid){
				DataAccess access = new DataAccess();
				access.newGame(playerName, RandomString(5));
			}

			return null;
		
		}

		public static string RandomString(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			var random = new Random();
			return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
		}
    }
}
