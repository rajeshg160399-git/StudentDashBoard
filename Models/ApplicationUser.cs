using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace StudentDashboard.Models;

public class ApplicationUser
{
    public int Id { get; set; }

    [Required, StringLength(100), Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    [Required, StringLength(50), Display(Name = "Username")]
    public string Username { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(100), Display(Name = "Email Address")]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string Role { get; set; } = "User";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? LastLoginAt { get; set; }
    public bool IsActive { get; set; } = true;

}


//Action Filter
//public class MyActionFilter : IActionFilter
//{
//    public void OnActionExecuting(ActionExecutingContext _context)
//    {
//        Console.WriteLine("");
//    }
//    public void OnActionExecuted(ActionExecutedContext _context)
//    {
//        Console.WriteLine("");
//    }
//}

//Exception Filter

//public class myExceptionFilter : IExceptionFilter
//{
//    public void OnException(ExceptionContext _context)
//    {
//        _context.Result = new ContentResult()
//        {
//            Content = ""
//        };
//        _context.ExceptionHandled = true;
//    }
//}

//Result Filter

//public class MyResultFilter : IResultFilter
//{
//    public void OnResultExecuting(ResultExecutingContext _Context)
//    {
//        Console.WriteLine("");
//    }

//    public void OnResultExecuted(ResultExecutedContext _context)
//    {
//        Console.WriteLine("");
//    }
//}

//Autorization Filter

public class AuthorizationFilter : IAuthorizationFilter 
{
    public void OnAuthorization(AuthorizationFilterContext _context)
    {
        var role = _context.HttpContext.Session.GetString("Role");

        if (role != "Admin")
        {
            _context.Result = new UnauthorizedResult();
        }
    }

}