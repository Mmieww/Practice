using System;
using System.Collections.Generic;
using System.Linq;

public class Employee
{
    public string Name { get; set; }
    public string Department { get; set; }
    public decimal Salary { get; set; }

    public Employee(string name, string department, decimal salary)
    {
        Name = name;
        Department = department;
        Salary = salary;
    }
}

public static class ArrayOperations
{
    public static List<Employee> SortEmployeesBySalary(Employee[] employees)
    {
        return employees.OrderBy(e => e.Salary).ToList();
    }

    public static List<Employee> FilterEmployeesByDepartment(Employee[] employees, string department)
    {
        return employees.Where(e => e.Department.Equals(department, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public static decimal CalculateAverageSalary(Employee[] employees)
    {
        return employees.Average(e => e.Salary);
    }

    public static Employee[] GenerateSampleData(int count)
    {
        var random = new Random();
        string[] departments = { "HR-отдел", "Отдел разработки", "Отдел маркетинга", "Отдел финансов" };
        var employees = new List<Employee>();

        for (int i = 1; i <= count; i++)
        {
            string name = "Сотрудник " + i;
            string department = departments[random.Next(departments.Length)];
            decimal salary = random.Next(30000, 120000);
            employees.Add(new Employee(name, department, salary));
        }

        return employees.ToArray();
    }

    public static List<Employee> FindEmployeesByDepartment(Employee[] employees, string department)
    {
        return employees.Where(e => e.Department.Equals(department, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}

public class Program
{
    public static void Main()
    {
        Employee[] employees = ArrayOperations.GenerateSampleData(10);

        Console.WriteLine("Сгенерированные сотрудники:");
        foreach (var employee in employees)
        {
            Console.WriteLine($"{employee.Name}, Отдел: {employee.Department}, Зарплата: {employee.Salary}");
        }

        Console.WriteLine("\nОтсортированные сотрудники по зарплате:");
        var sortedEmployees = ArrayOperations.SortEmployeesBySalary(employees);
        foreach (var employee in sortedEmployees)
        {
            Console.WriteLine($"{employee.Name}, Зарплата: {employee.Salary}");
        }

        decimal averageSalary = ArrayOperations.CalculateAverageSalary(employees);
        Console.WriteLine($"\nСредняя зарплата: {averageSalary}");

        Console.Write("\nВведите название отдела для фильтрации: ");
        string department = Console.ReadLine();
        var employeesInDepartment = ArrayOperations.FindEmployeesByDepartment(employees, department);

        Console.WriteLine($"\nСотрудники в отделе {department}:");
        foreach (var emp in employeesInDepartment)
        {
            Console.WriteLine(emp.Name);
        }
    }
}