using Microsoft.EntityFrameworkCore;
using StudentDashboard.Models;

namespace StudentDashboard.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Student> Students => Set<Student>();
    public DbSet<ApplicationUser> Users => Set<ApplicationUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().HasIndex(s => s.Email).IsUnique();
        modelBuilder.Entity<Student>().HasIndex(s => s.EnrollmentNumber).IsUnique();

        modelBuilder.Entity<ApplicationUser>().HasIndex(u => u.Username).IsUnique();
        modelBuilder.Entity<ApplicationUser>().HasIndex(u => u.Email).IsUnique();
        base.OnModelCreating(modelBuilder);
    }
}
