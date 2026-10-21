using System.ComponentModel.DataAnnotations;

namespace StudentDashboard.Models;

public class ApplicationUser
{
    public int Id { get; set; }

    [Required, StringLength(100), Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    [Required, StringLength(50), Display(Name = "Username")]
    public string Username { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(100), Display(Name = "Email Address")]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string Role { get; set; } = "User";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? LastLoginAt { get; set; }
}
