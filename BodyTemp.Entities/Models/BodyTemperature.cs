using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyTemp.Entities.Models
{
    public class BodyTemperature : Entity
    {
        public decimal Temperature { get; set; }

        public DateTime? DateRecorded { get; set; } = DateTime.UtcNow;

        public Employee Employee { get; set; }
    }
}
