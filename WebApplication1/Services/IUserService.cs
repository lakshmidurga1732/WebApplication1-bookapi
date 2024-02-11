using WebApplication1.Entity;

namespace WebApplication1.Services
{
    public interface IUserService
    {
        void CreateUser(User user); 
        List<User> GetAllUsers(); 
        User GetUser(string userId);
        void EditUser(User user); 
        void DeleteUser(string userId); 
        User ValidateUser(string email, string password); 
        User GetUserById(int userId); 
    }
}
