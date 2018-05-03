import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';

import {JoinPlayer, Player, GameItem, GameBoard} from './models/GameModels';


@Injectable()
export class GameService {

  private hostUrl:String = "http://tictactoe.web:8069/api/game/";
  private headers = new HttpHeaders();

  constructor(private http: HttpClient) {
    this.headers.append('Content-Type', 'application/json');
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
    return this.http.post(url, player, {headers: this.headers});
  }

  joinGame(joinPlayer:JoinPlayer): Observable<Object>{
    let url = this.hostUrl + 'postjoingame';
    return this.http.post(url, joinPlayer, {headers: this.headers});
  }

  getGameBoards(gameCode:String): Observable<GameBoard[]>{
    return this.http.get<GameBoard[]>(this.hostUrl + 'getgameboards?gamecode=' + gameCode);
  }

}
