using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyTemp.Entities.Models
{
    public class Employee : Entity
    {
        public string EmployeeNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<BodyTemperature> BodyTemperatures { get; set; } = new List<BodyTemperature>();
    }
}
