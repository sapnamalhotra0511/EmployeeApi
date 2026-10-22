
using EmployeeApi.Models;
using EmployeeApi.Repositories;
using EmployeeApi.Services;
using Moq;

namespace EmployeeApi.Test;

public class UnitTest1
{
    [Fact]
    public async Task GetEmployeesByPosition_ReturnsMatchingEmployees()
    {
        // Arrange: prepare sample employees
        var employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "Asha", Position = "Developer", Salary = 50000 },
            new Employee { Id = 2, Name = "Rahul", Position = "Manager", Salary = 60000 },
            new Employee { Id = 3, Name = "Priya", Position = "Senior Developer", Salary = 55000 }
        };

        var mockRepository = new Mock<IEmployeeRepository>();

        mockRepository
            .Setup(repo => repo.GetEmployees())
            .ReturnsAsync(employees);

        var service = new EmployeeService(mockRepository.Object);

        // Act: search for developer positions
        var result = await service.GetEmployeesByPosition("developer");

        // Assert: two employees should match
        Assert.Equal(2, result.Count);
        Assert.Contains(result, e => e.Name == "Asha");
        Assert.Contains(result, e => e.Name == "Priya");
    }

[Fact]
public async Task GetEmployeesByPosition_ReturnsEmptyList_WhenNoEmployeesMatch()
{
    // Arrange
    var employees = new List<Employee>
    {
        new Employee { Id = 1, Name = "Asha", Position = "Developer", Salary = 50000 },
        new Employee { Id = 2, Name = "Rahul", Position = "Manager", Salary = 60000 }
    };

    var mockRepository = new Mock<IEmployeeRepository>();

    mockRepository
        .Setup(repo => repo.GetEmployees())
        .ReturnsAsync(employees);

    var service = new EmployeeService(mockRepository.Object);

    // Act
    var result = await service.GetEmployeesByPosition("Accountant");

    // Assert
    Assert.Empty(result);
}

}
