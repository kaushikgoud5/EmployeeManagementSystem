// See https://aka.ms/new-console-template for more information
using EmployeeManagementSystem.Views;
using EmployeeManagementSystem.Services.Implementations;

Console.WriteLine("Employee Management System");
while (true) {
    try 
    { 
    Console.WriteLine("1.Employee Management\n2.Role Management\n3.Exit\n");
    int mainMenuOption=Convert.ToInt32(Console.ReadLine());
        switch (mainMenuOption)
        {
        case 1:
            EmployeeManagement employeeManagement=new EmployeeManagement();
            employeeManagement.EmployeeFeatures();
            break;
        case 2:
            RoleManagement roleManagement=new RoleManagement();
            roleManagement.RoleFeatures();
            break; 
        case 3:Environment.Exit(0);  
            break;
        default: Console.WriteLine("You have Entered An Invalid Option");
            break;
        }
     
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        Console.WriteLine();
    }
    
}
