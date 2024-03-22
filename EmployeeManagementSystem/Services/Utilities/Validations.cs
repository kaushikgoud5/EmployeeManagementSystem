using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeManagementSystem.Services.Utilities
{
    public class Validations
    {

        public bool IsValidPattern(string empId)
        {
            string pattern = @"^TZ\d{1,4}$";
            if (Regex.IsMatch(empId, pattern)) return true;
            return false;
        }
        public bool IsEmailValid(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (Regex.IsMatch(email, pattern)) return true;
            return false;
        }

        public bool CheckValidations(string roleName, string department, string location)
        {
            if (roleName.Length == 0) return false;
            if (department.Length == 0) return false;
            if (location.Length == 0) return false;
            return true;
        }

       
        public bool IsIdUnique(string? empId, EmployeeService employeeService)
        {
            List<Employee> list = employeeService.GetEmployees();
            for(int i=0;i<list.Count; i++)
            {
                if (list[i].EmpId==empId) return false;
            }
            return true;
        }

        public bool IsFirstNameEmpty(string fname)
        {
            return string.IsNullOrEmpty(fname);
        }
        public bool IsIdEmpty(string? empId)
        {
            return string.IsNullOrEmpty(empId);
        }

        public bool IsLastNameEmpty(string? lastName)
        {
            return string.IsNullOrEmpty(lastName);
        }

        public bool IsEmailEmpty(string email)
        {
            return string.IsNullOrEmpty(email);
        }

        public bool IsJoinDateEmpty(string? date)
        {
            return string.IsNullOrEmpty(date);
        }

        public bool IsLocationEmpty(string? location)
        {
           return string.IsNullOrEmpty(location);  
        }

        public bool IsDepartmentEmpty(string? department)
        {
            return string.IsNullOrEmpty(department);
        }
        public bool IsJobTitleEmpty(string? jtitle)
        {
            return string.IsNullOrEmpty(jtitle);
        }
    }
}
