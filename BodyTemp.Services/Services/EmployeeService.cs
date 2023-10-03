using BodyTemp.Entities.DTOs;
using BodyTemp.Entities.Enums;
using BodyTemp.Entities.Exceptions;
using BodyTemp.Entities.Models;
using BodyTemp.Repositories.Interfaces;
using BodyTemp.Services.Helpers;
using BodyTemp.Services.Interfaces;
using BodyTemp.Services.Mappers;

namespace BodyTemp.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        public readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeDTO> AddEmployee(string employeeNumber, string firstName, string lastName)
        {
            var exists = (await _employeeRepository.GetByEmployeeNumberAsync(employeeNumber)) != null;

            if (exists)
                throw new BodyTempException("Employee number already exists.");

            var result = await _employeeRepository.AddAsync(new Employee
            {
                Id = 0,
                EmployeeNumber = employeeNumber,
                FirstName = firstName,
                LastName = lastName
            });

            return EmployeeMapper.MapEmployeeToDto(result);
        }

        public async Task<EmployeeDTO> AddTemperature(int employeeId, decimal temperature, TemperatureUnit temperatureUnit)
        {
            if (temperatureUnit == TemperatureUnit.Fahrenheit)
                temperature = TemperatureConverter.FahrenheitToCelsius(temperature);

            var result = await _employeeRepository.AddTemperatureAsync(employeeId, temperature);
            return EmployeeMapper.MapEmployeeToDto(result);
        }

        public async Task<EmployeeDTO> GetEmployee(int employeeId)
        {
            var result = await _employeeRepository.GetEmployeeAsync(employeeId);
            if (result == null)
                throw new NotFoundException("Employee does not exist.");

            return EmployeeMapper.MapEmployeeToDto(result);
        }

        public async Task<IEnumerable<EmployeeDTO>> GetEmployees(
            int? id = null
            , string employeeNumber = ""
            , string firstName = ""
            , string lastName = ""
            , TemperatureUnit tempFormat = TemperatureUnit.Celsius
            , decimal? tempFrom = null
            , decimal? tempTo = null
            , DateTime? dateFrom = null
            , DateTime? dateTo = null
            )
        {
            var employees = new List<EmployeeDTO>();
            var results = await _employeeRepository.GetAllAsync(id, employeeNumber, firstName, lastName, tempFormat, tempFrom, tempTo, dateFrom, dateTo);

            foreach(var result in results)
            {
                employees.Add(EmployeeMapper.MapEmployeeToDto(result));
            }

            return employees;
        }

        public async Task<int> UpdateEmployee(int id, EmployeeDTO employee)
        {
            return await _employeeRepository.UpdateEmployee(id, employee);
        }
    }
}
