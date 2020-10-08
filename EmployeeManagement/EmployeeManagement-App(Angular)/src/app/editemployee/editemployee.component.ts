import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IDepartment } from 'src/Models/Department';
import { IEmployee } from 'src/Models/Employee';
import { databaseservice } from '../databaseservice';

@Component({
  selector: 'app-editemployee',
  templateUrl: './editemployee.component.html',
  styleUrls: ['./editemployee.component.css'],
  providers:[databaseservice]
})
export class EditemployeeComponent implements OnInit {
  Employee:IEmployee={departmentId:null,name:null,surname:null,address:null,employeeId:null,department:{department_name:null,departmentId:null},contactnumber:null,email:null};
  Id:number;
  Departments:IDepartment[];
  Role:string;
  AdminRole:string;
  Employeeole:String;
  HRrole:String;
  constructor(private route:ActivatedRoute
              ,private _database:databaseservice) { }

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
    this.Id=+this.route.snapshot.paramMap.get('id');
    this._database.getalldepartments().subscribe(x=>this.Departments=x);
    this._database.getemployeebyId(this.Id).subscribe(x=>{this.Employee=x;
      this._database.getdepartmentbyid(Number.parseInt( this.Employee.departmentId)).subscribe(x=>this.Employee.department=x);

    });
     }

  Update(){

    this._database.editemployee(this.Employee,this.Id).subscribe();
  }

}
