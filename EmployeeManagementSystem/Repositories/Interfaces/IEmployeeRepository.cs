using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ModelsView;


namespace EmployeeManagementSystem.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        List<EmployeeView> Get();
        void Add(Employee employee);
        void Delete(string employeeId);

        void Update(string idTobeUpdated, Employee employee);



    }
}
