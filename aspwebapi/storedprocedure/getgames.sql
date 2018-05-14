Alter procedure getGames                
as         
Begin
	select COUNT(tictactoe.game_code) as no_players, tictactoe.game_code, MAX(player.player_name) AS player_name 
	from tictactoe
	inner join board on board.game_code = tictactoe.game_code 
	inner join player on player.player_id = board.board_id
	group by tictactoe.game_code
	having COUNT(tictactoe.game_code) < 2
	order by player_name
End 