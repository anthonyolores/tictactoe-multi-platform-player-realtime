import { Component, OnInit, ViewChild } from '@angular/core';
import { GameService } from '../game.service';
import {JoinPlayer, Player, GameItem, GameBoard} from '../models/GameModels';
import * as $ from 'jquery';
import { SignalR, SignalRConnection, IConnectionOptions } from 'ng2-signalr';
import { join } from 'path';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.scss']
})
export class GameComponent implements OnInit {

  public showBoard: boolean = false;
  public showGames: boolean = false;
  public playerName: string = "";
  public games: GameItem[];
  public myBoard: GameBoard;
  public opponent: JoinPlayer;
  public boardStyle = {Code:'', CellColor:''};
  private connection:any;
  private signal:any;
  private playerJoinListener:any;
  private myTurn:boolean;
  
  @ViewChild('name') nameElement;

  constructor(private gameService:GameService, private _signalR: SignalR) {
   }
  public board = ['', '', '', '', '', '', '', '', ''];
  ngOnInit() {
    this.connect();
  }

  connect() {
    let o: IConnectionOptions;
    this.connection = this._signalR.createConnection();
    this.connection.start().then((c) => {
    this.signal = c;
      let movesListener = c.listenFor('opponentMove');
      let winnerListener = c.listenFor('opponentHasWon');
      this.playerJoinListener = c.listenFor('playerJoinGame');
      movesListener.subscribe((data:any) => {
        let opponentBoard = data as GameBoard;         
        if(opponentBoard.PlayerId != this.myBoard.PlayerId &&
        opponentBoard.GameCode == this.myBoard.GameCode){
          let moves = opponentBoard.Moves.split(",");
          this.changeTurn(true);
          for(let i = 0; i < moves.length; i++){
            this.addMoveToCell("#cell" + moves[i].toString(), 
            (this.boardStyle.Code=="O"?"green":"blue"), 
            (this.boardStyle.Code=="O"?"X":"O"));
          }    
          console.log('Opponents Board');
          console.log(data);
        }
      });
      winnerListener.subscribe((data:any) => {
        if(data === true){
          alert("You Lose! haha");  
          this.clearBoard();
        }
        else if(this.isDraw()){
          alert("It's a Draw");  
          this.clearBoard();
        }
      });
    });
  }

  getGames(){
    this.gameService.getGames().subscribe(data => {
      console.log(data);
      this.games = data;

      }, err => {
      });
  }
  newGame(event){   
    if(this.playerName.length > 0){
      this.clearBoard();
      this.boardStyle.Code = "X";
      this.boardStyle.CellColor = "green";
      $(".board-info").text("Waiting for opponent...");
      let p:Player = {
        PlayerName: this.playerName.toString(),
        Moves: ''
      }
      this.gameService.createGame(p).subscribe(data => {
        this.showBoard = true;
        this.myBoard = data as GameBoard;
        console.log(data);        
        }, err => {
          console.log(err);  
      });

      this.playerJoinListener.subscribe((data:any) => {
        this.opponent = data as JoinPlayer;  
        if(this.opponent.GameCode == this.myBoard.GameCode){
          $(".board-info").text(this.opponent.PlayerName + " has joined, just wait for their first move.");
          console.log('Opponents Board');
          console.log(data);
        }   
      });
    }
    else{
      this.nameElement.nativeElement.focus();
    }
  }

  joinGame(event){
    if(this.playerName.length > 0){
      this.getGames();
      this.showGames = true;
    }
    else{
      this.nameElement.nativeElement.focus();
    } 
  }

  playerJoinGame(index){
    this.myTurn = true;
    this.clearBoard();
    this.boardStyle.Code = "O";
    this.boardStyle.CellColor = "blue";
    $(".board-info").text("Your turn first");
    let jp:JoinPlayer = {
      PlayerName: this.playerName.toString(),
      GameCode: this.games[index].GameCode
    }
    this.gameService.joinGame(jp).subscribe(data => {
      this.showBoard = true;
        console.log(data);      
        this.gameService.getGameBoards(this.games[index].GameCode).subscribe(data => {          
            if(data){
              this.myBoard = data[1];     
            }
            this.showGames = false;
          }, err => {
            console.log(err);  
        });

      }, err => {
        console.log(err);  
    });
  }

  playerMakeMove(cell:number){
    if(this.myTurn){
        this.myBoard.Moves += (this.myBoard.Moves.length > 0? "," : "") + cell.toString();
        this.addMoveToCell("#cell" + cell.toString(), this.boardStyle.CellColor, this.boardStyle.Code);
        this.changeTurn(false);
        this.gameService.makeMove(this.myBoard).subscribe(data => {  
          this.signal.invoke('SendMessage', this.myBoard).then((data:any) => {
            console.log("Send Moves");
            console.log(data);
          });
    
          if(data){
            alert("You Win");   
            this.clearBoard();       
          }

          this.signal.invoke('OpponentWon', data).then((data:any) => {
            console.log("Send Move Result");
            console.log(data);
          });

        }, err => {
          console.log(err);  
      });
    }
  }

  changeTurn(turn:boolean){
    this.myTurn = turn;
    $(".board-info").text(turn?"Your Turn":this.opponent.PlayerName + "'s Turn");
  }
  addMoveToCell(id, color, code){
    if(!$(id).hasClass("blue") && !$(id).hasClass("green")){
      $(id).addClass(color);
      $(id + "> .cell-text").text(code);   
    }
  }

  homePage(event){
    this.showBoard = false;
    this.showGames = false;
  }

  resetBoard(){
    this.myBoard.Moves = "";
    this.clearBoard();
  }

  clearBoard(){
    for(let i = 0; i < 9; i++){
      let id = "#cell" + i.toString();
      $(id).removeClass("blue");
      $(id).removeClass("green");
      $(id + "> .cell-text").text("");   
    }
  }

  isDraw():boolean{
    var draw = true;
    for(let i = 0; i < 9; i++){
      let id = "#cell" + i.toString();
      if(!$(id).hasClass("blue") && !$(id).hasClass("green")){
        draw = false;
        break;
      } 
    }
    return draw;
  }



}
