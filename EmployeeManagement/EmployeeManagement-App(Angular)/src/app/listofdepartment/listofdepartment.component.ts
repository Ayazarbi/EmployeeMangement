import { Component, OnInit } from '@angular/core';
import { IDepartment } from 'src/Models/Department';
import {databaseservice} from 'src/app/databaseservice'
import { Router } from '@angular/router';

@Component({
  selector: 'app-listofdepartment',
  templateUrl: './listofdepartment.component.html',
  styleUrls: ['./listofdepartment.component.css'],
  providers:[databaseservice]
})
export class ListofdepartmentComponent implements OnInit {
 listofDedpartments:IDepartment[];
 Role:string;
  AdminRole:string;
  Employeeole:String;
  HRrole:String;

  constructor(private _database:databaseservice,private router:Router) { }

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

    
      this.listofDedpartments=x;
      console.log(x);
    } )
  }

  Deletedepartment(id:number){

    this._database.deletedepartment(id).subscribe(x=>{ window.location.reload()
    },
      err=>{
        if(err.status===500){
          alert("Something went wrong");

        }
        
      })
    
    

   }


}
