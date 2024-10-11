using ASI.Basecode.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ASI.Basecode.Data.Models;
using System.IO;
using System.Linq;
using System;
using Microsoft.AspNetCore.Identity;

namespace ASI.Basecode.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly PasswordHasher<Admin> _passwordHasher;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
            _passwordHasher = new PasswordHasher<Admin>();
        }

        public IActionResult Index(string search)
        {
            (bool result, IEnumerable<Admin> admins) = _adminService.GetAdmins();

            if (!result)
            {
                return View(null);
            }


            if (!string.IsNullOrEmpty(search))
            {
                admins = admins.Where(a => a.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                          a.Email.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            return View(admins.ToList());
        }

        public IActionResult CreateAdmin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAdmin(Admin admin)
        {
            try
            {
                _adminService.AddAdmin(admin);
                return RedirectToAction("Index");
            }
            catch (InvalidDataException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(admin);
            }
        }

        public IActionResult Delete(int AdminId)
        {
            (bool result, IEnumerable<Admin> admins) = _adminService.GetAdmins();
            var admin = admins.FirstOrDefault(a => AdminId== a.AdminId);
            if (admin != null)
            {
                _adminService.DeleteAdmin(admin);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int adminId)
        {
            (bool result, IEnumerable<Admin> admins) = _adminService.GetAdmins();
            var admin = admins.FirstOrDefault(a => adminId == a.AdminId);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        [HttpPost]
        public IActionResult Update(Admin model)
        {
            if (ModelState.IsValid)
            {
               
                (bool result, IEnumerable<Admin> admins) = _adminService.GetAdmins();
                var existingAdmin = admins.FirstOrDefault(a => model.AdminId == a.AdminId);

                if (existingAdmin != null)
                {
                    
                    existingAdmin.Name = model.Name;
                    existingAdmin.Email = model.Email;

                   
                    if (!string.IsNullOrWhiteSpace(model.Password))
                    {
                        existingAdmin.Password = _passwordHasher.HashPassword(existingAdmin, model.Password);
                    }

                    
                    _adminService.UpdateAdmin(existingAdmin);
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }

          
            return View(model);
        }

    }
}
