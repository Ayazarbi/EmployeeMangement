import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Injectable } from "@angular/core";
import { IUser } from 'src/Models/User';
import { IResetModel } from 'src/Models/ResetModel';

@Injectable({
  providedIn:"root"
})
  export class Authservice{

    constructor(private http:HttpClient){

    }

    login(user:IUser){

      return this.http.post("http://localhost:1234/Login",user)

  }

  checklogin():boolean{

    if(localStorage.getItem('token')==null){
      return false
    }
    else{
      return true;
    }

  }

  Resetpassword(model:IResetModel){

    return this.http.post("http://localhost:1234/ResetPassword",model)

  }



  }
