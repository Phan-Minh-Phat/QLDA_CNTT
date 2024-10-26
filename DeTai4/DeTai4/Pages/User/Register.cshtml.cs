using DeTai4.Reponsitories.Repositories.Entities;
using DeTai4.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

public class RegisterModel : PageModel
{
    private readonly IUserService _userService;

    public RegisterModel(IUserService userService)
    {
        _userService = userService;
    }

    [BindProperty]
    public string Username { get; set; }

    [BindProperty]
    public string Password { get; set; }

    [BindProperty]
    public string FullName { get; set; }

    [BindProperty]
    public string? Email { get; set; }

    [BindProperty]
    public string? PhoneNumber { get; set; }

    [BindProperty]
    public string Role { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        // Kiểm tra xem người dùng đã tồn tại chưa
        var existingUser = await _userService.GetUserByUsernameAsync(Username);
        if (existingUser != null)
        {
            ModelState.AddModelError("", "Tên đăng nhập đã tồn tại.");
            return Page();
        }

        // Tạo người dùng mới
        var newUser = new User
        {
            Username = Username,
            PasswordHash = HashPassword(Password), // Sử dụng hàm mã hóa mật khẩu
            FullName = FullName,
            Email = Email,
            PhoneNumber = PhoneNumber,
            Role = Role
        };

        // Gọi Service để tạo User
        await _userService.CreateUserAsync(newUser);

        // Chuyển hướng sau khi đăng ký thành công
        return RedirectToPage("/User/Login");
    }

    private string HashPassword(string password)
    {
        // Mã hóa mật khẩu (sử dụng cách mã hóa an toàn hơn trong thực tế)
        return password;
    }
}
