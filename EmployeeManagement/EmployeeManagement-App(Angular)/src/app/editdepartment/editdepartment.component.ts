import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IDepartment } from 'src/Models/Department';
import { routes } from '../app.module';
import { databaseservice } from '../databaseservice';

@Component({
  selector: 'app-editdepartment',
  templateUrl: './editdepartment.component.html',
  styleUrls: ['./editdepartment.component.css'],
  providers:[databaseservice]
})
export class EditdepartmentComponent implements OnInit {
  Department:IDepartment={department_name:null,departmentId:null}
  id:number;
  Role:string;
  AdminRole:string;
  Employeeole:String;
  HRrole:String;

  constructor(private route:ActivatedRoute,
              private _database:databaseservice,
              private router:Router) { }

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
  this.id=+(this.route.snapshot.paramMap.get('id'));
  this._database.getdepartmentbyid(this.id).subscribe(x=>{
    this.Department=x;
  })

  }

  update(){
  this._database.editdepartment(this.id,this.Department).subscribe();
  this.router.navigate(["/home/Departments"]);

   }

}
