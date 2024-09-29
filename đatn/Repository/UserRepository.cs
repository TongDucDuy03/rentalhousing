using System.Threading.Tasks;
using đatn.Models;
using đatn.Repository;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly RentalHousingDbContext _context;

    public UserRepository(RentalHousingDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserAsync(int userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}
