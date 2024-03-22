using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;
using EmployeeManagementSystem.Services;
using EmployeeManagementSystem.Services.Utilities;

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
                    case 1:
                        AddRole(rolesService);
                        break;
                    case 2:
                        DisplayRole(rolesService);
                        break;
                    case 3:
                        Console.WriteLine("Go Back");
                        return;

                }

            }


        }
        public void AddRole(RolesService rolesService)
        {
            Console.Write("Role Name*:");
            string roleName = Console.ReadLine();
            Console.Write("Department*:");
            string department = Console.ReadLine();
            Console.WriteLine("Description:");
            string description = Console.ReadLine();
            Console.WriteLine("Location:");
            string location = Console.ReadLine();
            Validations validations = new Validations();
            if (validations.CheckValidations(roleName, department, location))
            {
                 rolesService.AddRole(roleName, department, description, location);
            }
            else
            {
                Console.WriteLine("Fields are Empty");
            }
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
