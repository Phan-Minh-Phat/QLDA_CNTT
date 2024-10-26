using DeTai4.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using DeTai4.Reponsitories.Repositories.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;

public class LoginModel : PageModel
{
    private readonly IUserService _userService;

    public LoginModel(IUserService userService)
    {
        _userService = userService;
    }

    [BindProperty]
    public string Username { get; set; }

    [BindProperty]
    public string Password { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        // Xác thực người dùng
        var isValidUser = await _userService.ValidateUserAsync(Username, Password);
        if (!isValidUser)
        {
            ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
            return Page();
        }

        // Lấy người dùng theo tên đăng nhập để kiểm tra vai trò
        var user = await _userService.GetUserByUsernameAsync(Username);
        if (user == null)
        {
            ModelState.AddModelError("", "Người dùng không tồn tại.");
            return Page();
        }

        // Tạo các claims và lưu vào session/cookie để sử dụng cho các yêu cầu sau
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("UserId", user.UserId.ToString()),
            new Claim(ClaimTypes.Role, user.Role ?? string.Empty)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        return RedirectBasedOnRole(user.Role);
    }

    private IActionResult RedirectBasedOnRole(string? role)
    {
        return role switch
        {
            "Customer" => RedirectToPage("/Customer/CustomerDashboard"),
            "Manager" => RedirectToPage("/Manager/ManagerDashboard"),
            "ConsultingStaff" => RedirectToPage("/Consulting_Staff/ConsultingDashboard"),
            "DesignStaff" => RedirectToPage("/Design_Staff/DesignDashboard"),
            "ConstructionStaff" => RedirectToPage("/Construction_Staff/ConstructionDashboard"),
            _ => RedirectToPage("/User/Login")
        };
    }
}
