using ProjectMGN.Db;
using ProjectMGN.DTOS.Request;
using ProjectMGN.Helpers;
using ProjectMGN.Interfaces.Repositories;
using ProjectMGN.Interfaces.Services;
using ProjectMGN.Models;

namespace ProjectMGN.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ProjectMGNDB _dbContext;
        private readonly ISypherService _sypherService;

        public UserRepository(ProjectMGNDB dbContext,ISypherService sypherService)
        {
            _dbContext = dbContext;
            _sypherService = sypherService;
        }
        private bool CheckIfUserAlreadyExists(User user)
        {
            try
            {
                if (user == null)
                {
                    return false;
                }
                var existingUser = _dbContext.Users.FirstOrDefault(userFromDb => userFromDb.UserName == user.UserName || userFromDb.Email == user.Email);
                var userExists = (existingUser != null);
                return userExists;
            }
            catch
            {
                return false;
            }
        }
        public void RegisterUser(User user)
        {
            if (user.UserName == null)
            {
                throw new ArgumentException("Invalid user name");
            }
            if(user.Email == null)
            {
                throw new ArgumentException("Email is not valid");
            }
            if(user.Password == null)
            {
                throw new ArgumentException("Password is not valid");
            }

            if (CheckIfUserAlreadyExists(user))
            {
                throw new InvalidOperationException("User already exists");
            }
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
        public User Login(LoginRequest request)
        {
            var user = _dbContext.Users.FirstOrDefault(user => user.Email == request.Email);
            if (user == null)
            {
                throw new Exception("User does not exist");
            }
            var encryptedPassword = _sypherService.Decrypt(user.Password);

            if (encryptedPassword != request.Password)
            {
                throw new InvalidOperationException("Password is incorrect");
            }
            return user;

        }
        public void UnregisterUser(User user)
        {
            throw new NotImplementedException();
        }
        public int GetUserById(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(user => user.Id == id);
            if(user == null)
            {
                throw new InvalidOperationException("User not found");
            }
            return user.Id;
        }
    }
}
