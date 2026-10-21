using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentDashboard.Data;
using StudentDashboard.Models;

namespace StudentDashboard.Controllers;

public class AccountController(IUserRepository userRepository, IPasswordHasher<ApplicationUser> passwordHasher) : Controller
{
    [HttpGet]
    public IActionResult Login(string? returnUrl) =>
        User.Identity?.IsAuthenticated == true ? RedirectToAction("Index", "Home") : View(new LoginViewModel { ReturnUrl = returnUrl });

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var user = await userRepository.GetByUsernameOrEmailAsync(model.Username);
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return View(model);
        }

        var verificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);
        if (verificationResult == PasswordVerificationResult.Failed)
        {
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return View(model);
        }

        if (verificationResult == PasswordVerificationResult.SuccessRehashNeeded)
        {
            user.PasswordHash = passwordHasher.HashPassword(user, model.Password);
        }

        user.LastLoginAt = DateTime.UtcNow;
        await userRepository.UpdateAsync(user);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role),
            new("FullName", user.FullName)
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)),
            new AuthenticationProperties { IsPersistent = model.RememberMe });

        return LocalRedirect(Url.IsLocalUrl(model.ReturnUrl) ? model.ReturnUrl! : Url.Action("Index", "Home")!);
    }

    [HttpGet]
    public IActionResult Register() =>
        User.Identity?.IsAuthenticated == true ? RedirectToAction("Index", "Home") : View(new RegisterViewModel());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        // Check whether username is already taken
        if (await userRepository.ExistsByUsernameAsync(model.Username))
        {
            ModelState.AddModelError(nameof(model.Username), "This username is already taken. Please choose another.");
        }

        // Check whether email is already registered
        if (await userRepository.ExistsByEmailAsync(model.Email))
        {
            ModelState.AddModelError(nameof(model.Email), "This email address is already registered. Please sign in or use another email.");
        }

        if (!ModelState.IsValid) return View(model);

        var user = new ApplicationUser
        {
            FullName = model.FullName.Trim(),
            Username = model.Username.Trim(),
            Email = model.Email.Trim().ToLowerInvariant(),
            Role = "User",
            CreatedAt = DateTime.UtcNow
        };

        // Securely hash password using ASP.NET Core Identity PasswordHasher (PBKDF2 with HMAC-SHA256)
        user.PasswordHash = passwordHasher.HashPassword(user, model.Password);

        try
        {
            await userRepository.AddAsync(user);
            TempData["Success"] = "Registration successful! Please sign in with your new credentials.";
            return RedirectToAction(nameof(Login));
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(string.Empty, "Unable to register at this time. The username or email might already be in use.");
            return View(model);
        }
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }

    public IActionResult AccessDenied() => View();
}
