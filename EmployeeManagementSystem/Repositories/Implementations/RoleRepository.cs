using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Repositories.Interfaces;

namespace EmployeeManagementSystem.Repositories.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly List<Role> _role;
        string jsonRolePath = @"C:\Users\kaushik.n\source\repos\EmployeeManagementSystem\EmployeeManagementSystem\Repositories\DataSource\jsonRolePath.json";
        public RoleRepository()
        {
            _role = new List<Role>();

        }
        public void AddRole(Role role)
        {
            /* _role.Add(role);*/
            string jsonRole = JsonSerializer.Serialize(role);
            File.AppendAllText(jsonRolePath, jsonRole + "\n");
        }
        public List<Role> Get()
        {
            /*  return _role;*/
            List<Role> list = new List<Role>();
            IEnumerable<string> readText = File.ReadLines(jsonRolePath).ToList();
            foreach (string line in readText)
            {
                Role role = JsonSerializer.Deserialize<Role>(line);
                list.Add(role);
            }
            List<Role> _role = new List<Role>(list);
            return _role;
        }
    }
}
