using System.ComponentModel.DataAnnotations;

namespace TechTrackerBlazor.Models;

public class Employee
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");

    [Required]
    [MaxLength(40)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(40)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;

    public Address Address { get; set; } = new();

    [Required]
    public string PasswordHash { get; set; } = "demo-hash";

    [Required]
    public string EmployeeRole { get; set; } = "Technician";
    ///indexes to-be-implemented: FirstName, LastName, Email, EmployeeRole  aislopfilledthistex
}

public class Address
{
    [Required]
    public string Street { get; set; } = string.Empty;

    [Required]
    public string City { get; set; } = string.Empty;

    [Required]
    public string State { get; set; } = string.Empty;

    [Required]
    public string Zip { get; set; } = string.Empty;
}
