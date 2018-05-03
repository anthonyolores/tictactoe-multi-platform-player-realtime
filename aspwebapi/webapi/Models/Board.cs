using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models
{
	public class Board
	{
		int boardId;
		int playerId;
		string moves;
		string gameCode;

		public String GameCode { get => this.gameCode; set => this.gameCode = value; }
		public String Moves { get => this.moves; set => this.moves = value; }
		public Int32 PlayerId { get => this.playerId; set => this.playerId = value; }
		public Int32 BoardId { get => this.boardId; set => this.boardId = value; }
	}
}