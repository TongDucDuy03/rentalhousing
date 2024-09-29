using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using đatn.Models;
using Microsoft.IdentityModel.Tokens;
using đatn.Service;

public class UserService : IUserService
{
    private readonly RentalHousingDbContext _context;
    private readonly PasswordHasher<User> _passwordHasher;

    public UserService(RentalHousingDbContext context)
    {
        _context = context;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task RegisterUserAsync(RegisterModel model)
    {
        var user = new User
        {
            Username = model.Username,
            Email = model.Email,
            Role = model.Role ?? "tenant",
            Password = HashPassword(model.Password),
            user_image = model.UserImage,
            phone_number = model.PhoneNumber,
            Age = model.Age,
            Gender = model.Gender
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<string> LoginAsync(LoginModel model)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
        if (user == null || !VerifyPassword(model.Password, user.Password))
        {
            return null;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes("YourSuperSecretKey1234567890!@#$%^&*"); // Thay thế bằng khóa bí mật bảo mật
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<User> GetUserAsync(int userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task UpdateUserAsync(int userId, User user)
    {
        var existingUser = await _context.Users.FindAsync(userId);
        if (existingUser == null) return;

        existingUser.Username = user.Username;
        existingUser.Email = user.Email;
        existingUser.user_image = user.user_image;
        existingUser.phone_number = user.phone_number;
        existingUser.Age = user.Age;
        existingUser.Gender = user.Gender;

        _context.Users.Update(existingUser);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    // Hàm mã hóa mật khẩu
    private string HashPassword(string password)
    {
        var user = new User();
        return _passwordHasher.HashPassword(user, password);
    }

    // Hàm xác minh mật khẩu
    private bool VerifyPassword(string inputPassword, string hashedPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, inputPassword);
        return result == PasswordVerificationResult.Success;
    }
}
