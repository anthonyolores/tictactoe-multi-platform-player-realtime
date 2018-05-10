import { Component, OnInit, ViewChild } from '@angular/core';
import { GameService } from '../game.service';
import {JoinPlayer, Player, GameItem, GameBoard} from '../models/GameModels';
import * as $ from 'jquery';
import { SignalR, SignalRConnection } from 'ng2-signalr';

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
  public boardStyle = {Code:'', CellColor:''};
  
  @ViewChild('name') nameElement;

  constructor(private gameService:GameService, private _signalR: SignalR) {
   }
  public board = ['', '', '', '', '', '', '', '', ''];
  ngOnInit() {
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
    this.clearBoard();
    this.boardStyle.Code = "O";
    this.boardStyle.CellColor = "blue";
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
    this.myBoard.Moves += (this.myBoard.Moves.length > 0? "," : "") + cell.toString();
    this.gameService.makeMove(this.myBoard).subscribe(data => {      

      let id = "#cell" + cell.toString();
      $(id).addClass(this.boardStyle.CellColor);
      $(id + "> .cell-text").text(this.boardStyle.Code);   

      if(data){
        alert("You Win");          
      }
      else{
      }
    }, err => {
      console.log(err);  
  });
  }

  homePage(event){
    this.showBoard = false;
    this.showGames = false;
  }

  clearBoard(){
    for(let i = 0; i < 9; i++){
      let id = "#cell" + i.toString();
      $(id).removeClass("blue");
      $(id).removeClass("green");
      $(id + "> .cell-text").text("");   
    }
  }



}
