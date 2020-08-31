using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Transactions;

namespace EmployeeManagement.Models
{
    public class MockDepartmentRepository : IDepartmentRepository
    {
        private List<Department> _listOfDepartment;
        private IEmployeeRepository _listOfEmployee;
        private List<Department> _departmentList;
        private SqlConnection con;
        
        
        public MockDepartmentRepository()
        {
            con = new SqlConnection(@"Data Source = (localdb)\mssqllocaldb; Initial Catalog = EmployeeManagement; Integrated Security = True");
            _departmentList = new List<Department>();
        }
        
        
        public MockDepartmentRepository(IEmployeeRepository listOfEmployee)
        {

            con = new SqlConnection(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=Leavedata;Integrated Security=True");
            _listOfEmployee = listOfEmployee;
            _listOfDepartment = new List<Department>();
            _departmentList = new List<Department>();
        }

        public void CreateDepartment(Department dpt)
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("INSERT INTO Departmentlist VALUES(@DepartmentId,@DepartmentName)",con);
            cmd1.Parameters.AddWithValue("@DepartmentId", dpt.DepartmentId);
            cmd1.Parameters.AddWithValue("@DepartmentName", dpt.Department_name);
            cmd1.ExecuteNonQuery();
            con.Close();
            
            //_listOfDepartment.Add(dpt);
        }

        public void DeleteDepartment(int id)
        {
            SqlCommand cmd3 = new SqlCommand("DELETE FROM Departmentlist WHERE DepartmentId=@id", con);
            SqlCommand cmd4 = new SqlCommand("DELETE FROM Emplist WHERE EmployeeDepartmentId=@id", con);
            cmd3.Parameters.AddWithValue("@id", id);
            cmd4.Parameters.AddWithValue("@id", id);
            con.Open();
            cmd3.ExecuteNonQuery();
            cmd4.ExecuteNonQuery();
            con.Close();

        }

        public List<Department> GetallDepartments()
        {
            _departmentList.RemoveRange(0, _departmentList.Count);
            SqlCommand cmd1 = new SqlCommand("Select * from Departmentlist", con);
            con.Open();
            var result = cmd1.ExecuteReader();
            while (result.Read())
            {
                Department dept = new Department()
                {
                    DepartmentId = Convert.ToInt32(result[0]),
                    Department_name = result[1].ToString()

                };
                _departmentList.Add(dept);


            }
            con.Close();



            return _departmentList;
         
        }

        public void UpdateDepartment(int id,Department dpt)
        {
            SqlCommand cmd4 = new SqlCommand("UPDATE Departmentlist SET DepartmentName=@name where DepartmentId=@id", con);
            cmd4.Parameters.AddWithValue("@id", dpt.DepartmentId);
            cmd4.Parameters.AddWithValue("@name", dpt.Department_name);
            con.Open();
            cmd4.ExecuteNonQuery();
            con.Close();

           
        }
    }
}
