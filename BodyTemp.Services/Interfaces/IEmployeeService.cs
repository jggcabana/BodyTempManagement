using BodyTemp.Entities.DTOs;
using BodyTemp.Entities.Enums;
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
            , TemperatureFormat tempFormat = TemperatureFormat.Celsius
            , decimal? tempFrom = null
            , decimal? tempTo = null
            , DateTime? dateFrom = null
            , DateTime? dateTo = null
            );

        public Task<IEnumerable<EmployeeDTO>> GetEmployee(int employeeId);
    }
}
