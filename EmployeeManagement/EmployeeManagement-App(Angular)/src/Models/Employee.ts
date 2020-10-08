import { IDepartment } from './Department';

export interface IEmployee{

  employeeId:number;
  name:string;
  surname:string;
  contactnumber:string;
  address:string;
  department:IDepartment;
  email:string;
  departmentId:string;

}

