using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepositary;
        public EmployeeService()
        {
            _employeeRepositary = new EmployeeRepository();
        }
        public List<Employee> GetEmployees()
        {
            return _employeeRepositary.Get();
        }

        public void DeleteEmployee(string idToBeDeleted)
        {
            _employeeRepositary.Delete(idToBeDeleted);
        }

        public void UpdateEmployee(string idToBeUpdated, string firstName, string lastName, DateTime dob, string email, long mobile, DateTime date, string location, string jobTitle, string department, string managerName, string project)
        {
            var employee = new Employee { FirstName = firstName, LastName = lastName, DateOfBirth = dob, Email = email, Phone = mobile, JoinDate = date, Location = location, JobTitle = jobTitle, Department = department, Manager = managerName, Project = project };
            _employeeRepositary.Update(idToBeUpdated,employee);
        }
        public void AddEmployee(string empId, string firstName, string lastName, DateTime dob, string email, long mobile, DateTime date, string location, string jobTitle, string department, string managerName, string project)
        {

            var employee = new Employee { EmpId = empId, FirstName = firstName, LastName = lastName, DateOfBirth = dob, Email = email, Phone = mobile, JoinDate = date, Location = location, JobTitle = jobTitle, Department = department, Manager = managerName, Project = project };
            _employeeRepositary.Add(employee);
        }
    }
}
