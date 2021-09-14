using Lombiq.Privacy.Constants;
using Lombiq.Privacy.Models;
using Lombiq.Privacy.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Linq;
using OrchardCore.Users;
using OrchardCore.Users.Events;
using OrchardCore.Users.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Lombiq.Privacy.Handlers
{
    public class RegistrationFormEventHandler : IRegistrationFormEvents
    {
        private readonly IHttpContextAccessor _hca;
        private readonly IStringLocalizer T;
        private readonly UserManager<IUser> _userManager;

        public RegistrationFormEventHandler(
            IHttpContextAccessor hca,
            IStringLocalizer<RegistrationFormEventHandler> stringLocalizer,
            UserManager<IUser> userManager)
        {
            _hca = hca;
            _userManager = userManager;
            T = stringLocalizer;
        }

        public async Task RegisteredAsync(IUser user)
        {
            if (user is User orchardUser)
            {
                orchardUser.Properties[CustomProperties.Privacy] = JObject.FromObject(new PrivacyConsent { Accepted = true });
                await _userManager.UpdateAsync(orchardUser);
            }
        }

        public Task RegistrationValidationAsync(Action<string, string> reportError)
        {
            var registrationCheckbox = _hca.HttpContext?.Request?.Form?[nameof(RegistrationConsentCheckboxViewModel.RegistrationCheckbox)]
                .Select(value => bool.Parse(value));

            if (registrationCheckbox == null || !registrationCheckbox.Contains(true))
            {
                reportError(
                    nameof(RegistrationConsentCheckboxViewModel.RegistrationCheckbox),
                    T["You have to accept the privacy policy."]);
            }

            return Task.CompletedTask;
        }
    }
}
