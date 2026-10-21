using System.ComponentModel.DataAnnotations;

namespace StudentDashboard.Models;

public class Student
{
    public int Id { get; set; }
    [Required, StringLength(50), Display(Name = "First name")]
    public string FirstName { get; set; } = string.Empty;
    [Required, StringLength(50), Display(Name = "Last name")]
    public string LastName { get; set; } = string.Empty;
    [Required, EmailAddress, StringLength(100)]
    public string Email { get; set; } = string.Empty;
    [Required, StringLength(30), Display(Name = "Enrollment number")]
    public string EnrollmentNumber { get; set; } = string.Empty;
    [Required, DataType(DataType.Date), Display(Name = "Date of birth")]
    public DateTime? DateOfBirth { get; set; }
    [Required, StringLength(100)]
    public string Course { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
}
