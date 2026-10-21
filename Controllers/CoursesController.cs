using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace StudentDashboard.Controllers;
[Authorize]
public class CoursesController : Controller { public IActionResult Index() => View(); }
