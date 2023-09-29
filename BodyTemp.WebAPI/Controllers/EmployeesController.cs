using BodyTemp.Repositories.Interfaces;
using BodyTemp.Services.Interfaces;
using BodyTemp.WebAPI.ViewModels.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BodyTemp.WebAPI.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(IEmployeeService employeeService, ILogger<EmployeesController> logger)
        {
            _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var result = await _employeeService.GetEmployees();
            return Ok(new BaseResponse
            {
                Success = true,
                Message = "",
                Data = result
            });
        }

        [HttpGet]
        [Route("{employeeId}")]
        public async Task<IActionResult> GetEmployee(int employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
