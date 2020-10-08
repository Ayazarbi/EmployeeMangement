import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IEmployee } from 'src/Models/Employee';
import { databaseservice } from '../databaseservice';

@Component({
  selector: 'app-listofemployees',
  templateUrl: './listofemployees.component.html',
  styleUrls: ['./listofemployees.component.css'],
  providers:[databaseservice]
})
export class ListofemployeesComponent implements OnInit {
  listofEmployee:IEmployee[]=[];
  Role:string;
  AdminRole:string;
  Employeeole:String;
  HRrole:String;
  ActiveEmail:string;
  constructor(private _databse:databaseservice,
              private route:ActivatedRoute,
            ) {


  }

  ngOnInit(): void {

    var dept;
    var depts;


    
    this.Role=this._databse.Getrole();
    if(this.Role=="Employee"){
        this.Employeeole=this.Role;
        this.ActiveEmail=localStorage.getItem('email');
       this._databse.getalldepartments().subscribe(x=>depts=x);
         this._databse.getallemployees().subscribe(x=>{

          for (var emp in x) {
           if(x[emp].email==this.ActiveEmail){

            dept=x[emp].departmentId;

          }
          if(x[emp].departmentId==dept){
            x[emp].department=dept;
            this.listofEmployee.push(x[emp]);
          }
          

        }
           for(var emp in this.listofEmployee){
          this.listofEmployee[emp].department=depts.find(x=>x.departmentId==  this.listofEmployee[emp].departmentId);

    }

      })
    }
  
    else{
    if(this.Role=="HR"){
      this.HRrole=this.Role;
    }
    if(this.Role=="Admin"){
      this.AdminRole=this.Role;

    }
    
    this._databse.getalldepartments().subscribe(x=>depts=x);
    this._databse.getallemployees().subscribe(x=>{
    this.listofEmployee=x;

    for(var emp in this.listofEmployee){
          this.listofEmployee[emp].department=depts.find(x=>x.departmentId==  this.listofEmployee[emp].departmentId);

    }
  })
  }
  }
  DeleteEmployee(id:number){

  this._databse.deleteemployee(id).subscribe(x=>
    window.location.reload());
  

  }



}

