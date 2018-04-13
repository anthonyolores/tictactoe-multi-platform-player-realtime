Create procedure addWinner       
(                 
	@game_code VARCHAR(50),
	@player_id int
)        
as         
Begin
	Update tictactoe set game_winner_id = @player_id where game_code = @game_code;
End 