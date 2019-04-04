using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SMLYS.ApplicationCore.Interfaces.Base;

namespace SMLYS.Web.Areas.Identity.Pages.Account.Manage
{
    public class VerifyPhoneNumberModel : PageModel
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _authMessageSender;

        public VerifyPhoneNumberModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEmailSender emailSender,
           ISmsSender authMessageSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _authMessageSender = authMessageSender;
        }


        [BindProperty]
        public string Code { get; set; }

        [BindProperty]
        public string PhoneNumber { get; set; }

        public async Task<IActionResult> OnGetAsync(string phoneNumber)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            PhoneNumber = phoneNumber;

           // var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, PhoneNumber);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string PhoneNumber, string Code)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var result = await _userManager.ChangePhoneNumberAsync(user, PhoneNumber, Code);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToPage("/Identity/Account/Manage");
                    //return RedirectToAction(nameof(IndexModel), new { Message = "AddPhoneSuccess" });
                }
            }
            // If we got this far, something failed, redisplay the form
            ModelState.AddModelError(string.Empty, "Failed to verify phone number");
            return Page();
        }
    }
}