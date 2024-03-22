using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagementSystem.Models;


namespace EmployeeManagementSystem.Repositories
{
    public interface IEmployeeRepository
    {
        List<Employee> Get();
        void Add(Employee employee);
        void Delete(string  employeeId);

        void Update(string idTobeUpdated ,Employee employee);
        


    }
}
