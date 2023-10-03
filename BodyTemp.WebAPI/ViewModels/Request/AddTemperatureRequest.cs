using BodyTemp.Entities.Enums;

namespace BodyTemp.WebAPI.ViewModels.Request
{
    public class AddTemperatureRequest
    {
        public decimal Temperature { get; set; }

        public TemperatureUnit TemperatureUnit { get; set; }
    }
}
