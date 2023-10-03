using BodyTemp.Entities.DTOs;
using BodyTemp.Entities.Enums;
using BodyTemp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyTemp.Repositories.Interfaces
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        public Task<IEnumerable<Employee>> GetAllAsync(
            int? id = null
            , string? employeeNumber = null
            , string? firstName = null
            , string? lastName = null
            , TemperatureUnit tempFormat = TemperatureUnit.Celsius
            , decimal? temperatureFrom = null
            , decimal? temperatureTo = null
            , DateTime? dateFrom = null
            , DateTime? dateTo = null
        );

        public Task<Employee> AddTemperatureAsync(int employeeId, decimal temperature);

        public Task<Employee> GetEmployeeAsync(int employeeId);

        public Task<Employee> GetByEmployeeNumberAsync(string employeeNumber);

        public Task<int> UpdateEmployee(int id, EmployeeDTO employee);
    }
}
