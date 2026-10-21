using Microsoft.AspNetCore.Identity;
using StudentDashboard.Models;

namespace StudentDashboard.Data;

public static class DbInitializer
{
    public static void Seed(ApplicationDbContext context, IPasswordHasher<ApplicationUser> passwordHasher)
    {
        if (!context.Students.Any())
        {
            context.Students.AddRange(
                new Student { FirstName = "Aarav", LastName = "Sharma", Email = "aarav.sharma@example.com", EnrollmentNumber = "STU-1001", DateOfBirth = new DateTime(2003, 5, 12), Course = "Computer Science" },
                new Student { FirstName = "Priya", LastName = "Patel", Email = "priya.patel@example.com", EnrollmentNumber = "STU-1002", DateOfBirth = new DateTime(2004, 2, 23), Course = "Business Administration" });
            context.SaveChanges();
        }

        if (!context.Users.Any())
        {
            var adminUser = new ApplicationUser
            {
                FullName = "System Administrator",
                Username = "admin",
                Email = "admin@example.com",
                Role = "Administrator",
                CreatedAt = DateTime.UtcNow
            };
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin@123");
            context.Users.Add(adminUser);
            context.SaveChanges();
        }
    }
}
