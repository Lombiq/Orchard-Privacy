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

public class RegistrationFormEventHandler(
    IHttpContextAccessor hca,
    IStringLocalizer<RegistrationFormEventHandler> stringLocalizer,
    IPrivacyConsentService consentService) : IRegistrationFormEvents
{
    private readonly IStringLocalizer T = stringLocalizer;

    public Task RegisteredAsync(IUser user) => consentService.StoreUserConsentAsync(user);

    public Task RegistrationValidationAsync(Action<string, string> reportError)
    {
        var registrationCheckbox = hca.HttpContext?.Request?.Form?[nameof(PrivacyRegistrationConsentCheckboxViewModel.RegistrationCheckbox)]
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
