using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.Manager;
using System;
using ASI.Basecode.Services.ServiceModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        public AdminService(IAdminRepository adminRepository)
        {
            this._adminRepository = adminRepository;
        }

        public (bool, IEnumerable<Admin>) GetAdmins()
        {

            var admins = _adminRepository.ViewAdmins();
            if (admins != null)
            {
                return (true, admins);
            }
            return (false, null);
        }

        public void AddAdmin(Admin admin)
        {
            if (admin == null)
            {
                throw new ArgumentException();
            }
            var newAdmin = new Admin();

            newAdmin.Name = admin.Name;
            newAdmin.Email = admin.Email;
            newAdmin.Role = admin.Role;
            newAdmin.Password = PasswordManager.EncryptPassword(admin.Password);
            _adminRepository.AddAdmin(newAdmin);

        }
        public void DeleteAdmin(Admin admin)
        {
            if (admin == null)
            {
                throw new ArgumentException();
            }


            _adminRepository.DeleteAdmin(admin);
        }

        public void UpdateAdmin(Admin admin)
        {
            if (admin == null)
            {
                throw new ArgumentException();
            }


            _adminRepository.UpdateAdmin(admin);
        }
    }
}
