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
            , TemperatureFormat tempFormat = TemperatureFormat.Celsius
            , decimal? temperatureFrom = null
            , decimal? temperatureTo = null
            , DateTime? dateFrom = null
            , DateTime? dateTo = null
        );
    }
}
