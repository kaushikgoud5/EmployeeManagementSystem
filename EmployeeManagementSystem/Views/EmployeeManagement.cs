using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ConsoleTables;
using System.Reflection.Emit;
using EmployeeManagementSystem.Views.Enums;
using EmployeeManagementSystem.Services.Implementations;
using EmployeeManagementSystem.Services.Interfaces;
using EmployeeManagementSystem.Views.Utilities;

namespace EmployeeManagementSystem.Views
{
    public class EmployeeManagement
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeManagement()
        {
           _employeeService=new EmployeeService();  
        }
      
        public void EmployeeFeatures()
        {
            while (true)
            {
                Console.WriteLine("\n1.Add Employee\n2.Display All\n3.Display One\n4.Edit Employee\n5.Delete Employee\n6.Go Back\n");
                try
                {
                int empManagementMenu = Convert.ToInt32(Console.ReadLine());
                        switch (empManagementMenu)
                        {
                            case (int)EmployeeEnum.AddEmployee:
                                AddEmployee(_employeeService);
                                break;
                            case(int) EmployeeEnum.DisplayEmployees:
                                DisplayEmployees(_employeeService);
                                break;
                            case (int)EmployeeEnum.DisplayOneEmployee:
                                DisplayOneEmployee(_employeeService);
                                break;
                            case (int)EmployeeEnum.UpdateEmployee:
                                UpdateEmployee(_employeeService);
                                break;
                            case (int)EmployeeEnum.DeleteEmployee:
                                DeleteEmployee(_employeeService);
                                break;
                            case (int)EmployeeEnum.GoBack:
                                return;
                            default:
                                Console.WriteLine("You have Entered An Invalid Option");
                                break;
                        }
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
               
            }
        }


        void AddEmployee(IEmployeeService employeeService)
        {
                string empId = TakeInput.ValidateInput("Employee Number", Validations.IsIdValidPattern);
                while (!Validations.IsIdUnique(empId, employeeService))
                {
                    Console.WriteLine("ID Should be unique");
                    Console.Write("Enter Employee Number*:");
                    empId = Console.ReadLine();
                    Validations.IsIdUnique(empId, employeeService);
                }
                string firstName = TakeInput.ValidateInput("First Name*", Validations.IsNameValidPattern);
                string lastName = TakeInput.ValidateInput("Last Name*", Validations.IsNameValidPattern);
                string email = TakeInput.ValidateInput("Email", Validations.IsEmailValid);
                DateTime dob = TakeInput.ValidateDateInput("Date of Birth(DD/MM/YYYY format)");
                string location = TakeInput.ValidateInput("Location*", Validations.IsNameValidPattern);
                string jobTitle = TakeInput.ValidateInput("Job Title*", Validations.IsNameValidPattern);
                long mobile = TakeInput.ValidateMobileInput("Enter Mobile Number");
                string department = TakeInput.SelectFromMenu("Choose Departments*", new StaticData().departments);
                Console.Write("Enter Manager Name:");
                string managerName =Console.ReadLine();
                DateTime joinDate = TakeInput.ValidateDateInput("Joining Date(DD/MM/YYYY format)");
                string project = TakeInput.SelectFromMenu("Choose Projects:", new StaticData().projects);
                employeeService.AddEmployee(empId, firstName, lastName, dob, email, mobile, joinDate, location, jobTitle, department, managerName, project);
                Console.WriteLine("Employee Added Successfully!!");
             
        }
        void DisplayEmployees(IEmployeeService employeeService)
        {
            Console.WriteLine("Displaying All Users");
         
            var table = new ConsoleTable("Emp No", "Full Name", "Department", "Location", "Joining Date", "Manager name", "Project name");
            employeeService.GetEmployees().ForEach(emp =>
            {

                table.AddRow(emp.EmpId, emp.FirstName + " " + emp.LastName, emp.Department, emp.Location, emp.JoinDate.ToString().Substring(0,10), emp.Manager, emp.Project);
            }

            );
            Console.ForegroundColor = ConsoleColor.Green;
            table.Write();
            Console.ForegroundColor=ConsoleColor.White;

        }

        void DisplayOneEmployee(IEmployeeService employeeService)
        {
            bool isEmployeeFound = false;
            Console.WriteLine("Enter the Id of the Employee You want to Display");
            string id = Console.ReadLine();
            var table = new ConsoleTable("Emp No", "Full Name", "Department", "Location", "Joining Date", "Manager name", "Project name");
            employeeService.GetEmployees().ForEach(emp =>
            {
                if (emp.EmpId == id)

                {
                    isEmployeeFound = true;
                    table.AddRow(emp.EmpId, emp.FirstName + " " + emp.LastName, emp.Department, emp.Location, DateTime.Now.ToString().Substring(0, 10), emp.Manager, emp.Project);
                }
            });
          
            if (!isEmployeeFound){ 
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Employee Not Found !!!!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else { 
                Console.ForegroundColor = ConsoleColor.Green;
                table.Write();
                Console.ForegroundColor = ConsoleColor.White; }

        }

        void UpdateEmployee(IEmployeeService employeeService)
        {
            Console.WriteLine("Enter the Employee ID you want to Update");
            bool isEmployeeFound = false;
            string? idToBeUpdated = Console.ReadLine();
            employeeService.GetEmployees().ForEach(emp =>
            {
                if (emp.EmpId == idToBeUpdated)
                {
                    isEmployeeFound = true;
                    string firstName = TakeInput.ValidateInput("First Name*", Validations.IsNameValidPattern);
                    string lastName = TakeInput.ValidateInput("Last Name*", Validations.IsNameValidPattern);
                    string email = TakeInput.ValidateInput("Email", Validations.IsEmailValid);
                    DateTime dob = TakeInput.ValidateDateInput("Date of Birth(DD/MM/YYYY format)");
                    string location = TakeInput.ValidateInput("Location*", Validations.IsNameValidPattern);
                    string jobTitle = TakeInput.ValidateInput("Job Title*", Validations.IsNameValidPattern);
                    long mobile = TakeInput.ValidateMobileInput("Enter Mobile Number");
                    string department = TakeInput.SelectFromMenu("Choose Departments*", new StaticData().departments);
                    Console.Write("Enter Manager Name");
                    string managerName = Console.ReadLine();
                    DateTime joinDate = TakeInput.ValidateDateInput("Joining Date(DD/MM/YYYY format)");
                    string project = TakeInput.SelectFromMenu("Choose Projects:", new StaticData().projects);
                    employeeService.UpdateEmployee(idToBeUpdated, 
                                                    firstName, 
                                                    lastName, 
                                                    dob, 
                                                    email, 
                                                    mobile, 
                                                    joinDate, 
                                                    location, 
                                                    jobTitle,
                                                    department, 
                                                    managerName,
                                                    project);

                }
            });
            if (!isEmployeeFound) { 
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Employee Not Found !!!!"); 
                Console.ForegroundColor = ConsoleColor.White; }
            else { 
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine( "Updated Successfully");
                Console.ForegroundColor = ConsoleColor.White; 
            }
        }
        void DeleteEmployee(IEmployeeService employeeService)
        {
            Console.WriteLine("Enter Employee ID you want to delete");
            bool isEmployeeFound = false;
            string idToBeDeleted = Console.ReadLine();
            employeeService.GetEmployees().ForEach(emp =>
            {
                if (emp.EmpId == idToBeDeleted)
                {
                    isEmployeeFound = true;
                    employeeService.DeleteEmployee(idToBeDeleted);
                }
            });
            if (!isEmployeeFound) { 
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Employee Not Found"); 
                Console.ForegroundColor = ConsoleColor.White; }
            else { 
                Console.ForegroundColor = ConsoleColor.Green; 
                Console.WriteLine("Deleted Successfully");
                Console.ForegroundColor = ConsoleColor.White;
            }

        }



    }
}
