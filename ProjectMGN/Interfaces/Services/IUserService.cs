using ProjectMGN.DTOS.Request;
using ProjectMGN.DTOS.Response;
using ProjectMGN.Models;


namespace ProjectMGN.Interfaces.Services
{
    public interface IUserService
    {
        public void RegisterUser(RegisterUserRequest request);
        public LoginResponse LoginService(LoginRequest request);
        public int GetUserById(int id); 
    }
}
