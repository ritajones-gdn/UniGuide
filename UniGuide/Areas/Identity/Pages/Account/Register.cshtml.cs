using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniGuide.Data;

namespace UniGuide.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public string FullName { get; set; } = string.Empty;

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        [BindProperty]
        public string Role { get; set; } = string.Empty;

        [BindProperty]
        public string Major { get; set; } = string.Empty;

        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(FullName) || string.IsNullOrEmpty(Email) ||
                string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Role))
            {
                ErrorMessage = "Please fill in all required fields.";
                return Page();
            }

            // Create roles if they don't exist
            if (!await _roleManager.RoleExistsAsync("HighSchool"))
                await _roleManager.CreateAsync(new IdentityRole("HighSchool"));

            if (!await _roleManager.RoleExistsAsync("College"))
                await _roleManager.CreateAsync(new IdentityRole("College"));

            var user = new ApplicationUser
            {
                UserName = Email,
                Email = Email,
                FullName = FullName,
                Role = Role,
                Major = Role == "College" ? Major : string.Empty
            };

            var result = await _userManager.CreateAsync(user, Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Role);
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToPage("/Index");
            }

            ErrorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
            return Page();
        }
    }
}
