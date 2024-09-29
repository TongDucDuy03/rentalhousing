using đatn.Models;

namespace đatn.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(int userId);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
    }

}
