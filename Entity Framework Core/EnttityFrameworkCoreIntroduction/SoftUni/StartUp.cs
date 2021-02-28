namespace SoftUni
{
    using Microsoft.EntityFrameworkCore;
    using SoftUni.Data;
    using SoftUni.Models;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new SoftUniContext();
            Console.WriteLine(RemoveTown(context));
        }



        // Problem 3.Employees Full Information
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var sb = new StringBuilder();
            var employees = context
                .Employees
                .Select(e => new { e.FirstName, e.LastName, e.MiddleName, e.JobTitle, e.Salary, e.EmployeeId })
                .OrderBy(e => e.EmployeeId)
                .ToList();

            foreach (var e in employees)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }


        //Problem 4.Employees with salaries greater than 50000
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context.Employees
                .Where(e => e.Salary > 50000)
                .Select(e => new { e.FirstName, e.Salary })
                .OrderBy(n => n.FirstName)
                .ToList();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} - {employee.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        // Problem 5.Employees from Research and Development
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var sb = new StringBuilder();
            var employees = context
                .Employees
                .Where(e => e.Department.Name == "Research and Development")
                .Select(e => new { e.FirstName, e.LastName, DepartmentName = e.Department.Name, e.Salary })
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .ToList();

            foreach (var e in employees)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} from {e.DepartmentName} - ${e.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        // Problem 6.Adding a New Address and Updating Employee
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            var sb = new StringBuilder();
            var addressToAdd = new Address() { AddressText = "Vitoshka 15", TownId = 4 };
            var employeeNakov = context.Employees.First(e => e.LastName == "Nakov");
            employeeNakov.Address = addressToAdd;
            context.SaveChanges();

            var employees = context.Employees.OrderByDescending(e => e.AddressId).Take(10).Select(e => e.Address).ToList();
            foreach (var e in employees)
            {
                sb.AppendLine(e.AddressText);
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 7.Employees and Projects
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var sb = new StringBuilder();
            var employees = context
                .Employees
                .Where(e => e.EmployeesProjects
                .Any(p => p.Project.StartDate.Year >= 2001 && p.Project.StartDate.Year <= 2003))
                .Take(10)
                .Select
                (
                 e => new
                 {
                     e.FirstName,
                     e.LastName,
                     ManagerFirstName = e.Manager.FirstName,
                     ManagerLastName = e.Manager.LastName,
                     Projects = e.EmployeesProjects.Select
                         (
                         ep => new
                         {
                             ep.Project.Name,
                             StartDate = ep.Project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture),
                             EndDate = ep.Project.EndDate.HasValue ? ep.Project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture) : "not finished"
                         }
                         ).ToList()
                 }
                )
                .ToList();

            foreach (var e in employees)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} - Manager: {e.ManagerFirstName} {e.ManagerLastName}");
                foreach (var p in e.Projects)
                {
                    sb.AppendLine($"--{p.Name} - {p.StartDate} - {p.EndDate}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 8.Addresses by Town
        public static string GetAddressesByTown(SoftUniContext context)
        {
            var sb = new StringBuilder();
            var addresses = context.Addresses
                                    .OrderByDescending(a => a.Employees.Count)
                                    .ThenBy(a => a.Town.Name)
                                    .ThenBy(a => a.AddressText)
                                    .Take(10)
                                    .Select(a => new { a.AddressText, TownName = a.Town.Name, EmployeesCount = a.Employees.Count })
                                    .ToList();
            foreach (var a in addresses)
            {
                sb.AppendLine($"{a.AddressText}, {a.TownName} - {a.EmployeesCount} employees");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 9.Employee 147

        public static string GetEmployee147(SoftUniContext context)
        {
            var sb = new StringBuilder();
            var employee147 = context
                .Employees
                .Where(e => e.EmployeeId == 147)
                .Select(e =>
               new
               {
                   e.FirstName,
                   e.LastName,
                   e.JobTitle,
                   Projects = e.EmployeesProjects.Select(p => p.Project.Name).OrderBy(p => p).ToList()
               }
                )
                .Single();

            sb.AppendLine($"{employee147.FirstName} {employee147.LastName} - {employee147.JobTitle}");
            foreach (var p in employee147.Projects)
            {
                sb.AppendLine(p);
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 10.Departments with More Than 5 Employees
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var sb = new StringBuilder();
            var departments = context
                .Departments
                .Where(d => d.Employees.Count > 5)
                .OrderBy(d => d.Employees.Count)
                .Select(d => new
                {
                    d.Name,
                    ManagerFirstName = d.Manager.FirstName,
                    ManagerLastName = d.Manager.LastName,
                    Employees = d.Employees.Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        e.JobTitle
                    })
                   .OrderBy(e => e.FirstName)
                   .ThenBy(e => e.LastName)
                   .ToList()
                })
                .ToList();
            foreach (var dep in departments)
            {
                sb.AppendLine($" {dep.Name} - {dep.ManagerFirstName}  {dep.ManagerLastName} ");
                foreach (var employee in dep.Employees)
                {
                    sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 11.Find Latest 10 Projects

        public static string GetLatestProjects(SoftUniContext context)
        {
            var sb = new StringBuilder();
            var projects = context
               .Projects
               .OrderByDescending(p => p.StartDate)
               .Take(10)
               .Select(p => new
               {
                   p.Name,
                   p.Description,
                   p.StartDate
               })
               .OrderBy(p => p.Name)
               .ToList();

            foreach (var project in projects)
            {
                sb.AppendLine(project.Name);
                sb.AppendLine(project.Description);
                sb.AppendLine(project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture));
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 12.Increase Salaries
        public static string IncreaseSalaries(SoftUniContext context)
        {
            var departments = new string[] { "Engineering", "Marketing", "Tool Design", "Information Services" };
            var sb = new StringBuilder();
            var employees = context
                .Employees
                .Where(e => departments.Contains(e.Department.Name))
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

            foreach (var employee in employees)
            {
                employee.Salary *= 1.12m;
            }

            context.SaveChanges();
            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:f2})");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 13.Find Employees by First Name Starting with "Sa"
        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            var sb = new StringBuilder();
            var employees = context
                .Employees
                .Where(e => EF.Functions.Like(e.FirstName, "sa%"))
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    e.Salary
                })
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();


            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary:F2})");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 14.Delete Project by Id

        public static string DeleteProjectById(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var employeeprojects = context
                .EmployeesProjects
                .Where(e => e.ProjectId == 2).ToList();

            foreach (var ep in employeeprojects)
            {
                context.EmployeesProjects.Remove(ep);
            }

            var projectToRemove = context
                .Projects
                .Single(p => p.ProjectId == 2);
            context.Projects
                .Remove(projectToRemove);

            context.SaveChanges();

            context.Projects
                .Take(10)
                .Select(p => p.Name)
                .ToList()
                .ForEach(p => sb.AppendLine(p));

            return sb.ToString().TrimEnd();
        }

        //Problem 15.Remove Town

        public static string RemoveTown(SoftUniContext context)
        {
            var townToRemove = context.Towns.FirstOrDefault(t => t.Name == "Seattle");
            var addressesInTown = context.Addresses.Where(a => a.TownId == townToRemove.TownId);
            var employeesLivingInThatTown = context.Employees.Where(e => addressesInTown.Any(a => a.AddressId == e.AddressId));
            var removedAddresses = addressesInTown.Count();
            foreach (var employee in employeesLivingInThatTown)
            {
                employee.AddressId = null;
            }

            foreach (var address in addressesInTown)
            {
                context.Addresses.Remove(address);
            }

            context.Towns.Remove(townToRemove);

            context.SaveChanges();

            var output = $"{removedAddresses} addresses in {townToRemove.Name} were deleted";
            return output;
        }
    }
}
