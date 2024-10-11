using ASI.Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Interfaces
{
    public interface IAdminRepository
    {
        IEnumerable<Admin> ViewAdmins();
        void AddAdmin(Admin admin);

        void DeleteAdmin(Admin admin);

        void UpdateAdmin(Admin admin);
    }
}
