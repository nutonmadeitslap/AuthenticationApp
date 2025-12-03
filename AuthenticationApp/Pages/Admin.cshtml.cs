using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthenticationApp.Pages
{
    [Authorize(Roles = "Admin")]
    public class AdminModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AdminModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public List<UserRolesViewModel> Users { get; set; }

        [BindProperty]
        public string SelectedUserId { get; set; }

        [BindProperty]
        public string SelectedRole { get; set; }

        public async Task OnGetAsync()
        {
            Users = new List<UserRolesViewModel>();
            foreach (var user in _userManager.Users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                Users.Add(new UserRolesViewModel
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Roles = roles.ToList()
                });
            }
        }
        public async Task<IActionResult> OnPostChangeRoleAsync()
        {
            if (string.IsNullOrEmpty(SelectedUserId) || string.IsNullOrEmpty(SelectedRole))
                return RedirectToPage();

            var user = await _userManager.FindByIdAsync(SelectedUserId);
            if (user != null)
            {
                var currentRoles = await _userManager.GetRolesAsync(user);

                foreach (var role in currentRoles)
                {
                    await _userManager.RemoveFromRoleAsync(user, role);
                }

                await _userManager.AddToRoleAsync(user, SelectedRole);
            }

            return RedirectToPage();
        }

        public class UserRolesViewModel
        {
            public string UserId { get; set; }
            public string Email { get; set; }
            public List<string> Roles { get; set; }
        }
    }
}
