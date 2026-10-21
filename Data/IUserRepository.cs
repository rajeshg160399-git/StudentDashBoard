using StudentDashboard.Models;

namespace StudentDashboard.Data;

public interface IUserRepository
{
    Task<ApplicationUser?> GetByIdAsync(int id);
    Task<ApplicationUser?> GetByUsernameAsync(string username);
    Task<ApplicationUser?> GetByEmailAsync(string email);
    Task<ApplicationUser?> GetByUsernameOrEmailAsync(string usernameOrEmail);
    Task<bool> ExistsByUsernameAsync(string username);
    Task<bool> ExistsByEmailAsync(string email);
    Task AddAsync(ApplicationUser user);
    Task UpdateAsync(ApplicationUser user);
}
