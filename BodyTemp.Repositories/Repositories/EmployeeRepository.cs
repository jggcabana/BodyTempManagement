using BodyTemp.Entities.Enums;
using BodyTemp.Entities.Models;
using BodyTemp.Repositories.Interfaces;
using BodyTemp.Repositories.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyTemp.Repositories.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context)
            : base(context)
        {

        }

        public async Task<IEnumerable<Employee>> GetAllAsync(
            int? id = null
            , string? employeeNumber = null
            , string? firstName = null
            , string? lastName = null
            , TemperatureFormat tempFormat = TemperatureFormat.Celsius
            , decimal? temperatureFrom = null
            , decimal? temperatureTo = null
            , DateTime? dateFrom = null
            , DateTime? dateTo = null
            )
        {
            if (id != null)
                return await _context.Employees.Include(e => e.BodyTemperatures).Where(x => x.Id == id).ToListAsync();

            if (!string.IsNullOrEmpty(employeeNumber))
                return await _context.Employees.Include(e => e.BodyTemperatures).Where(x => x.EmployeeNumber == employeeNumber).ToListAsync();

            IQueryable<Employee> query = _context.Employees.Include(e => e.BodyTemperatures);
            
            if (!String.IsNullOrEmpty(firstName))
                query = query.Where(e => e.FirstName == firstName);
            
            if (!String.IsNullOrEmpty(lastName))
                query = query.Where(e => e.LastName == lastName);

            if (temperatureFrom != null)
                query = query.Where(e => e.BodyTemperatures.Where(t => t.Temperature >= temperatureFrom).Any());

            if (temperatureTo != null)
                query = query.Where(e => e.BodyTemperatures.Where(t => t.Temperature <= temperatureTo).Any());

            if (dateFrom != null)
                query = query.Where(e => e.BodyTemperatures.Where(t => t.DateRecorded <= dateFrom).Any());

            if (dateFrom != null)
                query = query.Where(e => e.BodyTemperatures.Where(t => t.DateRecorded >= dateTo).Any());

            return await query.ToListAsync();
        }
    }
}
