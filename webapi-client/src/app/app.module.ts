import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { GameService } from './game.service';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { GameComponent } from './game/game.component';

import { SignalRModule } from 'ng2-signalr';
import { SignalRConfiguration, ConnectionTransport } from 'ng2-signalr';

const config = new SignalRConfiguration();
config.hubName = 'Ng2SignalRHub';  //default
config.qs = { user: 'donald' };
config.url = 'http://ng2-signalr-backend.azurewebsites.net/';
// Specify one Transport: config.transport = ConnectionTransports.longPolling; or fallback options with order like below. Defaults to best avaliable connection.
config.transport = [ConnectionTransports.webSockets, ConnectionTransports.longPolling];

@NgModule({
  declarations: [
    AppComponent,
    GameComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    SignalRModule
  ],
  providers: [GameService],
  bootstrap: [AppComponent]
})
export class AppModule { 
}
