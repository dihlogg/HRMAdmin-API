using System.ComponentModel.DataAnnotations;

namespace AdminHRM.Dtos;

public class EmployeeCreateDto
{
    /// <summary>
    /// First name: required
    /// </summary>
    [Required]
    public string FirstName { get; set; }

    /// <summary>
    /// last name: required
    /// </summary>
    [Required]
    public string LastName { get; set; }

    /// <summary>
    /// job title: dev, test, ba
    /// </summary>
    public string? JobTitle { get; set; }

    /// <summary>
    /// status part time, full time
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Suppervisor Id
    /// </summary>
    public Guid? EmployeeId { get; set; }

    /// <summary>
    /// Unit Id
    /// </summary>
    public Guid? SubUnitId { get; set; }
}