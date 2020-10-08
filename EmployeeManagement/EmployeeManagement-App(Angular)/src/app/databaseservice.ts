import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IEmployee } from 'src/Models/Employee';
import { Observable } from 'rxjs';
import { IDepartment } from 'src/Models/Department';


@Injectable()
export class databaseservice{

  constructor(private http:HttpClient){

  }

  getallemployees():Observable< IEmployee[]>{

    return this.http.get<IEmployee[]>("http://localhost:1234/Employee");
  }

  getemployeebyId(id:number):Observable<IEmployee>{

    return this.http.get<IEmployee>("http://localhost:1234/Employee/"+id);
  }

  getalldepartments():Observable<IDepartment[]>{
    return this.http.get<IDepartment[]>("http://localhost:1234/Department");
  }
  getdepartmentbyid(id:number):Observable<IDepartment>{
    return this.http.get<IDepartment>("http://localhost:1234/Department/"+id);
  }
  creatremployee(Employee :IEmployee){

   return this.http.post("http://localhost:1234/Employee",Employee,{
    headers:new HttpHeaders({
     'Content-Type': 'application/json'
    })
  });
  }

  createdepartment(Department:IDepartment){

   return this.http.post("http://localhost:1234/Department/",Department,{
     headers:new HttpHeaders({
      'Content-Type': 'application/json'
     })
   });
  }
//postman che?haaa eathi k
  editemployee(Employee :IEmployee,id:number){

   return this.http.put("http://localhost:1234/Employee/"+id,Employee);
  }

  editdepartment(id:number,Department :IDepartment) {

    return this.http.put("http://localhost:1234/Department/"+id,Department);
  }

  deletedepartment(id:number){

    return this.http.delete("http://localhost:1234/Department/"+id);
  }

  deleteemployee(id:number){

    return this.http.delete("http://localhost:1234/Employee/"+id);
  }

  Getrole():string{

    return localStorage.getItem('role');
  }

}
