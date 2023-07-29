using AutoMapper;
using ProjectMGN.DTOS.Request;
using ProjectMGN.DTOS.Response;
using ProjectMGN.Interfaces.Repositories;
using ProjectMGN.Interfaces.Services;
using ProjectMGN.Models;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using Aes = System.Security.Cryptography.Aes;

namespace ProjectMGN.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IToken _tokenService;
        private readonly IConfiguration _configuration;
        private readonly ISypherService _sypherService;

        public UserService(IUserRepository userRepository, IToken tokenService, IConfiguration configuration,ISypherService sypherService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _configuration = configuration;
            _sypherService = sypherService;
        }
        
        

        public void RegisterUser(User request)
        {
            User userWithEncryptedPassword = new()
            {
                Email = request.Email,
                UserName = request.UserName,
                Password = _sypherService.Encrypt(request.Password),
            };
            _userRepository.RegisterUser(userWithEncryptedPassword);
        }
        public LoginResponse LoginService(LoginRequest request)
        {
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
