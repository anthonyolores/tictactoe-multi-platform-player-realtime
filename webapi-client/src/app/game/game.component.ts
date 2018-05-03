import { Component, OnInit, ViewChild } from '@angular/core';
import { GameService } from '../game.service';
import {JoinPlayer, Player, GameItem, GameBoard} from '../models/GameModels';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.scss']
})
export class GameComponent implements OnInit {

  public showBoard:boolean = false;
  public showGames:boolean = false;
  public playerName: string = "";
  public games:GameItem[];
  @ViewChild('name') nameElement;

  constructor(private gameService:GameService) {
   }
  public board = ['', '', '', '', '', '', '', '', ''];
  ngOnInit() {
    this.getGames();
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
      let p:Player = {
        playerName: this.playerName.toString(),
        moves: ''
      }
      this.gameService.createGame(p).subscribe(data => {
        this.showBoard = true;
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
    let jp:JoinPlayer = {
      playerName: this.playerName.toString(),
      gameCode: this.games[index].gameCode
    }
    this.gameService.joinGame(jp).subscribe(data => {
      //this.showBoard = true;
        //console.log(data);      
        // this.gameService.getGameBoards(this.games[index].gameCode).subscribe(data => {
        //     console.log(data);
        //     this.showGames = false;
        //   }, err => {
        //     console.log(err);  
        // });

      }, err => {
        console.log(err);  
    });
  }

  homePage(event){
    this.showBoard = false;
    this.showGames = false;
  }

}
