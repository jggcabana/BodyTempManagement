using BodyTemp.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyTemp.Services.Interfaces
{
    public interface IEmployeeService 
    {
        public Task<IEnumerable<EmployeeDTO>> GetEmployees();

        public Task<IEnumerable<EmployeeDTO>> GetEmployee(int employeeId);
    }
}
