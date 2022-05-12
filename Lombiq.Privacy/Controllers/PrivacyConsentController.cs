using Lombiq.Privacy.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lombiq.Privacy.Controllers;

public class PrivacyConsentController : Controller
{
    private readonly IPrivacyConsentService _consentService;

    public PrivacyConsentController(IPrivacyConsentService consentService) => _consentService = consentService;

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
