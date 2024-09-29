using đatn.Models;

namespace đatn.Service
{
    public interface IUserService
    {
        Task RegisterUserAsync(RegisterModel model);
        Task<string> LoginAsync(LoginModel model);
        Task<User> GetUserAsync(int userId);
        Task UpdateUserAsync(int userId, User user);
        Task DeleteUserAsync(int userId);
    }

}
