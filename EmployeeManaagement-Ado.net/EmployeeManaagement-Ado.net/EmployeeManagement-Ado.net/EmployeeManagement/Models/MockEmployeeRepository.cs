using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> employeeList = new List<Employee>();
        private SqlConnection con;
        private List<Department> _listofdepartments;
        public MockEmployeeRepository()
        {
            con = new SqlConnection(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=Leavedata;Integrated Security=True");

        }

        public List<Employee> CreateEmployee(Employee newEmployee)
        {

            
            //Ado.net implementation
            con.Open();
            SqlCommand cmd2 = new SqlCommand("INSERT INTO Emplist VALUES(@ID,@Name,@Surname,@ContactNumber,@Address,@Department)", con);
            cmd2.Parameters.AddWithValue("@Id", newEmployee.EmployeeId);
            cmd2.Parameters.AddWithValue("@Name", newEmployee.Name);
            cmd2.Parameters.AddWithValue("@Surname", newEmployee.Surname);
            cmd2.Parameters.AddWithValue("@ContactNumber", newEmployee.Contactnumber);
            cmd2.Parameters.AddWithValue("@Address", newEmployee.Address);
            cmd2.Parameters.AddWithValue("@Department", newEmployee.Department.DepartmentId);

            cmd2.ExecuteNonQuery();
            con.Close();
            return employeeList;
        }

        public void DeleteEmployee(int id)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM emplist WHERE EmpoyeeId=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public List<Employee> GetAllEmployees()
        {

            SqlCommand cmd = new SqlCommand("Select * from Emplist INNER JOIN Departmentlist ON Emplist.EmployeeDepartmentId = Departmentlist.DepartmentId;", con);
            con.Open();
            var result = cmd.ExecuteReader();
            employeeList.RemoveAll(x => x.Name.Length > 0);
            while (result.Read())


            {
                employeeList.Add(new Employee
                {
                    EmployeeId = Convert.ToInt32(result[0]),
                    Name = result[1].ToString(),
                    Surname = result[2].ToString(),
                    Contactnumber = result[3].ToString(),
                    Address = result[4].ToString(),
                    Department = new Department { DepartmentId = Convert.ToInt32(result[5]), Department_name = result[7].ToString() }



                });
            }
            con.Close();

            return employeeList;
        }


            public Employee UpdateEmployee(Employee modifiedEmployee,int id)
        {
            SqlCommand cmd2 = new SqlCommand("Update Emplist SET EmpployeeName=@Name,EmployeeSurname=@Surname,EmployeeMobileno=@ContactNumber,EmployeeAddress=@Address,EmployeeDepartmentId=@Department where EmpoyeeId=@id", con);
            cmd2.Parameters.AddWithValue("@id", modifiedEmployee.EmployeeId);
            cmd2.Parameters.AddWithValue("@Name", modifiedEmployee.Name);
            cmd2.Parameters.AddWithValue("@Surname", modifiedEmployee.Surname);
            cmd2.Parameters.AddWithValue("@ContactNumber", modifiedEmployee.Contactnumber);
            cmd2.Parameters.AddWithValue("@Address", modifiedEmployee.Address);
            cmd2.Parameters.AddWithValue("@Department", modifiedEmployee.Department.DepartmentId);
            con.Open();

            cmd2.ExecuteNonQuery();
            con.Close();
            

            return modifiedEmployee;
          
        }
    }
}
