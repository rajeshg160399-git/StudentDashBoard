using Microsoft.EntityFrameworkCore;
using StudentDashboard.Models;

namespace StudentDashboard.Data;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public Task<ApplicationUser?> GetByIdAsync(int id) =>
        context.Users.FindAsync(id).AsTask();

    public Task<ApplicationUser?> GetByUsernameAsync(string username) =>
        context.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == username.Trim().ToLower());

    public Task<ApplicationUser?> GetByEmailAsync(string email) =>
        context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.Trim().ToLower());

    public Task<ApplicationUser?> GetByUsernameOrEmailAsync(string usernameOrEmail)
    {
        var normalized = usernameOrEmail.Trim().ToLower();
        return context.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == normalized || u.Email.ToLower() == normalized);
    }

    public Task<bool> ExistsByUsernameAsync(string username) =>
        context.Users.AnyAsync(u => u.Username.ToLower() == username.Trim().ToLower());

    public Task<bool> ExistsByEmailAsync(string email) =>
        context.Users.AnyAsync(u => u.Email.ToLower() == email.Trim().ToLower());

    public async Task AddAsync(ApplicationUser user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ApplicationUser user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();
    }
}
