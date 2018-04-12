Create procedure joinGame         
(               
    @game_code VARCHAR(50),     
	@player_name VARCHAR(50)
)        
as         
Begin       
	Insert into dbo.player (player_name) Values (@player_name)
	Insert into dbo.board (game_code, player_id) Values (@game_code, scope_identity())
End 