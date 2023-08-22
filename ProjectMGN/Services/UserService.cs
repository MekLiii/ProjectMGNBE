using ProjectMGN.DTOS.Request;
using ProjectMGN.DTOS.Response;
using ProjectMGN.Interfaces.Repositories;
using ProjectMGN.Interfaces.Services;
using ProjectMGN.Models;

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
        
        

        public void RegisterUser(RegisterUserRequest request)
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
            var user = _userRepository.Login(request);
            var token = _tokenService.GenerateToken(user);

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
