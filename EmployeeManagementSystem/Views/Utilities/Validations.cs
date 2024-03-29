using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ModelsView;
using EmployeeManagementSystem.Services.Implementations;
using EmployeeManagementSystem.Services.Interfaces;
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

namespace EmployeeManagementSystem.Views.Utilities
{
    public class Validations
    {
        public  static bool IsIdValidPattern(string empId)
        {
            string pattern = @"^TZ\d{1,4}$";
            if (Regex.IsMatch(empId, pattern)) return true;
            return false;
        }
        public static bool IsNameValidPattern(string name)
        {
            string pattern = @"[a-zA-Z]+$";
            if (Regex.IsMatch(name, pattern)) return true;
            return false;
        }
        public static bool IsEmailValid(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (Regex.IsMatch(email, pattern)) return true;
            return false;
        }
        public static bool IsIdUnique(string? empId, IEmployeeService employeeService)
        {
            List<EmployeeView> list = employeeService.GetEmployees();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].EmpId == empId) return false;
            }
            return true;
        }

       

    }
}
