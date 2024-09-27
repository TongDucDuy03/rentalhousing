using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using đatn.Models;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly RentalHousingDbContext _context;
    private readonly PasswordHasher<User> _passwordHasher;

    public UserController(RentalHousingDbContext context)
    {
        _context = context;
        _passwordHasher = new PasswordHasher<User>(); // Khởi tạo PasswordHasher
    }

    // Đăng ký người dùng
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User model)
    {
        if (model == null)
        {
            return BadRequest("Invalid user data.");
        }

        // Kiểm tra xem email đã tồn tại chưa
        if (await _context.Users.AnyAsync(u => u.Email == model.Email))
        {
            return BadRequest("Email already exists.");
        }

        // Mã hóa mật khẩu
        model.Password = HashPassword(model);
        _context.Users.Add(model);
        await _context.SaveChangesAsync();

        return Ok("User registered successfully.");
    }

    // Đăng nhập người dùng
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        if (model == null)
        {
            return BadRequest("Invalid login data.");
        }

        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
        if (user == null || !VerifyPassword(model.Password, user.Password))
        {
            return Unauthorized("Invalid credentials.");
        }

        return Ok("Login successful.");
    }

    // Hàm mã hóa mật khẩu
    private string HashPassword(User user)
    {
        return _passwordHasher.HashPassword(user, user.Password);
    }

    // Hàm xác minh mật khẩu
    private bool VerifyPassword(string inputPassword, string hashedPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, inputPassword);
        return result == PasswordVerificationResult.Success;
    }
}

// Model cho đăng nhập
public class LoginModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}
