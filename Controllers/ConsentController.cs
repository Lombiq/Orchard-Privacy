using Lombiq.Privacy.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lombiq.Privacy.Controllers
{
    public class ConsentController : Controller
    {
        private readonly IConsentService _consentService;

        public ConsentController(IConsentService consentService) => _consentService = consentService;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcceptanceOfConsent()
        {
            if (!await _consentService.IsUserAcceptedConsentAsync(ControllerContext.HttpContext))
            {
                await _consentService.StoreUserConsentAsync(User);
            }

            return Ok();
        }
    }
}
