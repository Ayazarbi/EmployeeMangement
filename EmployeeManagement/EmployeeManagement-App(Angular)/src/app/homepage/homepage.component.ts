import { Component, OnInit } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { Authservice } from '../Authservice';
import { databaseservice } from '../databaseservice';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css'],
  providers:[databaseservice,Authservice]
})
export class HomepageComponent implements OnInit {
  isloogedin:boolean;
  Notifications:string[]=[""];
  Count:number=0;
  private hubConnection: signalR.HubConnection
  constructor(private _auth:Authservice,
              private _database:databaseservice) { }

  ngOnInit(): void {
    this.isloogedin= this._auth.checklogin();
    console.log(this.Notifications);
    var dept;

        this.hubConnection =  new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Debug)
      .withUrl("http://localhost:1234/Myhub", {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
        accessTokenFactory:()=>localStorage.getItem('token')
      })
      .build();
      this.hubConnection
        .start()
        .then(() => {
          this._database.getallemployees().subscribe(x=>{
            for (var key in x) {
                if(x[key].email==(localStorage.getItem('email')).toString()){
                   dept=x[key].departmentId;
                  console.log(dept)
                }

              
            }
            this.hubConnection.send('Setrole',localStorage.getItem('role').toString(),dept);
            
          })
          
          
         
        console.log('Connection started');
        
        this.hubConnection.on('Receive',(value)=>{
          console.log("Receive called");
          this.Count++;
          
          

           console.log(dept);
          
          
          // else{
          //   this.hubConnection.send('Setrole',localStorage.getItem('role').toString());
          // }  
          
          console.log(value)
          this.Notifications.push(value);
        })
      }).catch(err => console.log('Error while starting connection: ' + err))
      }
       


         
  
  
Logout(){

  localStorage.removeItem('token');
  localStorage.removeItem('role');
  localStorage.removeItem('email');
  window.location.reload();
  }

}
