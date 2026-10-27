using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentDashboard.Data;
using StudentDashboard.Models;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace StudentDashboard.Controllers;

[Authorize]
public class StudentsController : Controller
{
    // with DI
    private readonly IStudentRepository _student;
    private readonly ApplicationDbContext _context;

    public StudentsController(IStudentRepository student, ApplicationDbContext context)
    {
        _student = student;
        _context = context;
    }



    //witout DI no Interface

    //private readonly StudentRepository _student;
    //    public  StudentsController()
    //{
    //    _student = new StudentRepository(_context);
    //}

    public async Task<IActionResult> Index(int page = 1)
    {
        var current = new PageRequest(Math.Max(page, 1), 10);
        var next = current.Next();            // copy: 'current' is unchanged

        ViewBag.Page = current.Page;          // 1 (or the requested page)
        ViewBag.NextPage = next.Page;         // current + 1

        var all = await _student.GetAllAsync();
        return View(all.Skip(current.Skip).Take(current.Size).ToList());
    }

    public async Task<IActionResult> Details(int? id) => id is null ? NotFound() : await FindViewAsync(id.Value);
    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Student student)
    {
        if (!ModelState.IsValid)
            return View(student);
        try
        {
            await _student.AddAsync(student); TempData["Success"] = "Student created successfully."; return RedirectToAction(nameof(Index));


        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(string.Empty, "The email address or enrollment number is already in use."); return View(student);
        }
    }

    public async Task<IActionResult> Edit(int? id) => id is null ? NotFound() : await FindViewAsync(id.Value);

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Student student)
    {
        List<Student> objStudent = new List<Student>();
        var studentIds = objStudent.Where(s => s.FirstName.StartsWith("A")).Select(s => s.Id).ToList();
        if (id != student.Id) return NotFound();
        if (!ModelState.IsValid) return View(student);
        var existing = await _student.GetByIdAsync(id);   // reference to the tracked entity
        if (existing is null) return NotFound();
        //PolyMorphism
        Trees[] objTree = { new NeemTree(), new LemonTree() };

        foreach (var i in objTree)
        {
            i.UsefulTrees();
        }
        //Method OverLoading
        Calculation Objcal = new Calculation();
        var addData = Objcal.Add(4, 5, 6);
        var newAddData = Objcal.Add(4, 5);

        //Method OverRidding
        Birds objBird = new Ostrich();
        objBird.Fly();

        //async /await
        await GetMessageasync();
        //task.Run
        await GetcalculatedDataAsync();

        var alias = existing;
        alias.Email = student.Email; // same object, NOT a copy
        alias.EnrollmentNumber = student.EnrollmentNumber;
        // Reference equality is true; a real copy would need explicit cloning
        bool sameObject = ReferenceEquals(alias, existing);
        try
        {
            await _student.UpdateAsync(existing);

            TempData["Success"] = "Student updated successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(string.Empty, "Unable to update the student. Check that email and enrollment number are unique."); return View(student);
        }
    }

    public async Task<IActionResult> Delete(int? id) => id is null ? NotFound() : await FindViewAsync(id.Value);

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        //IEnumerable
        //IEnumerable<Student> Students = GetUsers();
        //var local = Students.Where(s => s.Email.StartsWith("a")).ToList();
        //IQueryable

        IQueryable<Student> Query = _context.Students;
        var Students = Query.Where(s => s.IsActive == true);

        BankAccount account = new BankAccount();

        account.Deposit(1000);
        Console.WriteLine(account.balance);
        //account.balance = 500; // This line will cause a compilation error because Balance has no setter
        //account._balance = 500//  Not allowed: _balance is private
        //Inheritance Example
        Dog objDog = new Dog();
        objDog.Eat();
        //abstract Class
        Cat objCat = new Cat();
        objCat.Sleep();
        objCat.Run();




        var student = await _student.GetByIdAsync(id);
        if (student is null) return NotFound();
        try { await _student.DeleteAsync(student); TempData["Success"] = "Student deleted successfully."; }
        catch (DbUpdateException) { TempData["Error"] = "The student could not be deleted. Please try again."; }
        return RedirectToAction(nameof(Index));
    }

    private async Task<IActionResult> FindViewAsync(int id)
    {
        var student = await _student.GetByIdAsync(id);

        return student is null ? NotFound() : View(student);

    }

    //DeadLock
    public ActionResult GetStudent(int id)
    {
        var student = _student.GetByStudentIdAsync(id).Result;
        return View(student);
    }

    //Synchronus Code
    public void GetData()
    {
        Console.WriteLine("Geting Started");
        Thread.Sleep(5000);
        Console.WriteLine("Finisshed");
    }



    //asynchronus
    public async Task<string> GetMessageasync()
    {
        Console.WriteLine("Message started");
        await Task.Delay(500);
        return "Message Returned";
        
    }


    //task.run example

    public int Calculate()
    {
        int result = 0;
        for(int i = 0;  i<10000000000; i++)
        {
            result = i;
        }
        return result;
    }

    public async Task<int> GetcalculatedDataAsync()
    {
        Task<int> task = Task.Run(Calculate);
        int data = await task;
        return data;

    }
}
