using Lombiq.Privacy.Services;
using Lombiq.Privacy.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using OrchardCore.Users;
using OrchardCore.Users.Events;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Lombiq.Privacy.Handlers;

public class ExternalRegistrationFormEventHandler : RegistrationFormEventsBase
{
    private readonly IHttpContextAccessor _hca;
    private readonly IStringLocalizer T;
    private readonly IPrivacyConsentService _consentService;

    public ExternalRegistrationFormEventHandler(
        IHttpContextAccessor hca,
        IStringLocalizer<ExternalRegistrationFormEventHandler> stringLocalizer,
        IPrivacyConsentService consentService)
    {
        _hca = hca;
        T = stringLocalizer;
        _consentService = consentService;
    }

    public override Task RegisteredAsync(IUser user) => _consentService.StoreUserConsentAsync(user);

    public override Task RegistrationValidationAsync(Action<string, string> reportError)
    {
        var registrationCheckbox = _hca.HttpContext?
            .Request
            .Form[nameof(PrivacyRegistrationConsentCheckboxViewModel.RegistrationCheckbox)]
            .Select(bool.Parse)
            .ToList();

        if (registrationCheckbox == null ||
            (registrationCheckbox.Count > 0 && !registrationCheckbox.Contains(value: true)))
        {
            reportError(
                nameof(PrivacyRegistrationConsentCheckboxViewModel.RegistrationCheckbox),
                T["You have to accept the privacy policy."]);
        }

        return Task.CompletedTask;
    }
}
