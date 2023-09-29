using BodyTemp.Entities.DTOs;
using BodyTemp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyTemp.Services.Mappers
{
    public static class EmployeeMapper
    {
        public static EmployeeDTO MapEmployeeToDto(Employee employee)
        {
            return new EmployeeDTO
            {
                EmployeeId = employee.Id,
                EmployeeNumber = employee.EmployeeNumber,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                BodyTemperatures = employee.BodyTemperatures.Select(x => new BodyTemperature
                {
                    Temperature = x.Temperature,
                    DateRecorded = x.DateRecorded
                }).ToList()
            };
        }
    }
}
