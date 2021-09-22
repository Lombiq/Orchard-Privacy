using Lombiq.Privacy.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Entities;
using OrchardCore.Users;
using OrchardCore.Users.Models;
using OrchardCore.Users.Services;
using System.Threading.Tasks;

namespace Lombiq.Privacy.Services
{
    public class ConsentService : IConsentService
    {
        private readonly UserManager<IUser> _userManager;

        private readonly IOptions<CookiePolicyOptions> _cookiePolicyOptions;

        public ConsentService(UserManager<IUser> userManager, IOptions<CookiePolicyOptions> cookiePolicyOptions)
        {
            _userManager = userManager;
            _cookiePolicyOptions = cookiePolicyOptions;
        }

        public async Task<bool> IsConsentBannerNeededAsync(HttpContext httpContext)
        {
            var consentFeature = httpContext.Features.Get<ITrackingConsentFeature>();
            var consentCookie = httpContext.Request.Cookies[_cookiePolicyOptions.Value.ConsentCookie.Name];

            if (!consentFeature.IsConsentNeeded) return false;

            if (httpContext.User.Identity.IsAuthenticated)
            {
                var userService = httpContext.RequestServices.GetService<IUserService>();
                var orchardUser = (User)await userService.GetAuthenticatedUserAsync(httpContext.User);
                return !IsUserAcceptedConsent(orchardUser);
            }
            else
            {
                return consentCookie == null;
            }
        }

        public async Task<bool> IsConsentNeededAsync(HttpContext httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                var userService = httpContext.RequestServices.GetService<IUserService>();
                var orchardUser = (User)await userService.GetAuthenticatedUserAsync(httpContext.User);
                return !IsUserAcceptedConsent(orchardUser);
            }

            return true;
        }

        public bool IsUserAcceptedConsent(User user) => user.Has<PrivacyConsent>() && user.As<PrivacyConsent>().Accepted;

        public Task StoreUserConsentAsync(User user)
        {
            user.Put(new PrivacyConsent { Accepted = true });
            return _userManager.UpdateAsync(user);
        }
    }
}
