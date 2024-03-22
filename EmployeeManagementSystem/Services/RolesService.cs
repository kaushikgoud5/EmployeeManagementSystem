﻿using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Services
{
    public class RolesService
    {
        private readonly IRoleRepository _roleRepositary;
        public RolesService()
        {
            _roleRepositary = new RoleRepository();
        }

        public void AddRole(string roleName, string department, string description, string location)
        {
            var role = new Role { Department = department, Name = roleName, Description = description, Location = location };
            _roleRepositary.AddRole(role);
        }

        public List<Role> GetRoles()
        {
            return _roleRepositary.Get();
        }
    }
}
