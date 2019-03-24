using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SMLYS.ApplicationCore.DTOs.User;
using SMLYS.ApplicationCore.Interfaces.Services.Users;
using SMLYS.Infrastructure.Identity;
using SMLYS.Web.ViewModels.SiteUsers;

namespace SMLYS.Web.Controllers
{
    [Authorize]
    [Route("SiteUser")]
    public class SiteUserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;

        public SiteUserController(
         UserManager<ApplicationUser> userManager,
         SignInManager<ApplicationUser> signInManager,
         IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }

        [Route("{view=Index}")]
        public IActionResult Index(int id, string view)
        {
            if (view == "SiteUserForm")
            {
                if (id == 0)
                {
                    ViewData["Title"] = $"New User";
                    ViewData["FormType"] = $"New User";
                }
                else
                {
                    ViewData["Title"] = $"Edit User";
                    ViewData["FormType"] = $"Edit User";

                   // var data = _patientApiService.SearchPatient(id);
                   
                }

                return View(view);
            }
            else if (view == "Index")
            {
                ViewData["Title"] = $"Search Users";
                var data = _userService.SearchSiteUsers();
                var viewData = data.Select(p => new SiteUserViewModel
                {
                    Email = p.Email,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    SiteUserLevelName = p.SiteUserLevelName
                });
                return View(view, viewData);
            }

            return View(view);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(SiteUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var siteUserModel = new SiteUserModel {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    IsDoctor = true,
                    LastName = model.LastName,
                    Password =model.Password,
                    SiteUserId = model.SiteUserId,
                    UserId = user.Id,
                    SiteUserLevelId = 1
                    };

                    var registerUserResult = _userService.RegisterUser(siteUserModel);
                    if (registerUserResult.Success)
                    {
                        return View(model);
                    }
                    else
                    {
                        await _userManager.DeleteAsync(user);
                        ModelState.AddModelError(siteUserModel.Email, "Create user Failed. ");
                    }
                }
                AddErrors(result);
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}