using BodyTemp.Entities.DTOs;
using BodyTemp.Repositories.Interfaces;
using BodyTemp.Services.Interfaces;
using BodyTemp.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyTemp.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        public readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetEmployee(int employeeId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EmployeeDTO>> GetEmployees()
        {
            var employees = new List<EmployeeDTO>();
            var results = await _employeeRepository.GetAllAsync();

            foreach(var result in results)
            {
                employees.Add(EmployeeMapper.MapEmployeeToDto(result));
            }

            return employees;
        }
    }
}
