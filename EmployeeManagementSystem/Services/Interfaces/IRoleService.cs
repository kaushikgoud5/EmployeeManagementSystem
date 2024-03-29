using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Services.Interfaces
{
    public interface IRoleService
    {
        void AddRole(string roleName, string department, string description, string location);
        List<Role> GetRoles();
    }
}
