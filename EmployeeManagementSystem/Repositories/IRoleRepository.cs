using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Repositories
{
    public interface IRoleRepository
    {
        void AddRole(Role role);
        List<Role> Get();
    }
}
