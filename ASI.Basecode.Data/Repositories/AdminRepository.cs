using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using Basecode.Data.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Repositories
{
    public class AdminRepository : BaseRepository, IAdminRepository
    {
        List<Admin> _allAdmin = new();
        private readonly AsiBasecodeDBContext _dbContext;

        public AdminRepository(AsiBasecodeDBContext dbContext, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Admin> ViewAdmins()
        {
            return _dbContext.Admins.ToList();
        }

        public void AddAdmin(Admin admin)
        {
            try
            {
                var newAdmin= new Admin();
                newAdmin.Name= admin.Name;
                newAdmin.Email = admin.Email;
                newAdmin.Role= admin.Role;
                newAdmin.Password = admin.Password;
                _dbContext.Admins.Add(newAdmin);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw new InvalidDataException("error adding admins");
            }
        }
        public void DeleteAdmin(Admin admin)
        {
            _dbContext.Admins.Remove(admin);
            _dbContext.SaveChanges();
        }

        public void UpdateAdmin(Admin admin)
        {
            var existingAdmin= _dbContext.Admins.FirstOrDefault(a => a.AdminId == admin.AdminId);
            if (existingAdmin != null)
            {
                existingAdmin.Name = admin.Name;
                existingAdmin.Email= admin.Email;
                existingAdmin.Role= admin.Role;
                existingAdmin.Password = admin.Password;

                _dbContext.Admins.Update(existingAdmin);
                _dbContext.SaveChanges();

            }
        }
        public (bool, IEnumerable<Admin>) GetAdmins()
        {
            throw new NotImplementedException();
        }
    }
}
