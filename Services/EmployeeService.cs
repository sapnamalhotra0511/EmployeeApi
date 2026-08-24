using EmployeeApi.Models;
namespace EmployeeApi.Services
{
    public class EmployeeService
    {
            private readonly List<Employee> employees  = new()
            {
                new Employee { Id = 1, Name = "John Doe", Position = "Software Engineer", Salary = 60000 },
                new Employee { Id = 2, Name = "Jane Smith", Position = "Project Manager", Salary = 75000 },
                new Employee { Id = 3, Name = "Mike Johnson", Position = "QA Analyst", Salary = 50000 } 
            };
            public  List<Employee> GetEmployees()
            { 
                return employees;
            }

            public Employee AddEmployee(Employee employee)
            {
                employee.Id = employees.Count + 1;
                employees.Add(employee);

                return employee;
            }
            public Employee? GetEmployeeById(int id)
            {
                return employees.FirstOrDefault(e => e.Id == id);   
            }  
            public Employee? UpdateEmployee(int id, Employee updatedEmployee)
            {
                var employee = employees.FirstOrDefault(e => e.Id == id);

                if (employee == null)
                {
                    return null;
                }

                employee.Name = updatedEmployee.Name;
                employee.Position = updatedEmployee.Position;
                employee.Salary = updatedEmployee.Salary;

                return employee;
            }  
            public bool DeleteEmployee(int id)
            {
                var employee = employees.FirstOrDefault(e => e.Id == id);

                if (employee == null)
                {
                    return false;
                }

                employees.Remove(employee);

                return true;
            }

     }
    }

