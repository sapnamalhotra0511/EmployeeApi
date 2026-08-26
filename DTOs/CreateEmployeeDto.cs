using System.ComponentModel.DataAnnotations;
namespace EmployeeApi.DTOs;

public class CreateEmployeeDto
{
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Position { get; set; } = string.Empty;

    [Range(1, 10000000)]
    public decimal Salary { get; set; }
}