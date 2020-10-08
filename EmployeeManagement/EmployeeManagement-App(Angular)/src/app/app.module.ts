import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { ListofemployeesComponent } from './listofemployees/listofemployees.component';
import { AddemployeeComponent } from './addemployee/addemployee.component';

import { LoginComponent } from './login/login.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ListofdepartmentComponent } from './listofdepartment/listofdepartment.component';
import { AdddepartmentComponent } from './adddepartment/adddepartment.component';
import { EditdepartmentComponent } from './editdepartment/editdepartment.component';
import { EditemployeeComponent } from './editemployee/editemployee.component';
import { HttpClientModule } from '@angular/common/http';
import { authguard } from 'src/authguard';
import { ForgotpasswordComponent } from './forgotpassword/forgotpassword.component';
import { HomepageComponent } from './homepage/homepage.component';



export const routes: Routes = [
{path:'',component:LoginComponent},
{path:'home',component:HomepageComponent,canActivate:[authguard],children:[
  
{path:'Employees',component:ListofemployeesComponent,canActivateChild:[authguard]},
{path:'Addemployee',component:AddemployeeComponent,canActivateChild:[authguard]},
{path:'Departments',component:ListofdepartmentComponent,canActivateChild:[authguard]},
{path:'Adddepartment',component:AdddepartmentComponent,canActivateChild:[authguard]},
{path:'Editdepartment/:id',component:EditdepartmentComponent,canActivateChild:[authguard]},
{path:'Editemployee/:id',component:EditemployeeComponent,canActivateChild:[authguard]},
{path:'**',component:ListofemployeesComponent,canActivateChild:[authguard]},
]},
{path:'Forgotpassword',component:ForgotpasswordComponent},
{path:'**',component:LoginComponent}]

@NgModule


({
  declarations: [
    AppComponent,
    ListofemployeesComponent,
    AddemployeeComponent,

    LoginComponent,
    ListofdepartmentComponent,
    AdddepartmentComponent,
    EditdepartmentComponent,
    EditemployeeComponent,
    ForgotpasswordComponent,
    HomepageComponent,

  ],
  imports: [
    BrowserModule,
    NgbModule,RouterModule.forRoot(routes),
    FormsModule,HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
