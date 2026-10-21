using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentDashboard.Data;
namespace StudentDashboard.Controllers;
[Authorize]
public class ReportsController(IStudentRepository students) : Controller { public async Task<IActionResult> Index() => View(await students.GetAllAsync()); }
