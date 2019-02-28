using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using SMLYS.ApplicationCore.Entities.CustomerAggregate;
//using SMLYS.ApplicationCore.Interfaces;
//using SMLYS.ApplicationCore.Interfaces.Services;
//using SMLYS.ApplicationCore.Models.ViewModels.Customer;
using SMLYS.Backend.Models.AccountViewModels;
using SMLYS.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SMLYS.Backend.Controllers
{
    [Authorize]
    [Route("Board")]
    public class BoardController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _rolesManager;
        //private readonly ICustomerService _customerService;
        private readonly ILogger _logger;

        public BoardController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
           // ICustomerService customerService,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _rolesManager = roleManager;
           // _customerService = customerService;
            _logger = logger;
        }

        [Route("{view=Index}")]
        public IActionResult Index(string view)
        {
            ViewData["Title"] = "主面板";

            return View(view);
        }

        //[HttpPost]
        //public async Task<IActionResult> NewCustomer(RegisterCustomerViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        //        var result = await _userManager.CreateAsync(user, model.Password);
        //        if (result.Succeeded)
        //        {

        //            var utcDate = DateTime.UtcNow;

        //            var customer = new Customer()
        //            {
        //                Active = true,
        //                Address = model.Customer.Address,
        //                City = model.Customer.City,
        //                CountryId = 1,
        //                Email = model.Email,
        //                FirstName = model.Customer.FirstName,
        //                JoinDateUTC = utcDate,
        //                LastUpdatedUTC = utcDate,
        //                LastName = string.Empty,
        //                PhoneNumber = model.Customer.PhoneNumber,
        //                StateName = model.Customer.StateName,
        //                Zip = model.Customer.Zip,
        //                UserId = user.Id
        //            };


        //            var account = await _customerService.CreateCustomerAccountAsync(model.Customer.Company, customer);
        //            if (account != null)
        //            {
        //                var roleCheck = await _rolesManager.RoleExistsAsync("Customer");
        //                if (roleCheck)
        //                {
        //                    await _userManager.AddToRoleAsync(user, "Customer");
        //                }

        //                _logger.LogInformation("User created a new account with password.");
        //            }
        //            else
        //            {
        //                await _userManager.DeleteAsync(user);
        //                _logger.LogInformation("Customer created failed, user deleted. ");
        //            }

        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}
    }


}
