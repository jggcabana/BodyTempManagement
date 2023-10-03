using BodyTemp.Entities.DTOs;
using BodyTemp.Entities.Enums;
using BodyTemp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyTemp.Services.Interfaces
{
    public interface IEmployeeService 
    {
        public Task<IEnumerable<EmployeeDTO>> GetEmployees(
            int? id = null
            , string? employeeNumber = null
            , string? firstName = null
            , string? lastName = null
            , TemperatureUnit tempFormat = TemperatureUnit.Celsius
            , decimal? tempFrom = null
            , decimal? tempTo = null
            , DateTime? dateFrom = null
            , DateTime? dateTo = null
            );

        public Task<EmployeeDTO> AddEmployee(string employeeNumber, string firstName, string lastName);

        public Task<int> UpdateEmployee(int employeeId, EmployeeDTO employee);

        public Task<EmployeeDTO> GetEmployee(int employeeId);

        public Task<EmployeeDTO> AddTemperature(int employeeId, decimal temperature, TemperatureUnit temperatureUnit);
    }
}
