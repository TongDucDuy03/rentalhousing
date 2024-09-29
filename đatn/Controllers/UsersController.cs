using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using đatn.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

    // Endpoint đăng ký người dùng
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        if (model == null)
        {
            return BadRequest("Dữ liệu người dùng không hợp lệ.");
        }

        // Kiểm tra xem email đã tồn tại chưa
        if (await _context.Users.AnyAsync(u => u.Email == model.Email))
        {
            return BadRequest("Email đã tồn tại.");
        }

        var user = new User
        {
            Username = model.Username,
            Email = model.Email,
            Role = model.Role ?? "tenant", // Cung cấp vai trò mặc định nếu Role là null
            Password = HashPassword(model.Password), // Mã hóa mật khẩu
            user_image = model.UserImage, // Lưu đường dẫn ảnh
            phone_number = model.PhoneNumber, // Lưu số điện thoại
            Age = model.Age, // Lưu tuổi
            Gender = model.Gender // Lưu giới tính
        };

        _context.Users.Add(user); // Thêm đối tượng User vào DbContext
        await _context.SaveChangesAsync();

        return Ok("Đăng ký thành công.");
    }

    // Endpoint đăng nhập người dùng
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        if (model == null)
        {
            return BadRequest("Dữ liệu đăng nhập không hợp lệ.");
        }

        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
        if (user == null || !VerifyPassword(model.Password, user.Password))
        {
            return Unauthorized("Thông tin đăng nhập không chính xác.");
        }

        // Tạo JWT Token
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes("YourSuperSecretKey1234567890!@#$%^&*"); // Thay thế bằng khóa bí mật bảo mật
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role), // Thêm vai trò vào token
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new
        {
            message = "Đăng nhập thành công.",
            token = tokenString,
            role = user.Role
        });
    }

    // Hàm mã hóa mật khẩu
    private string HashPassword(string password)
    {
        var passwordHasher = new PasswordHasher<User>();
        var user = new User(); // Tạo đối tượng User
        return passwordHasher.HashPassword(user, password);
    }

    // Hàm xác minh mật khẩu
    private bool VerifyPassword(string inputPassword, string hashedPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, inputPassword);
        return result == PasswordVerificationResult.Success;
    }
}

// Các model cho đăng ký và đăng nhập
public class RegisterModel
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } // Vai trò (tuỳ chọn)
    public string UserImage { get; set; } // Đường dẫn ảnh
    public string PhoneNumber { get; set; } // Số điện thoại
    public int Age { get; set; } // Tuổi
    public string Gender { get; set; } // Giới tính
}

public class LoginModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}
