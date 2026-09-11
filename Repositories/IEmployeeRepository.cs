using EmployeeApi.Models;

namespace EmployeeApi.Repositories;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetEmployees();
    Task<Employee?> GetEmployeeById(int id);
    Task<Employee> AddEmployee(Employee employee);
    Task<Employee?> UpdateEmployee(int id, Employee updatedEmployee);
    Task<bool> DeleteEmployee(int id);
}