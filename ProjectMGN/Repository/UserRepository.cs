using ProjectMGN.Db;
using ProjectMGN.DTOS.Request;
using ProjectMGN.Interfaces.Repositories;
using ProjectMGN.Models;

namespace ProjectMGN.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ProjectMGNDB _dbContext;

        public UserRepository(ProjectMGNDB dbContext)
        {
            _dbContext = dbContext;
        }
        private bool ChechIfEmailIsValid(User user)
        {
            return true;
        }
        private bool CheckIfUserAlreadyExists(User user)
        {
            try
            {
                if (user == null)
                {
                    return false;
                }
                User existingUser = _dbContext.Users.FirstOrDefault(userFromDb => userFromDb.UserName == user.UserName || userFromDb.Email == user.Email);
                bool userExists = (existingUser != null);
                return userExists;
                return false;
            }
            catch
            {
                return false;
            }
        }
        public void RegisterUser(User user)
        {
            if (CheckIfUserAlreadyExists(user))
            {
                throw new InvalidOperationException("User already exists");
            }
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return;
        }
        public User Login(LoginRequest request)
        {
            User user = _dbContext.Users.FirstOrDefault(user => user.Email == request.Email);
            if (user == null)
            {
                throw new InvalidOperationException("User does not exist");
            }
            if (user.Password != request.Password)
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
            User user = _dbContext.Users.FirstOrDefault(user => user.Id == id);
            if(user == null)
            {
                throw new InvalidOperationException("User not found");
            }
            return user.Id;
        }
    }
}
