using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.Manager;
using ASI.Basecode.Services.ServiceModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static ASI.Basecode.Resources.Constants.Enums;

namespace ASI.Basecode.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public LoginResult AuthenticateUser(string userId, string password, ref User user)
        {
            user = _repository.GetUsers().FirstOrDefault(x => x.UserId == userId && x.Password == PasswordManager.EncryptPassword(password));
            return user != null ? LoginResult.Success : LoginResult.Failed;
        }

        public void AddUser(UserViewModel model)
        {
            if (!_repository.UserExists(model.UserId))
            {
                var user = _mapper.Map<User>(model);
                user.Password = PasswordManager.EncryptPassword(model.Password);
                user.CreatedTime = DateTime.Now;
                user.UpdatedTime = DateTime.Now;
                user.CreatedBy = Environment.UserName;
                user.UpdatedBy = Environment.UserName;

                _repository.AddUser(user);
            }
            else
            {
                throw new InvalidDataException("User already exists");
            }
        }

        public IEnumerable<User> GetUsers() => _repository.GetUsers().ToList();

        public void UpdateUser(User user)
        {
            var existingUser = _repository.GetUsers().FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                if (!string.IsNullOrEmpty(user.Password))
                {
                    existingUser.Password = PasswordManager.EncryptPassword(user.Password);
                }
                _repository.UpdateUser(existingUser);
            }
        }

        public void DeleteUser(int id)
        {
            var user = _repository.GetUsers().FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _repository.DeleteUser(user);  
            }
        }

        public bool UserExists(string userId) => _repository.UserExists(userId);
    }
}
