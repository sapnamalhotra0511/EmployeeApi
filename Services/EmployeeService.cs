using EmployeeApi.Models;
using EmployeeApi.Repositories;

namespace EmployeeApi.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<List<Employee>> GetEmployees()
    {
        return await _employeeRepository.GetEmployees();
    }
    public async Task<Employee> AddEmployee(Employee employee)
    {
        return await _employeeRepository.AddEmployee(employee);
    }
    
    
        public async Task<Employee?> GetEmployeeById(int id)
    {
        return await _employeeRepository.GetEmployeeById(id);
    }
    public async Task<Employee?> UpdateEmployee(int id, Employee updatedEmployee)
    {
        return await _employeeRepository.UpdateEmployee(id, updatedEmployee);
    }
    public async Task<bool> DeleteEmployee(int id)
    {
        return await _employeeRepository.DeleteEmployee(id);
    }
}