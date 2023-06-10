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

public class RegistrationFormEventHandler : IRegistrationFormEvents
{
    private readonly IHttpContextAccessor _hca;
    private readonly IStringLocalizer T;
    private readonly IPrivacyConsentService _consentService;

    public RegistrationFormEventHandler(
        IHttpContextAccessor hca,
        IStringLocalizer<RegistrationFormEventHandler> stringLocalizer,
        IPrivacyConsentService consentService)
    {
        _hca = hca;
        T = stringLocalizer;
        _consentService = consentService;
    }

    public Task RegisteredAsync(IUser user) => _consentService.StoreUserConsentAsync(user);

    public Task RegistrationValidationAsync(Action<string, string> reportError)
    {
        var registrationCheckbox = _hca.HttpContext?.Request?.Form?[nameof(PrivacyRegistrationConsentCheckboxViewModel.RegistrationCheckbox)]
            .Select(bool.Parse);

        if (registrationCheckbox == null || !registrationCheckbox.Contains(value: true))
        {
            reportError(
                nameof(PrivacyRegistrationConsentCheckboxViewModel.RegistrationCheckbox),
                T["You have to accept the privacy policy."]);
        }

        return Task.CompletedTask;
    }
}
