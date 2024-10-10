using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.Manager;
using ASI.Basecode.Services.ServiceModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ASI.Basecode.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index(string search)
        {
            var users = _userService.GetUsers();

            if (!string.IsNullOrEmpty(search))
            {
                users = users.Where(u => u.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                         u.Email.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            return View(users.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser(UserViewModel userViewModel)
        {
            if (_userService.UserExists(userViewModel.UserId))
            {
                ModelState.AddModelError("UserId", "A user with this User ID already exists.");
            }

            if (!ModelState.IsValid)
            {
                var users = _userService.GetUsers().ToList();
                ViewBag.UserViewModel = userViewModel;
                return View("Index", users);
            }

            try
            {
                _userService.AddUser(userViewModel);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while creating the user.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult EditUser(User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _userService.GetUsers().FirstOrDefault(u => u.Id == user.Id);
                if (existingUser != null)
                {
                    existingUser.Name = user.Name;
                    existingUser.Email = user.Email;

                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        existingUser.Password = PasswordManager.EncryptPassword(user.Password);
                    }

                    _userService.UpdateUser(existingUser);
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "User not found.");
            }

            return View(user);
        }

        [HttpPost]
        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id);  // Permanently delete the user
            return RedirectToAction("Index");
        }
    }
}
