using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using EmployeeManagementSystem.Services;
using EmployeeManagementSystem.Services.Utilities;
using ConsoleTables;
using System.Reflection.Emit;

namespace EmployeeManagementSystem.Views
{
    internal class EmployeeManagement
    {
        public void employeeFeatures()
        {
            var employeeService = new EmployeeService();
            while (true)
            {
                Console.WriteLine("\n1.Add Employee\n2.Display All\n3.Display One\n4.Edit Employee\n5.Delete Employee\n6.Go Back\n");
                int empManagementMenu = Convert.ToInt32(Console.ReadLine());
                switch (empManagementMenu)
                {
                    case 1:
                        AddEmployee(employeeService);
                        break;
                    case 2:
                        DisplayEmployees(employeeService);
                        break;
                    case 3:
                        DisplayOneEmployee(employeeService);
                        break;
                    case 4:
                        UpdateEmployee(employeeService);
                        break;
                    case 5:
                        DeleteEmployee(employeeService);
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("You have Entered An Invalid Option");
                        break;
                }
            }
        }
        public void AddEmployee(EmployeeService employeeService)
        {
            try {
                Validations validations = new Validations();
                Console.Write("Enter Employee Number*:");
                string empId = Console.ReadLine();
                if (validations.IsIdEmpty(empId)) { Console.WriteLine("ID cannot be empty"); return; }
                if (!validations.IsIdUnique(empId, employeeService)) { Console.WriteLine("ID Should be unique"); return; };
                if (!validations.IsValidPattern(empId)) { Console.WriteLine("ID is not valid"); return; };
                Console.Write("Enter First Name*:");
                string firstName = Console.ReadLine();
                if (validations.IsFirstNameEmpty(firstName)) { Console.WriteLine("First name cannot be empty"); return; }
                Console.Write("Enter Last Name*:");
                string lastName = Console.ReadLine();
                if (validations.IsLastNameEmpty(lastName)) { Console.WriteLine("Last Name Cannot be Empty");return; }
                Console.Write("Enter Date of Birth(DD/MM/YYYY format):");
                string dob = Console.ReadLine();
                DateTime dateOfBirth = DateTime.ParseExact(dob, "d/M/yyyy", null);
                Console.Write("Enter Email Id*:");
                string email = Console.ReadLine();
                if (validations.IsEmailEmpty(email)) { Console.WriteLine("Email Cannot be Empty"); return; }
                if (!validations.IsEmailValid(email)) { Console.WriteLine("In valid Email Format"); return; }
                Console.Write("Enter Mobile Number:");
                long mobile = Convert.ToInt64(Console.ReadLine());
                Console.Write("Enter Joining Date*(DD/MM/YYYY format)::");
                DateTime joinDate;
                string jdate = Console.ReadLine();
                if (validations.IsJoinDateEmpty(jdate)) { Console.WriteLine("Joining Date cannot be Empty"); return; }
                joinDate = DateTime.ParseExact(jdate, "d/M/yyyy", null);
                Console.Write("Enter Location*:");
                string location = Console.ReadLine();
                if (validations.IsLocationEmpty(location)) { Console.WriteLine("Location Cannot be empty"); return; }
                Console.Write("Enter Job Title*:");
                string jobTitle = Console.ReadLine();
                if (validations.IsJobTitleEmpty(jobTitle)) { Console.WriteLine("Job Title Cannot be empty"); return; }
                Console.Write("Enter Department*:");
                string department = Console.ReadLine();
                if (validations.IsDepartmentEmpty(department)) { Console.WriteLine( "Department Cannot be Empty"); return; }
                Console.Write("Enter Your Manager:");
                string managerName = Console.ReadLine();
                Console.Write("Enter Your Project:");
                string project = Console.ReadLine();
                employeeService.AddEmployee(empId, firstName, lastName, dateOfBirth, email, mobile, joinDate, location, jobTitle, department, managerName, project);
            }
            catch(Exception e) {
                Console.WriteLine(e.Message+"Please check you date!");
                return;
            }
            
        }
        public void DisplayEmployees(EmployeeService employeeService)
        {
            Console.WriteLine("Displaying All Users");
            var table = new ConsoleTable("Emp No", "Full Name", "Department", "Location", "Joining Date", "Manager name", "Project name");
            employeeService.GetEmployees().ForEach(emp =>
            {

                table.AddRow(emp.EmpId, emp.FirstName + " " + emp.LastName, emp.Department, emp.Location, emp.JoinDate.ToString().Substring(0,10), emp.Manager, emp.Project);
            }

            );
            table.Write();

        }

