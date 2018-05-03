Create procedure getGameBoards
(             
    @game_code VARCHAR(50)
)
as         
Begin
	select * 
	from board
	where game_code = @game_code;
End 