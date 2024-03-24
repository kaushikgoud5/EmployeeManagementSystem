using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;
using EmployeeManagementSystem.Services;
using EmployeeManagementSystem.Services.Utilities;
using EmployeeManagementSystem.Views.Enums;

namespace EmployeeManagementSystem.Views
{
    public class RoleManagement
    {
        public void roleFeatures()
        {
            RolesService rolesService = new RolesService();
            while (true)
            {
                Console.WriteLine("1.Add Role\n2.Display All\n3.Go Back");
                int roleManagementMenu = Convert.ToInt32(Console.ReadLine());
                switch (roleManagementMenu)
                {
                    case (int)RoleEnum.AddRole:
                        AddRole(rolesService);
                        break;
                    case (int)RoleEnum.DisplayRole:
                        DisplayRole(rolesService);
                        break;
                    case (int)RoleEnum.GoBack:
                        Console.WriteLine("Go Back");
                        return;

                }

            }


        }
        public void AddRole(RolesService rolesService)
        {
            Validations validations = new Validations();
            Console.Write("Role Name*:");
            string roleName = Console.ReadLine();
            if (validations.IsRoleNameEmpty(roleName)) { Console.WriteLine("Role Cannot be Empty"); return; }
            Console.Write("Department*:");
            string department = Console.ReadLine();
            if (validations.IsDepartmentEmpty(department)) { Console.WriteLine("Department Cannot be Empty"); return; }
            Console.WriteLine("Description:");
            string description = Console.ReadLine();
            Console.WriteLine("Location:");
            string location = Console.ReadLine();

            rolesService.AddRole(roleName, department, description, location);
            
        }
        public void DisplayRole(RolesService rolesService)
        {
            Console.WriteLine("Displaying All Roles");
            var table = new ConsoleTable("Name", "Description", "Department", "Location");
            rolesService.GetRoles().ForEach(roles =>
            {
                table.AddRow(roles.Name, roles.Description, roles.Description, roles.Location);
            });
            table.Write();
        }

    }
}
