using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models
{
	public class GetGameBoard
	{
		private int boardId;
		private int playerId;
		private String moves;
		private String gameCode;

		public Int32 BoardId { get => this.boardId; set => this.boardId = value; }
		public Int32 PlayerId { get => this.playerId; set => this.playerId = value; }
		public String Moves { get => this.moves; set => this.moves = value; }
		public String GameCode { get => this.gameCode; set => this.gameCode = value; }
	}
}