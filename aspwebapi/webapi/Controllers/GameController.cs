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

		//[System.Web.Http.Description.ResponseType(typeof(ResponseGetGames[]))]
		public IEnumerable<Object> GetGames()
		{
			DataAccess access = new DataAccess();
			return access.GetGames();
		}

		public IEnumerable<GetGameBoard> GetGameBoards(String gameCode)
		{
			DataAccess access = new DataAccess();
			return access.GetGameBoards(gameCode);
		}

		[System.Web.Http.Description.ResponseType(typeof(String))]
        public IHttpActionResult PostCreateGame(Player player)
        {
			String result = "";

			 try
            {
				if (this.ModelState.IsValid)
				{
					DataAccess access = new DataAccess();
					access.CreateGame(player.PlayerName);

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
        public IHttpActionResult PostJoinGame(JoinPlayer player)
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

		[System.Web.Http.Description.ResponseType(typeof(String))]
        public IHttpActionResult MakeMove([FromBody] Board board)
        {
			String result = "";

			 try
            {
				if (this.ModelState.IsValid)
				{
					DataAccess access = new DataAccess();
					access.MakeMove(board);

					if(UtilGame.checkWin(board.Moves)){
						access.AddWinner(board.GameCode, board.PlayerId);
						result = "Congratulations you won in the board(" + board.BoardId + ")";
					}
					else{
						result = "You have successfully made your move in the board(" + board.BoardId + ")";
					}

				}
				else{
					result = " if Making move has error";
				}
            }
            catch (Exception e)
            {
                result = "Making move has error";
            }

            return CreatedAtRoute("DefaultApi", new { id = 1 }, result);
        }
    }
}
