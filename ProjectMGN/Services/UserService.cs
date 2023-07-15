using AutoMapper;
using ProjectMGN.DTOS.Request;
using ProjectMGN.DTOS.Response;
using ProjectMGN.Interfaces;
using ProjectMGN.Interfaces.Services;
using ProjectMGN.Models;
using ProjectMGN.Repository;

namespace ProjectMGN.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IToken _tokenService;

        public UserService(UserRepository userRepository, IMapper mapper, IToken tokenService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;

        }

        public void RegisterUser(User request)
        {
            var user = _mapper.Map<User>(request);
            _userRepository.RegisterUser(user);
        }
        public LoginResponse LoginService(LoginRequest request)
        {
            User user = _userRepository.Login(request);
            string token = _tokenService.GenerateToken(request);

            LoginResponse loggedUser = new()
            {
                Email = user.Email,
                Token = token,
                UserName = user.UserName
            };
            Console.WriteLine(loggedUser.Token);

            //return loggedUser;
            return loggedUser;
        }
    }
}
