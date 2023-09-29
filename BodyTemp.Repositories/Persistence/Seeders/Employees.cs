using BodyTemp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyTemp.Repositories.Persistence.Seeders
{
    public static partial class Seeders
    {
        public static List<Employee> Employees()
        {
            var employees = new List<Employee>();

            var id = 1;

            employees.Add(new Employee
            {
                Id = id++,
                EmployeeNumber = "00001",
                FirstName = "Juan",
                LastName = "Dela Cruz"
            });

            employees.Add(new Employee
            {
                Id = id++,
                EmployeeNumber = "00002",
                FirstName = "Kat",
                LastName = "Gatmaitan"
            });

            employees.Add(new Employee
            {
                Id = id++,
                EmployeeNumber = "00003",
                FirstName = "Diego",
                LastName = "Garbanzo"
            });

            return employees;
        }
    }
}
