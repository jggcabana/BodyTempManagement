using BodyTemp.Entities.DTOs;
using BodyTemp.Entities.Exceptions;
using BodyTemp.Entities.Models;
using BodyTemp.Repositories.Interfaces;
using BodyTemp.Services.Helpers;
using BodyTemp.Services.Mappers;
using BodyTemp.Services.Services;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyTemp.UnitTests
{
    [TestClass]
    public class EmployeeTests
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeTests()
        {
            _employeeRepository = Substitute.For<IEmployeeRepository>();
        }

        [TestMethod]
        public void FahrenheitToCelsius_Converter_MustReturn_CorrectValue()
        {
            var c = TemperatureConverter.FahrenheitToCelsius(98);
            Assert.AreEqual((decimal)36.7, c);
        }

        [TestMethod]
        public void EmployeeMapper_Return_EmployeeDTO()
        {
            // Arrange

            var date = DateTime.UtcNow;
            var employee = new Employee
            {
                Id = 1,
                EmployeeNumber = "00002",
                FirstName = "Employee",
                LastName = "Test",
                BodyTemperatures = new List<BodyTemperature>
                {
                    new BodyTemperature { DateRecorded = date, Temperature = (decimal)37.5 },
                    new BodyTemperature { DateRecorded = date, Temperature = (decimal)38.5 },
                }
            };

            // Act
            var employeeDto = EmployeeMapper.MapEmployeeToDto(employee);

            // Arrange
            Assert.AreEqual(1, employeeDto.EmployeeId);
            Assert.AreEqual("00002", employeeDto.EmployeeNumber);
            Assert.AreEqual("Employee", employeeDto.FirstName);
            Assert.AreEqual("Test", employeeDto.LastName);
            Assert.IsTrue(employee.BodyTemperatures.Any());
            Assert.AreEqual(2, employeeDto.BodyTemperatures.Count());
            Assert.AreEqual((decimal)37.5, employeeDto.BodyTemperatures[0].Temperature);
            Assert.AreEqual((decimal)38.5, employeeDto.BodyTemperatures[1].Temperature);
        }

        [TestMethod]
        public async Task UpdateEmployee_ThrowsError_If_Employee_NotFound()
        {
            // Arrange
            _employeeRepository.GetByEmployeeNumberAsync(Arg.Any<string>()).Returns(new Employee { Id = 5 });
            var service = new EmployeeService(_employeeRepository);

            try
            {
                // Act
                var result = await service.AddEmployee("00001", "Juan", "Dela Cruz");
                Assert.Fail();
            }
            catch (Exception e)
            {
                // Assert
                Assert.IsInstanceOfType(e, typeof(BodyTempException));
            }
        }

        [TestMethod]
        public async Task AddTemperature_Adds_Temperature()
        {
            // Arrange
            var date = DateTime.UtcNow;
            var employee = new Employee
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Dela Cruz",
                EmployeeNumber = "00001",
                BodyTemperatures = new List<BodyTemperature>
                {
                    new BodyTemperature { Temperature = (decimal)36.7, DateRecorded = date }
                }
            };

            _employeeRepository.AddTemperatureAsync(Arg.Any<int>(), Arg.Any<decimal>()).Returns(employee);
            var service = new EmployeeService(_employeeRepository);

            // Act
            var dto = await service.AddTemperature(1, 98, Entities.Enums.TemperatureUnit.Fahrenheit);
            var temp = dto.BodyTemperatures.FirstOrDefault(x => x.Temperature == (decimal)36.7);

            // Assert 
            Assert.IsTrue(dto.BodyTemperatures.Any());
            Assert.IsNotNull(temp);
            Assert.AreEqual((decimal)36.7, temp.Temperature);
            Assert.AreEqual(date, temp.DateRecorded);
        }
    }
}
