using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentDashboard.Data;
using StudentDashboard.Models;

namespace StudentDashboard.Controllers;

[Authorize]
public class HomeController(IStudentRepository students) : Controller
{
    public async Task<IActionResult> Index() => View(new DashboardViewModel { StudentCount = await students.CountAsync() });
    [AllowAnonymous]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
