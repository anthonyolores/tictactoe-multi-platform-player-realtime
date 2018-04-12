using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapi.Models;

namespace webapi.Controllers
{
    public class GameController : ApiController
    {
		// GET api/values
		public IEnumerable<string> Get2()
		{
			DataAccess access = new DataAccess();
			access.CreateGame("Yoshiaki Arigato");
			return new string[] { "value2", "value2" };
		}
		// POST api/game/creategame
		[System.Web.Http.Description.ResponseType(typeof(String))]
        public IHttpActionResult CreateGame([FromBody] String name)
        {
			String result = "";

			 try
            {
				if (this.ModelState.IsValid)
				{
					DataAccess access = new DataAccess();
					access.CreateGame(name);

					result = "New Game has been created Successfully <br>" + 
						"Invite your friends to play with you using this code <b>" + access.GameCode + "</b>";
				}
            }
            catch (Exception e)
            {
                result = "Creating game has error";
            }

            return CreatedAtRoute("DefaultApi", new { id = 1 }, result);
        }

		[System.Web.Http.Description.ResponseType(typeof(String))]
        public IHttpActionResult JoinGame([FromBody] JoinPlayer player)
        {
			String result = "";

			 try
            {
				if (this.ModelState.IsValid)
				{
					DataAccess access = new DataAccess();
					access.JoinGame(player);

					result = "You have successfully joint the game(" + player.GameCode + ")";
				}
            }
            catch (Exception e)
            {
                result = "Joining game has error";
            }

            return CreatedAtRoute("DefaultApi", new { id = 1 }, result);
        }
    }
}
