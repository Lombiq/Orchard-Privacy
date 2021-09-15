using Lombiq.Privacy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.Entities;
using OrchardCore.Users;
using OrchardCore.Users.Models;
using OrchardCore.Users.Services;
using System.Threading.Tasks;

namespace Lombiq.Privacy.Controllers
{
    public class ConsentController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<IUser> _userManager;

        public ConsentController(IUserService userService, UserManager<IUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // This action stores the user consent in case if the user existed before the Registration consent feature was
        // enabled.
        public async Task<IActionResult> AcceptanceOfConsent()
        {
            var user = (User)await _userService.GetAuthenticatedUserAsync(ControllerContext.HttpContext.User);
            if (user != null && !user.Has<PrivacyConsent>())
            {
                user.Put(new PrivacyConsent { Accepted = true });
                await _userManager.UpdateAsync(user);
            }

            return Ok();
        }
    }
}
