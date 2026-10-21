using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentDashboard.Data;
using StudentDashboard.Models;

namespace StudentDashboard.Controllers;

[Authorize]
public class StudentsController(IStudentRepository students) : Controller
{
    public async Task<IActionResult> Index() => View(await students.GetAllAsync());

    public async Task<IActionResult> Details(int? id) => id is null ? NotFound() : await FindViewAsync(id.Value);
    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Student student)
    {
        if (!ModelState.IsValid) return View(student);
        try { await students.AddAsync(student); TempData["Success"] = "Student created successfully."; return RedirectToAction(nameof(Index)); }
        catch (DbUpdateException) { ModelState.AddModelError(string.Empty, "The email address or enrollment number is already in use."); return View(student); }
    }

    public async Task<IActionResult> Edit(int? id) => id is null ? NotFound() : await FindViewAsync(id.Value);

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Student student)
    {
        if (id != student.Id) return NotFound();
        if (!ModelState.IsValid) return View(student);
        try { await students.UpdateAsync(student); TempData["Success"] = "Student updated successfully."; return RedirectToAction(nameof(Index)); }
        catch (DbUpdateException) { ModelState.AddModelError(string.Empty, "Unable to update the student. Check that email and enrollment number are unique."); return View(student); }
    }

    public async Task<IActionResult> Delete(int? id) => id is null ? NotFound() : await FindViewAsync(id.Value);

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var student = await students.GetByIdAsync(id);
        if (student is null) return NotFound();
        try { await students.DeleteAsync(student); TempData["Success"] = "Student deleted successfully."; }
        catch (DbUpdateException) { TempData["Error"] = "The student could not be deleted. Please try again."; }
        return RedirectToAction(nameof(Index));
    }

    private async Task<IActionResult> FindViewAsync(int id)
    {
        var student = await students.GetByIdAsync(id);
        return student is null ? NotFound() : View(student);
    }
}
