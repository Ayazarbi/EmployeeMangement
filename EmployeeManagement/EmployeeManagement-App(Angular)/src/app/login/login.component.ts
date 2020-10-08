import { ThrowStmt } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import * as signalR from '@aspnet/signalr';
import { IUser } from 'src/Models/User';
import { Authservice } from '../Authservice';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers:[Authservice]
})
export class LoginComponent implements OnInit {
  User:IUser={email:null,password:null,rememberme:false}
  error:string;
  hubConnection:signalR.HubConnection;

  constructor(private _auth:Authservice
              ,private route:Router) { }

  ngOnInit(): void {
  }

  login(){

    
      
    
  

    //console.log(this.login.email);

    //console.log(emp);
    // this._auth.login(this.User).subscribe(
    //   (res:any) => {
        
    //     localStorage.setItem('token',res.token);
    //     var token = localStorage.getItem('token');
    //     const payLoad = JSON.parse(window.atob(token.split('.')[1]));
    //     console.log(payLoad);
    //     localStorage.setItem('UserID',payLoad['UserID']);
    //     localStorage.setItem('role',payLoad['role']);
    //     this.hubConnection =  new signalR.HubConnectionBuilder()
    //   .configureLogging(signalR.LogLevel.Debug)
    //   .withUrl("http://localhost:1234/Myhub", {
    //     skipNegotiation: true,
    //     transport: signalR.HttpTransportType.WebSockets,
    //     accessTokenFactory:()=>localStorage.getItem('token')
    //   })
    //   .build();
    //   this.hubConnection
    //     .start()
    //     .then(() => {console.log('Connection started')
    //     this.hubConnection.send('Setrole',localStorage.getItem('role').toString()).then(()=>{
      
    //     })
    //     this.hubConnection.on('Receive',(value)=>{
    //       console.log(value)
         
          
    //     })
        
    //   })
    //     .catch(err => console.log('Error while starting connection: ' + err))


    //      },
    //   err => {
    //     if(err.status === 400){
    //       this.error="Invalid";
    //     }

    //   }
    // );


    this._auth.login(this.User).subscribe(result=>{
        console.log(result)

      if(result["result"]){
        
       
        if(result["result"].succeeded){
          localStorage.setItem('token',result["token"])
          var token = localStorage.getItem('token');
          console.log(token)
          
          const payLoad = JSON.parse(window.atob(token.split('.')[1]));
          
          console.log(payLoad)
          localStorage.setItem('UserID',payLoad['UserID']);
          localStorage.setItem('email',payLoad['Email']);
            localStorage.setItem('role',payLoad['role'][0]); 
              
            console.log(payLoad);
              this.route.navigate(['/home/Employees'])
                }
                else if(!result["result"].succeeded) {
                  this.error="Invalid Credentials"
                }


      }
      else{
        this.error="Use not found"
      }
   })
}
}
