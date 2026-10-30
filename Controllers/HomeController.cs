using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StudentDashboard.Data;
using StudentDashboard.Models;
using System.Diagnostics;

namespace StudentDashboard.Controllers;

[Authorize]
public class HomeController(IStudentRepository students) : Controller
{
    //[ServiceFilter(typeof(MyActionFilter))]
    //  [ServiceFilter(typeof(MyResultFilter))]
    public async Task<IActionResult> Index() => View(new DashboardViewModel { StudentCount = await students.CountAsync() });
    [AllowAnonymous]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
