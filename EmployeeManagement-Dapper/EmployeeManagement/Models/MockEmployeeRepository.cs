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
            con = new SqlConnection(@"Data Source = (localdb)\mssqllocaldb; Initial Catalog = EmployeeManagement; Integrated Security = True");

        }

        public List<Employee> CreateEmployee(Employee newEmployee)
        {

            con.Close();
            con.Open();
            DynamicParameters param = new DynamicParameters();
            param.Add("@Empid", newEmployee.EmployeeId);
            param.Add("@Empname", newEmployee.Name);
            param.Add("@Empsurname", newEmployee.Surname);
            param.Add("@Empadddress", newEmployee.Address);
            param.Add("@Empmobileno", newEmployee.Contactnumber);
            param.Add("@Empdepartmentid", newEmployee.Department.DepartmentId);
            con.Execute("EmpAddorEdit", param, commandType: CommandType.StoredProcedure);
            con.Close();
            return employeeList;
        }

        public void DeleteEmployee(int id)
        {
            con.Close();
            con.Open();
            DynamicParameters param = new DynamicParameters();
            param.Add("@Empid", id);
            con.Execute("Delteemp", param, commandType: CommandType.StoredProcedure);
            con.Close();


        }

        public List<Employee> GetAllEmployees()
        {

            con.Close();
            con.Open();
            var result = con.ExecuteReader("Listemp");
            employeeList.RemoveAll(x => x.Name.Length > 0);
            while (result.Read())


            {

                employeeList.Add(new Employee
                {
                    EmployeeId = Convert.ToInt32(result[0]),
                    Name = result[1].ToString(),
                    Surname = result[2].ToString(),
                    Contactnumber = result[4].ToString(),
                    Address = result[3].ToString(),
                    Department = new Department
                    {
                        DepartmentId = Convert.ToInt32(result[5]),
                        Department_name = result[7].ToString()
                    }



                });

            }

            return employeeList;
        }


        public Employee UpdateEmployee(Employee modifiedEmployee, int id)
        {

            con.Close();
            con.Open();
            DynamicParameters param = new DynamicParameters();
            param.Add("@Empid", modifiedEmployee.EmployeeId);
            param.Add("@Empname", modifiedEmployee.Name);
            param.Add("@Empsurname", modifiedEmployee.Surname);
            param.Add("@Empadddress", modifiedEmployee.Address);
            param.Add("@Empmobileno", modifiedEmployee.Contactnumber);
            param.Add("@Empdepartmentid", modifiedEmployee.Department.DepartmentId);
            con.Execute("EmpAddorEdit", param, commandType: CommandType.StoredProcedure);
            con.Close();


            return modifiedEmployee;

        }
    }
}
