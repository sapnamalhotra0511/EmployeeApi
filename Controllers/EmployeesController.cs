using EmployeeApi.Models;
using EmployeeApi.Services; 
using Microsoft.AspNetCore.Mvc;
using EmployeeApi.DTOs;

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
        public ActionResult<List<Employee>> GetEmployees()
        {
            return _employeeService.GetEmployees();
            
        }
        
        [HttpPost]
        public ActionResult<Employee> AddEmployee(CreateEmployeeDto employeeDto)
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
            var addedEmployee = _employeeService.AddEmployee(employee);

            return Ok(addedEmployee);
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPut("{id}")]
        public ActionResult<Employee> UpdateEmployee( int id,UpdateEmployeeDto employeeDto )
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

            var updatedEmployee = _employeeService.UpdateEmployee(id, employee);


            if (updatedEmployee == null)
            {
                return NotFound();
            }

            return updatedEmployee;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var deleted = _employeeService.DeleteEmployee(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
 }

