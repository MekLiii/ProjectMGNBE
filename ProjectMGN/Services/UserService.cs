using AutoMapper;
using ProjectMGN.DTOS.Request;
using ProjectMGN.DTOS.Response;
using ProjectMGN.Interfaces;
using ProjectMGN.Interfaces.Repositories;
using ProjectMGN.Interfaces.Services;
using ProjectMGN.Models;
using ProjectMGN.Repository;

namespace ProjectMGN.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IToken _tokenService;

        public UserService(IUserRepository userRepository, IToken tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;

        }

        public void RegisterUser(User request)
        {
            _userRepository.RegisterUser(request);
        }
        public LoginResponse LoginService(LoginRequest request)
        {
            if(request.Email == null)
            {
                throw new Exception("Email is empty");
            }
            if(request.Password == null)
            {
                throw new Exception("Password is empty");
            }
            User user = _userRepository.Login(request);
            string token = _tokenService.GenerateToken(user);

            LoginResponse loggedUser = new()
            {
                Token = token,
            };

            return loggedUser;
        }
        public int GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }
    }
}
