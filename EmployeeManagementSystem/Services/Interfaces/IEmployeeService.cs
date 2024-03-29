using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Services.Interfaces
{
    public interface IEmployeeService
    {
        List<EmployeeView> GetEmployees();
        void DeleteEmployee(string idToBeDeleted);
        void UpdateEmployee(string idToBeUpdated, string firstName, string lastName, DateTime dob, string email, long mobile, DateTime date, string location, string jobTitle, string department, string managerName, string project);
        void AddEmployee(string empId, string firstName, string lastName, DateTime dob, string email, long mobile, DateTime date, string location, string jobTitle, string department, string managerName, string project);
    }
}
