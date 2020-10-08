import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IResetModel } from 'src/Models/ResetModel';
import { Authservice } from '../Authservice';

@Component({
  selector: 'app-forgotpassword',
  templateUrl: './forgotpassword.component.html',
  styleUrls: ['./forgotpassword.component.css'],
  providers:[Authservice]
})
export class ForgotpasswordComponent implements OnInit {
 model:IResetModel={email:null,password:null,confirmpassword:null};
 error:string;
  constructor(private route:Router,
              private _auth:Authservice) { }

  ngOnInit(): void {
  }

  Reset(){
    console.log(this.model);
    this._auth.Resetpassword(this.model).subscribe(x=>{
      if(x["err"]=="Success"){
        this.route.navigate(['Updatepassword']);
      }
      if(x["err"]=="Fail"){
        this.error="fail"
      }
      if(x["err"]=="Notfound"){
        this.error="No user exist"
      }
    })
    }
}
