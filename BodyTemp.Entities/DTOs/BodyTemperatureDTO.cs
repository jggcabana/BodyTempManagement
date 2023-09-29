using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyTemp.Entities.DTOs
{
    public class BodyTemperatureDTO
    {
        public decimal Temperature { get; set; }

        public DateTime? DateRecorded { get; set; }
    }
}
