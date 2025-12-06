using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthenticationApp.Pages
{
    [Authorize]
    public class UserOnlyModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
