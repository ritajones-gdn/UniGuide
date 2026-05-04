using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniGuide.Data;

namespace UniGuide.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        [BindProperty]
        public bool RememberMe { get; set; }

        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Please enter your email and password.";
                return Page();
            }

            var result = await _signInManager.PasswordSignInAsync(
                Email, Password, RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToPage("/Index");
            }

            ErrorMessage = "Invalid email or password.";
            return Page();
        }
    }
}