import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';

import {JoinPlayer, Player, GameItem, GameBoard} from './models/GameModels';


@Injectable()
export class GameService {

  private hostUrl:String = "http://localhost:56168/api/game/";

  constructor(private http: HttpClient) {
   }

  handleError(arg0: any): any {
    throw new Error("Method not implemented.");
  }

  getGames(): Observable<GameItem[]>{
    return this.http
      .get<GameItem[]>(this.hostUrl + 'getgames');
  }

  createGame(player:Player){
    let url = this.hostUrl + 'postcreategame';
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');
    return this.http.post(url, player, {headers: headers});
  }

  joinGame(joinPlayer:JoinPlayer){
    let url = this.hostUrl + 'postjoingame';
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');
    return this.http.post(url, joinPlayer, {headers: headers});
  }

  makeMove(board:GameBoard){  
    let url = this.hostUrl + 'postmakemove';
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');
    return this.http.post(url, board, {headers: headers});
  }

  getGameBoards(gameCode:String): Observable<GameBoard[]>{
    return this.http.get<GameBoard[]>(this.hostUrl + 'getgameboards?gamecode=' + gameCode);
  }

}
