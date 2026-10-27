using Microsoft.EntityFrameworkCore;
using StudentDashboard.Models;

namespace StudentDashboard.Data;

public class StudentRepository(ApplicationDbContext context) : IStudentRepository
{
    public Task<List<Student>> GetAllAsync() => context.Students.AsNoTracking().OrderBy(s => s.LastName).ThenBy(s => s.FirstName).ToListAsync();
    public Task<Student?> GetByIdAsync(int id) => context.Students.FindAsync(id).AsTask();
    public async Task AddAsync(Student student) { context.Students.Add(student); await context.SaveChangesAsync(); }
    public async Task UpdateAsync(Student student) { context.Students.Update(student); await context.SaveChangesAsync(); }
    public async Task DeleteAsync(Student student) { context.Students.Remove(student); await context.SaveChangesAsync(); }
    public Task<int> CountAsync() => context.Students.CountAsync();
    public async Task<Student?> GetByStudentIdAsync(int id)
    {
        var student = await context.Students.FindAsync(id); // wants to resume on request context
        return student;
    }
}
