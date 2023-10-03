using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyTemp.Services.Helpers
{
    public static class TemperatureConverter
    {
        public static decimal FahrenheitToCelsius(decimal temp)
        {
            decimal c = ((temp - 32) * 5 / 9);
            return Math.Round(c * 10) / 10;
        }
    }
}
