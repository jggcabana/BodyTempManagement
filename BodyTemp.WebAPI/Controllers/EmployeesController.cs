using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BodyTemp.WebAPI.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(ILogger<EmployeesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{employeeId}")]
        public async Task<IActionResult> GetEmployee(int employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
