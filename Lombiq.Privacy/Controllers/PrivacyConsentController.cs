using Lombiq.Privacy.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lombiq.Privacy.Controllers;

public class PrivacyConsentController(IPrivacyConsentService consentService) : Controller
{
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AcceptanceOfConsent()
    {
        if (!await consentService.IsUserAcceptedConsentAsync(ControllerContext.HttpContext))
        {
            await consentService.StoreUserConsentAsync(User);
        }

        return Ok();
    }
}
