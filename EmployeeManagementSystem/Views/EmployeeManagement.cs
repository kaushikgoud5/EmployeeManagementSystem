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
using EmployeeManagementSystem.Views.Enums;

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
                    case (int)EmployeeEnum.AddEmployee:
                        AddEmployee(employeeService);
                        break;
                    case(int) EmployeeEnum.DisplayEmployees:
                        DisplayEmployees(employeeService);
                        break;
                    case (int)EmployeeEnum.DisplayOneEmployee:
                        DisplayOneEmployee(employeeService);
                        break;
                    case (int)EmployeeEnum.UpdateEmployee:
                        UpdateEmployee(employeeService);
                        break;
                    case (int)EmployeeEnum.DeleteEmployee:
                        DeleteEmployee(employeeService);
                        break;
                    case (int)EmployeeEnum.GoBack:
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
                while (validations.IsIdEmpty(empId)) { 
                    Console.WriteLine("ID cannot be empty");
                    Console.Write("Enter Employee Number*:");
                    empId = Console.ReadLine();
                    validations.IsIdEmpty(empId);
                } 
                while (!validations.IsValidPattern(empId)) { 
                    Console.WriteLine("ID is not valid");
                    Console.Write("Enter Employee Number*(EX:TZ0000):");
                    empId = Console.ReadLine();
                    validations.IsValidPattern(empId);
                    
                };
                while (!validations.IsIdUnique(empId, employeeService)) {
                    Console.WriteLine("ID Should be unique");
                    Console.Write("Enter Employee Number*:");
                    empId = Console.ReadLine();
                    validations.IsIdUnique(empId,employeeService);
                }
               
                Console.Write("Enter First Name*:");
                string firstName = Console.ReadLine();
                while (validations.IsFirstNameEmpty(firstName)) { 
                    Console.WriteLine("First name cannot be empty");
                    Console.Write("Enter First Name*:");
                    firstName = Console.ReadLine();
                }
                Console.Write("Enter Last Name*:");
                string lastName = Console.ReadLine();
                while (validations.IsLastNameEmpty(lastName)) { 
                    Console.WriteLine("Last Name Cannot be Empty");
                    Console.Write("Enter Last Name*:");
                    lastName = Console.ReadLine();
                    validations.IsLastNameEmpty(lastName);
                }
                Console.Write("Enter Date of Birth(DD/MM/YYYY format):");
                string dob = Console.ReadLine();
                DateTime dateOfBirth = DateTime.ParseExact(dob, "d/M/yyyy", null);
                Console.Write("Enter Email Id*:");
                string email = Console.ReadLine();
                while (validations.IsEmailEmpty(email)) { 
                    Console.WriteLine("Email Cannot be Empty");
                    email = Console.ReadLine();
                    validations.IsEmailEmpty(email);
                }
                while (!validations.IsEmailValid(email)) { 
                    Console.WriteLine("In valid Email Format");
                    email = Console.ReadLine();
                    validations.IsEmailValid(email);


                }
                Console.Write("Enter Mobile Number:");
                long mobile = Convert.ToInt64(Console.ReadLine());
                while (!validations.IsMobileValid(mobile)) {
                    Console.WriteLine("Mobile Number is Invalid(Enter a 10 Digit Number Only)");
                    mobile = Convert.ToInt64(Console.ReadLine());
                    validations.IsMobileValid(mobile);
                }
                Console.Write("Enter Joining Date*(DD/MM/YYYY format)::");
                DateTime joinDate;
                string jdate = Console.ReadLine();
                while (validations.IsJoinDateEmpty(jdate)) { 
                    Console.WriteLine("Joining Date cannot be Empty");
                    jdate = Console.ReadLine();
                    validations.IsJoinDateEmpty(jdate);
                }
                joinDate = DateTime.ParseExact(jdate, "d/M/yyyy", null);
                Console.Write("Enter Location*:");
                string location = Console.ReadLine();
                while (validations.IsLocationEmpty(location)) { 
                    Console.WriteLine("Location Cannot be empty");
                    location = Console.ReadLine();
                    validations.IsLocationEmpty(location);
                }
                Console.Write("Enter Job Title*:");
                string jobTitle = Console.ReadLine();
                while (validations.IsJobTitleEmpty(jobTitle)) {
                    Console.WriteLine("Job Title Cannot be empty");
                    jobTitle = Console.ReadLine();
                    validations.IsJobTitleEmpty(jobTitle);
                }
                Console.WriteLine("Choose Departments*:");
                string department;
                StaticData staticData = new StaticData();
                for (int i = 0; i < staticData.departments.Count; i++) {
                    Console.WriteLine(i+1 + "." + staticData.departments[i]);
                }
                int chooseMenuDept=Convert.ToInt32(Console.ReadLine());
                while (true)
                {
                    if(chooseMenuDept <= 0 && chooseMenuDept > staticData.departments.Count) { 
                        Console.WriteLine("Invalid");
                    }
                    else
                    {
                        department = staticData.departments[chooseMenuDept];
                        break;
                    }
                }
                if (validations.IsDepartmentEmpty(department)) { 
                    Console.WriteLine( "Department Cannot be Empty");
                    return;
                }

                Console.Write("Enter Your Manager:");
                string managerName = Console.ReadLine();
                Console.Write("Choose our Projects:");
                string project;
                for (int i = 0; i < staticData.projects.Count; i++)
                {
                    Console.WriteLine(i + 1 + "." + staticData.departments[i]);
                }
                int chooseMenuProject = Convert.ToInt32(Console.ReadLine());
                while (true)
                {
                    if (chooseMenuProject <= 0 && chooseMenuProject > staticData.projects.Count)
                    {
                        Console.WriteLine("Invalid");
                    }
                    else
                    {
                        project = staticData.projects[chooseMenuProject];
                        break;
                    }
                }
                employeeService.AddEmployee(empId, firstName, lastName, dateOfBirth, email, mobile, joinDate, location, jobTitle, department, managerName, project);
            }
            catch(Exception e) {
                Console.WriteLine("Please check your date!");
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
                    table.AddRow(emp.EmpId, emp.FirstName + " " + emp.LastName, emp.Department, emp.Location, DateTime.Now.ToString().Substring(0, 10), emp.Manager, emp.Project);
                }
            });
            table.Write();

        }

        public void UpdateEmployee(EmployeeService employeeService)
        {
            Console.WriteLine("Enter the Employee ID you want to Update");
            bool isEmployeeFound = false;
            string idToBeUpdated = Console.ReadLine();
            employeeService.GetEmployees().ForEach(emp =>
            {
                if (emp.EmpId == idToBeUpdated)
                {
                    isEmployeeFound = true;
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
            if (!isEmployeeFound) { Console.WriteLine("Employee Not Found !!!!"); }
            else { Console.WriteLine("Updated Successfully"); }
        }
        public void DeleteEmployee(EmployeeService employeeService)
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
            if (!isEmployeeFound) { Console.WriteLine("Employee Not Found"); }
            else { Console.WriteLine("Deleted Successfully"); }

        }



    }
}
