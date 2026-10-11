using EmployeeApi.Models;
using EmployeeApi.Services; 
using Microsoft.AspNetCore.Mvc;
using EmployeeApi.DTOs;
using EmployeeApi.Data;

namespace EmployeeApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class EmployeesController: ControllerBase
{
        //private readonly field
        private readonly EmployeeService _employeeService;

        //add a contructor to inject the EmployeeService
        public EmployeesController(EmployeeService employeeService)
        {
            _employeeService= employeeService;
        }
       
        [HttpGet]
        public async Task<ActionResult<List<EmployeeResponseDto>>> GetEmployees(
            [FromQuery] string? position)
        {
            var employees = string.IsNullOrWhiteSpace(position)
                ? await _employeeService.GetEmployees()
                : await _employeeService.GetEmployeesByPosition(position);

            var employeeDtos = employees.Select(employee => new EmployeeResponseDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Position = employee.Position,
                Salary = employee.Salary
            }).ToList();

            return Ok(employeeDtos);
        }
        
        
        [HttpPost]
        public async Task<ActionResult<EmployeeResponseDto>> AddEmployee(
            CreateEmployeeDto employeeDto)
        {
            var employee = new Employee
            {
                Name = employeeDto.Name,
                Position = employeeDto.Position,
                Salary = employeeDto.Salary
            };

            var addedEmployee = await _employeeService.AddEmployee(employee);

            var employeeResponse = new EmployeeResponseDto
            {
                Id = addedEmployee.Id,
                Name = addedEmployee.Name,
                Position = addedEmployee.Position,
                Salary = addedEmployee.Salary
            };

            return CreatedAtAction(
                nameof(GetEmployee),
                new { id = addedEmployee.Id },
                employeeResponse);
        }
       
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeResponseDto>> GetEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }

            var employeeDto = new EmployeeResponseDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Position = employee.Position,
                Salary = employee.Salary
            };

            return Ok(employeeDto);
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeResponseDto>> UpdateEmployee(
            int id, UpdateEmployeeDto employeeDto)
        {
            var employee = new Employee
            {
                Name = employeeDto.Name,
                Position = employeeDto.Position,
                Salary = employeeDto.Salary
            };

            var updatedEmployee = await _employeeService.UpdateEmployee(id, employee);

            if (updatedEmployee == null)
            {
                return NotFound();
            }

            var responseDto = new EmployeeResponseDto
            {
                Id = updatedEmployee.Id,
                Name = updatedEmployee.Name,
                Position = updatedEmployee.Position,
                Salary = updatedEmployee.Salary
            };

            return Ok(responseDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var deleted = await _employeeService.DeleteEmployee(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
 }

