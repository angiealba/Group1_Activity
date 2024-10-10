using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using Basecode.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public IQueryable<User> GetUsers() => GetDbSet<User>();

        public bool UserExists(string userId) => GetDbSet<User>().Any(x => x.UserId == userId);

        public void AddUser(User user)
        {
            GetDbSet<User>().Add(user);
            UnitOfWork.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            var existingUser = GetDbSet<User>().FirstOrDefault(u => u.UserId == user.UserId);

            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                if (!string.IsNullOrEmpty(user.Password))
                {
                    existingUser.Password = user.Password;
                }

                GetDbSet<User>().Update(existingUser);
                UnitOfWork.SaveChanges();
            }
        }

        public void DeleteUser(User user)
        {
            GetDbSet<User>().Remove(user);  
            UnitOfWork.SaveChanges();
        }
    }
}
