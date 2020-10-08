import { ThrowStmt } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IDepartment } from 'src/Models/Department';
import { databaseservice } from '../databaseservice';

@Component({
  selector: 'app-adddepartment',
  templateUrl: './adddepartment.component.html',
  styleUrls: ['./adddepartment.component.css'],
  providers:[databaseservice]
})
export class AdddepartmentComponent implements OnInit {
      Department:IDepartment={department_name:"",departmentId:0}
  Role:string;
  AdminRole:string;
  Employeeole:String;
  HRrole:String;

  constructor( private _database:databaseservice,private router:Router) { }

  ngOnInit(): void {

    this.Role=this._database.Getrole();
    console.log(this.Role)
    if(this.Role=="Employee"){
        this.Employeeole=this.Role;

    }
    if(this.Role=="HR"){
      this.HRrole=this.Role;
    }
    if(this.Role=="Admin"){
      this.AdminRole=this.Role;

    }
  }

submit(){

console.log(this.Department);
  this._database.createdepartment(this.Department).subscribe(x=>this.router.navigate(['home/Departments']));
}

}
