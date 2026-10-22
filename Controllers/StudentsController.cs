using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentDashboard.Data;
using StudentDashboard.Models;

namespace StudentDashboard.Controllers;

[Authorize]
public class StudentsController(IStudentRepository students) : Controller
{
    public async Task<IActionResult> Index(int page = 1)
    {
        var current = new PageRequest(Math.Max(page, 1), 10);
        var next = current.Next();            // copy: 'current' is unchanged

        ViewBag.Page = current.Page;          // 1 (or the requested page)
        ViewBag.NextPage = next.Page;         // current + 1

        var all = await students.GetAllAsync();
        return View(all.Skip(current.Skip).Take(current.Size).ToList());
    }

    public async Task<IActionResult> Details(int? id) => id is null ? NotFound() : await FindViewAsync(id.Value);
    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Student student)
    {
        if (!ModelState.IsValid) 
            return View(student);
        try {
            await students.AddAsync(student); TempData["Success"] = "Student created successfully."; return RedirectToAction(nameof(Index)); 
        }
        catch (DbUpdateException) { 
            ModelState.AddModelError(string.Empty, "The email address or enrollment number is already in use."); return View(student); 
        }
    }

    public async Task<IActionResult> Edit(int? id) => id is null ? NotFound() : await FindViewAsync(id.Value);

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Student student)
    {
        if (id != student.Id) return NotFound();
        if (!ModelState.IsValid) return View(student);
        var existing = await students.GetByIdAsync(id);   // reference to the tracked entity
        if (existing is null) return NotFound();

        var alias = existing; 
        alias.Email = student.Email; // same object, NOT a copy
        alias.EnrollmentNumber = student.EnrollmentNumber;
        // Reference equality is true; a real copy would need explicit cloning
        bool sameObject = ReferenceEquals(alias, existing);
        try
        {
            await students.UpdateAsync(existing);

            TempData["Success"] = "Student updated successfully.";
                return RedirectToAction(nameof(Index)); 
            }
        catch (DbUpdateException) { 
            ModelState.AddModelError(string.Empty, "Unable to update the student. Check that email and enrollment number are unique."); return View(student); 
        }
    }

    public async Task<IActionResult> Delete(int? id) => id is null ? NotFound() : await FindViewAsync(id.Value);

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        BankAccount account = new BankAccount();

        account.Deposit(1000);
        Console.WriteLine(account.balance);
        //account.balance = 500; // This line will cause a compilation error because Balance has no setter
        //account._balance = 500//  Not allowed: _balance is private



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
