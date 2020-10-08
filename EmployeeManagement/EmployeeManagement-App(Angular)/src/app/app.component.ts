import { Component, OnInit } from '@angular/core';
import { Authservice } from './Authservice';
import * as signalR from '@aspnet/signalr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers:[Authservice]
})
export class AppComponent implements OnInit {
  isloogedin:boolean;
  Notifications:string[]=[""];

  private hubConnection: signalR.HubConnection
  constructor(private _auth:Authservice){}

  
  


  ngOnInit(): void {

    
     this.isloogedin= this._auth.checklogin();
     console.log(this.Notifications);
  }


  title = 'Employeemanagement';

Logout(){

  localStorage.removeItem('token');
  localStorage.removeItem('role');
  localStorage.removeItem('email');
  window.location.reload();
  }
}
