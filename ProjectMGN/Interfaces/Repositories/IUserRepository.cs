using ProjectMGN.DTOS.Request;
using ProjectMGN.DTOS.Response;
using ProjectMGN.Models;

namespace ProjectMGN.Interfaces.Repositories
{
    public interface IUserRepository
    {
        void RegisterUser(User user);
        void UnregisterUser(User user);
        User Login(LoginRequest request);
        int GetUserById(int id);
    }
}
