using EmployeeApi.Data;
using EmployeeApi.Models;

namespace EmployeeApi.Services;

public class EmployeeService
{
    private readonly EmployeeDbContext _context;

    public EmployeeService(EmployeeDbContext context)
    {
        _context = context;
    }

    public List<Employee> GetEmployees()
    {
        return _context.Employees.ToList();
    }
    public Employee AddEmployee(Employee employee)
    {
        _context.Employees.Add(employee);
        _context.SaveChanges();

        return employee;
    }
    public Employee? GetEmployeeById(int id)
    {
        return _context.Employees.FirstOrDefault(e => e.Id == id);
    }
    public Employee? UpdateEmployee(int id, Employee updatedEmployee)
    {
        var employee = _context.Employees.FirstOrDefault(e => e.Id == id);

        if (employee == null)
            return null;

        employee.Name = updatedEmployee.Name;
        employee.Position = updatedEmployee.Position;
        employee.Salary = updatedEmployee.Salary;

        _context.SaveChanges();

        return employee;
    }
public bool DeleteEmployee(int id)
    {
        var employee = _context.Employees.FirstOrDefault(e => e.Id == id);

        if (employee == null)
            return false;

        _context.Employees.Remove(employee);
        _context.SaveChanges();

        return true;
    }
}