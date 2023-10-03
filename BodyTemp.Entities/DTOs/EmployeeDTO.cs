using BodyTemp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyTemp.Entities.DTOs
{
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }

        public string EmployeeNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<BodyTemperatureDTO> BodyTemperatures { get; set; } = new List<BodyTemperatureDTO>();
    }
}
