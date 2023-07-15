﻿using ProjectMGN.Db;
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
            if (user == null)s
            {
                return false;
            }
            User existingUser = _dbContext.Users.FirstOrDefault(userFromDb => userFromDb.UserName == user.UserName || userFromDb.Email == user.Email);
            bool userExists = (existingUser != null);
            return userExists;
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
            User user = _dbContext.Users.FirstOrDefault(user => user.UserName == request.UserName);
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
    }
}