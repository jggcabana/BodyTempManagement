using BodyTemp.Entities.DTOs;
using BodyTemp.Entities.Enums;
using BodyTemp.Entities.Exceptions;
using BodyTemp.Entities.Models;
using BodyTemp.Repositories.Interfaces;
using BodyTemp.Repositories.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace BodyTemp.Repositories.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context)
            : base(context)
        {

        }

        public async Task<Employee> AddTemperatureAsync(int employeeId, decimal temperature)
        {
            var emp = _context.Employees.Include(e => e.BodyTemperatures).FirstOrDefault(e => e.Id == employeeId);
            if (emp == null)
                throw new BodyTempException("Employee not found.");

            emp.BodyTemperatures.Add(new BodyTemperature
            {
                Temperature = temperature,
                DateRecorded = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return emp;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(
            int? id = null
            , string? employeeNumber = null
            , string? firstName = null
            , string? lastName = null
            , TemperatureUnit tempFormat = TemperatureUnit.Celsius
            , decimal? temperatureFrom = null
            , decimal? temperatureTo = null
            , DateTime? dateFrom = null
            , DateTime? dateTo = null
            )
        {

            var query = _context.Employees.Include(e => e.BodyTemperatures)
                            .Where(x => id == null || x.Id == id)
                            .Where(x => employeeNumber == null || x.EmployeeNumber == employeeNumber)
                            .Where(x => firstName == null || x.FirstName == firstName)
                            .Where(x => lastName == null || x.LastName == lastName);

            bool hasQueryParams = temperatureFrom != null || temperatureTo != null || dateFrom != null || dateTo != null;

            if (hasQueryParams)
            {
                query = query.Where(x => x.BodyTemperatures.Any())
                               .Select(x => new Employee
                               {
                                   Id = x.Id,
                                   EmployeeNumber = x.EmployeeNumber,
                                   FirstName = x.FirstName,
                                   LastName = x.LastName,
                                   BodyTemperatures = x.BodyTemperatures
                                        .Where(t => temperatureFrom == null || t.Temperature >= temperatureFrom)
                                        .Where(t => temperatureTo == null || t.Temperature <= temperatureTo)
                                        .Where(t => dateFrom == null || t.DateRecorded >= dateFrom)
                                        .Where(t => dateTo == null || t.DateRecorded >= dateTo)
                                        .ToList()
                               });

                query = query.Where(x => x.BodyTemperatures.Any());
            }

            return await query.ToListAsync();
        }

        public async Task<Employee> GetByEmployeeNumberAsync(string employeeNumber)
        {
            return await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeNumber == employeeNumber);
        }

        public async Task<Employee> GetEmployeeAsync(int employeeId)
        {
            return await _context.Employees.Include(e => e.BodyTemperatures).FirstOrDefaultAsync(e => e.Id == employeeId);
        }

        public async Task<int> UpdateEmployee(int id, EmployeeDTO employee)
        {
            var existing = await this.GetEmployeeAsync(id);

            if (existing == null)
                throw new NotFoundException("Employee does not exist.");

            _context.Entry(existing).CurrentValues.SetValues(employee);

            return await _context.SaveChangesAsync();
        }
    }
}
