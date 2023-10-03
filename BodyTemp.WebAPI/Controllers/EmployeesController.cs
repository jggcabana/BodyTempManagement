using BodyTemp.Entities.Enums;
using BodyTemp.Repositories.Interfaces;
using BodyTemp.Services.Interfaces;
using BodyTemp.WebAPI.ViewModels.Request;
using BodyTemp.WebAPI.ViewModels.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Text.Json.Nodes;

namespace BodyTemp.WebAPI.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeesController> _logger;
        private readonly string _baseUrl;

        public EmployeesController(IEmployeeService employeeService, ILogger<EmployeesController> logger, IConfiguration config)
        {
            _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _baseUrl = config.GetValue<string>("BaseUrl") ?? throw new ArgumentNullException(nameof(config));
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees(
            [FromQuery] int? id = null
            , [FromQuery] string? employeeNumber = null
            , [FromQuery] string? firstName = null
            , [FromQuery] string? lastName = null
            , [FromQuery] TemperatureUnit tempFormat = TemperatureUnit.Celsius
            , [FromQuery] decimal? tempFrom = null
            , [FromQuery] decimal? tempTo = null
            , [FromQuery] DateTime? dateFrom = null
            , [FromQuery] DateTime? dateTo = null
            )
        {
            var result = await _employeeService.GetEmployees(id, employeeNumber, firstName, lastName, tempFormat, tempFrom, tempTo, dateFrom, dateTo);
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
            var result = await _employeeService.GetEmployee(employeeId);
            return Ok(new BaseResponse
            {
                Data = result
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] CreateEmployeeRequest request)
        {
            var result = await _employeeService.AddEmployee(request.EmployeeNumber, request.FirstName, request.LastName);

            var url = $"{_baseUrl}/api/employees/{result.EmployeeId}";
            return Created(url, new BaseResponse
            {
                Message = $"New employee created at {url}",
                Success = true,
                Data = result
            });
        }

        [HttpPost]
        [Route("{employeeId}/temperature")]
        public async Task<IActionResult> AddTemperature(int employeeId, [FromBody] AddTemperatureRequest request)
        {
            var result = await _employeeService.AddTemperature(employeeId, request.Temperature, request.TemperatureUnit);
            return Ok(new BaseResponse
            {
                Success = true,
                Message = "Temperature added.",
                Data = result
            });
        }

        [HttpPut]
        [Route("{employeeId}")]
        public async Task<IActionResult> UpdateEmployee(int employeeId, [FromBody] UpdateEmployeeRequest request)
        {
            var result = await _employeeService.UpdateEmployee(employeeId, request);
            return Ok(new BaseResponse
            {
                Success = true,
                Message = $"Updated {result} record(s)."
            });
        }
    }
}