        public void DisplayOneEmployee(EmployeeService employeeService)
        {
            Console.WriteLine("Enter the Id of the Employee You want to Display");
            string id = Console.ReadLine();
            var table = new ConsoleTable("Emp No", "Full Name", "Department", "Location", "Joining Date", "Manager name", "Project name");
            employeeService.GetEmployees().ForEach(emp =>
            {
                if (emp.EmpId == id)

                {
                    Console.WriteLine("fOUND");
                    table.AddRow(emp.EmpId, emp.FirstName + " " + emp.LastName, emp.Department, emp.Location, DateTime.Now.ToString().Substring(0, 10), emp.Manager, emp.Project);
                }
            });
            table.Write();

        }

        public void UpdateEmployee(EmployeeService employeeService)
        {
            Console.WriteLine("Enter the Employee ID you want to Update");
            bool employeeFound = false;
            string idToBeUpdated = Console.ReadLine();
            employeeService.GetEmployees().ForEach(emp =>
            {
                if (emp.EmpId == idToBeUpdated)
                {
                    employeeFound = true;
                    Console.WriteLine("Enter new Details of an Employee");
                    Console.Write("Enter First Name*:");
                    string firstName = Console.ReadLine();
                    Console.Write("Enter Last Name*:");
                    string lastName = Console.ReadLine();
                    Console.Write("Enter Date of Birth(YYYY-MM-DD format):");
                    string dob = Console.ReadLine();
                    DateTime dateOfBirth = DateTime.ParseExact(dob, "d/M/yyyy", null);
                    Console.Write("Enter Email Id*:");
                    string email = Console.ReadLine();
                    Console.Write("Enter Mobile Number:");
                    long mobile = Convert.ToInt64(Console.ReadLine());
                    Console.Write("Enter Joining Date*:");
                    string jdate = Console.ReadLine();
                    DateTime joinDate = DateTime.ParseExact(jdate, "d/M/yyyy", null);
                    Console.Write("Enter Location*:");
                    string location = Console.ReadLine();
                    Console.Write("Enter Job Title*:");
                    string jobTitle = Console.ReadLine();
                    Console.Write("Enter Department*:");
                    string department = Console.ReadLine();
                    Console.Write("Enter Your Manager:");
                    string managerName = Console.ReadLine();
                    Console.Write("Enter Your Project:");
                    string project = Console.ReadLine();
                    employeeService.UpdateEmployee(idToBeUpdated, firstName, lastName, dateOfBirth, email, mobile, joinDate, location, jobTitle, department, managerName, project);

                }
            });
            if (!employeeFound) { Console.WriteLine("Employee Not Found !!!!"); }
            else { Console.WriteLine("Updated Successfully"); }
        }
        public void DeleteEmployee(EmployeeService employeeService)
        {
            Console.WriteLine("Enter Employee ID you want to delete");
            bool employeeFound = false;
            string idToBeDeleted = Console.ReadLine();
            employeeService.GetEmployees().ForEach(emp =>
            {
                if (emp.EmpId == idToBeDeleted)
                {
                    employeeFound = true;
                    employeeService.DeleteEmployee(idToBeDeleted);
                }
            });
            if (!employeeFound) { Console.WriteLine("Employee Not Found"); }
            else { Console.WriteLine("Deleted Successfully"); }

        }



    }
}
