﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagementSystem.Models;
using System.Text.Json;
using System.Collections;
using EmployeeManagementSystem.Repositories.Interfaces;
using AutoMapper;
using EmployeeManagementSystem.ModelsView;

namespace EmployeeManagementSystem.Repositories.Implementations
{

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> _employee;
        public string jsonEmployeePath;

        public EmployeeRepository()
        {
            jsonEmployeePath = @"C:\Users\kaushik.n\source\repos\EmployeeManagementSystem\EmployeeManagementSystem\Repositories\DataSource\jsonEmployeePath.json";
            _employee = new List<Employee>();
        }
        public void Add(Employee employee)
        {
            
            string jsonEmployee = JsonSerializer.Serialize(employee);
            File.AppendAllText(jsonEmployeePath, jsonEmployee + "\n");
        }

        public void Update(string idTobeUpdated, Employee employee)
        {
            List<string> readText = File.ReadLines(jsonEmployeePath).ToList();
            List<Employee> updatedEmployees = new List<Employee>();
            foreach (string line in readText)
            {
                Employee updateEmployee = JsonSerializer.Deserialize<Employee>(line);
                if (updateEmployee.EmpId == idTobeUpdated)
                {
                    updateEmployee.EmpId = idTobeUpdated;
                    updateEmployee.FirstName = employee.FirstName;
                    updateEmployee.LastName = employee.LastName;
                    updateEmployee.DateOfBirth = employee.DateOfBirth;
                    updateEmployee.Email = employee.Email;
                    updateEmployee.JobTitle = employee.JobTitle;
                    updateEmployee.JoinDate = employee.JoinDate;
                    updateEmployee.Location = employee.Location;
                    updateEmployee.Project = employee.Project;
                    updateEmployee.Department = employee.Department;
                }
                updatedEmployees.Add(updateEmployee);
            }
            List<string> serializedEmployees = updatedEmployees.Select(e => JsonSerializer.Serialize(e)).ToList();

            File.WriteAllLines(jsonEmployeePath, serializedEmployees);
        }

        public List<EmployeeView> Get()
        {
            var list = new List<EmployeeView>();
            IEnumerable<string> readText = File.ReadLines(jsonEmployeePath).ToList();
            foreach (string line in readText)
            {
                Employee? employee = JsonSerializer.Deserialize<Employee>(line);
                EmployeeView employeeView = MapToViewModel(employee);
                list.Add(employeeView);
            }
            return list;
        }
        public EmployeeView MapToViewModel(Employee employee)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeView>());
            var mapper = new Mapper(config);
            var empView = mapper.Map<EmployeeView>(employee);
            return empView;
        }

        public void Delete(string id)
        {
            List<string> readText = File.ReadLines(jsonEmployeePath).ToList();
            List<Employee> demo = new List<Employee>();
            foreach (string line in readText)
            {
                Employee employee = JsonSerializer.Deserialize<Employee>(line);
                if (employee.EmpId != id)
                {
                    demo.Add(employee);

                }
            }
            List<string> serializedEmployees = new List<string>();
            foreach (var employee in demo)
            {
                serializedEmployees.Add(JsonSerializer.Serialize(employee));
            }
            File.WriteAllLines(jsonEmployeePath, serializedEmployees);

        }

    }
}
