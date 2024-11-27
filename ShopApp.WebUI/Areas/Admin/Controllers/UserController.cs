using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstratc;
using ShopApp.Business.Models.VMs;
using ShopApp.Business.Models.VMs.IdentityVms;
using ShopApp.DataAccess.Identity;
using ShopApp.WebUI.Extentions;

namespace ShopApp.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {

        private RoleManager<IdentityRole> _roleManager;
        private UserManager<User> _userManager;
        public UserController(
                                RoleManager<IdentityRole> roleManager,
                                UserManager<User> userManager
                                )
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult UserList()
        {
            return View(_userManager.Users);
        }


        public async Task<IActionResult> UserEdit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var selectedRoles = await _userManager.GetRolesAsync(user);
                var roles = _roleManager.Roles.Select(i => i.Name);
                ViewBag.Roles = roles;

                return View(new UserVm()
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    SelectedRoles = selectedRoles
                });
            }
            TempData.Put("message", new AlertMessage()
            {
                Title = "User not found",
                Message = "User not found",
                AlertType = "danger"
            });
            return Redirect("~/");
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(UserVm model, string[] selectedRoles)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.EmailConfirmed = model.EmailConfirmed;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    selectedRoles = selectedRoles ?? new string[] { };
                    result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles).ToArray());
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", "Selected roles cannot be added");
                        return View(model);
                    }
                    result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles).ToArray());
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", "Selected roles cannot be removed");
                        return View(model);
                    }
                    TempData.Put("message", new AlertMessage()
                    {
                        Title = "User updated successfully",
                        Message = "User updated successfully",
                        AlertType = "success"
                    });
                    return RedirectToAction("UserList");
                }
            }
            return View(model);
        }

    }
}
