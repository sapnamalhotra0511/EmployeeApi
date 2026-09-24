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
        public async Task<ActionResult<List<Employee>>> GetEmployees()
        {

             return await _employeeService.GetEmployees();
        }
        
        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(CreateEmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = new Employee
            {
                Name = employeeDto.Name,
                Position = employeeDto.Position,
                Salary = employeeDto.Salary
            };
            var addedEmployee = await _employeeService.AddEmployee(employee);

            return Ok(addedEmployee);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> UpdateEmployee( int id,UpdateEmployeeDto employeeDto )
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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

            return updatedEmployee;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteEmployee(int id)
        {
            var deleted = await _employeeService.DeleteEmployee(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
 }

