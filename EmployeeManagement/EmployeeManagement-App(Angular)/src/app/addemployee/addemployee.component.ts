import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HubConnection } from '@aspnet/signalr';
import { stringify } from 'querystring';

import { IDepartment } from 'src/Models/Department';
import { IEmployee } from 'src/Models/Employee';
import { databaseservice } from '../databaseservice';

@Component({
  selector: 'app-addemployee',
  templateUrl: './addemployee.component.html',
  styleUrls: ['./addemployee.component.css'],
  providers:[databaseservice]
})
export class AddemployeeComponent implements OnInit {
  Error:string;
  Departmetns:IDepartment[];
  Role:string;
  AdminRole:string;
  Employeeole:String;
  HRrole:String;

  Employee:IEmployee={employeeId:0,departmentId:null,name:null,surname:null,address:null,department:null,contactnumber:null,email:null};
  constructor(private _database:databaseservice,private router:Router) {


  }

  ngOnInit(): void {
    
    this.Role=this._database.Getrole();
    if(this.Role=="Employee"){
        this.Employeeole=this.Role;

    }
    if(this.Role=="HR"){
      this.HRrole=this.Role;
    }
    if(this.Role=="Admin"){
      this.AdminRole=this.Role;

    }

this._database.getalldepartments().subscribe(x=>{
  this.Departmetns=x;
})


  }


  submit():void{

    console.log(this.Employee);
    Number.parseInt(this.Employee.departmentId);
    console.log(this.Employee);
   this._database.creatremployee(this.Employee).subscribe(x=>{
    this.router.navigate(['/home/Employees']
    )},
    err=>{
      if(err.status===400){
        this.Error="User Already Exist with same Email"
      }
    }

      

    );


  }

}
