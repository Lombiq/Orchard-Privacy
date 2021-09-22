using Lombiq.Privacy.Services;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.Users.Models;
using OrchardCore.Users.Services;
using System.Threading.Tasks;

namespace Lombiq.Privacy.Controllers
{
    public class ConsentController : Controller
    {
        private readonly IUserService _userService;
        private readonly IConsentService _consentService;

        public ConsentController(IUserService userService, IConsentService consentService)
        {
            _userService = userService;
            _consentService = consentService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcceptanceOfConsent()
        {
            var user = (User)await _userService.GetAuthenticatedUserAsync(ControllerContext.HttpContext.User);
            if (user != null && !_consentService.IsUserAcceptedConsent(user))
            {
                await _consentService.StoreUserConsentAsync(user);
            }

            return Ok();
        }
    }
}
