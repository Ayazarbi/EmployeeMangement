using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

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

            con = new SqlConnection(@"Data Source = (localdb)\mssqllocaldb; Initial Catalog = EmployeeManagement; Integrated Security = True");
            _listOfEmployee = listOfEmployee;
            _listOfDepartment = new List<Department>();
            _departmentList = new List<Department>();

        }

        public void CreateDepartment(Department dpt)
        {
            con.Open();
            DynamicParameters param = new DynamicParameters();

            param.Add("@Deptid", dpt.DepartmentId);
            param.Add("@DeptName", dpt.Department_name);
            con.Execute("AddandEditdept", param, commandType: CommandType.StoredProcedure);
            con.Close();

        }

        public void DeleteDepartment(int id)
        {


            con.Open();
            DynamicParameters param = new DynamicParameters();
            param.Add("@Deptid", id);
            con.Execute("Deletedept", param, commandType: CommandType.StoredProcedure);
            con.Close();

        }

        public List<Department> GetallDepartments()
        {

            con.Open();
            var result = con.ExecuteReader("listdept", commandType: CommandType.StoredProcedure);
            _departmentList.RemoveAll(x => true);
            while (result.Read())
            {
                Department dept = new Department();
                dept.DepartmentId = Convert.ToInt32(result[0]);
                dept.Department_name = result[1].ToString();
                _departmentList.Add(dept);

            }
            con.Close();
            return _departmentList;

        }

        public void UpdateDepartment(int id, Department dpt)
        {
            con.Open();
            DynamicParameters param = new DynamicParameters();
            param.Add("@Deptid", dpt.DepartmentId);
            param.Add("@DeptName", dpt.Department_name);
            con.Execute("AddandEditdept", param, commandType: CommandType.StoredProcedure);
            con.Close();

        }
    }
}
