Create procedure MakeMove         
(                 
	@board_id int,
	@moves VARCHAR(50)
)        
as         
Begin
	Update board set moves = @moves where board_id = @board_id;
End 