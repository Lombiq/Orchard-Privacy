using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using OrchardCore.Users;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lombiq.Privacy.Services
{
    /// <summary>
    /// Service to manage user privacy consent.
    /// </summary>
    public interface IPrivacyConsentService
    {
        /// <summary>
        /// Decides whether the consent banner should be displayed based on the configuration of the
        /// <see cref="ITrackingConsentFeature"/> and whether the user has already accepted the privacy statement.
        /// </summary>
        /// <param name="httpContext">The current HTTP context.</param>
        /// <returns>
        /// <see langword="true"/> if it needs to display the banner, <see langword="false"/> otherwise.
        /// </returns>
        Task<bool> IsConsentBannerNeededAsync(HttpContext httpContext);

        /// <summary>
        /// Decides whether in the current HTTP context it is required to accept this privacy statement.
        /// </summary>
        /// <param name="httpContext">The current HTTP context.</param>
        /// <returns>
        /// <see langword="true"/> if the consent must be accepted in the current context, <see langword="false"/>
        /// otherwise.
        /// </returns>
        Task<bool> IsConsentNeededAsync(HttpContext httpContext);

        /// <summary>
        /// Decides whether the user already accepts the privacy policy in the given context.
        /// </summary>
        /// <param name="httpContext">The current HTTP context.</param>
        /// <returns>
        /// <see langword="true"/> if the user already accepted the privacy consent, <see langword="false"/> otherwise.
        /// </returns>
        Task<bool> IsUserAcceptedConsentAsync(HttpContext httpContext);

        /// <summary>
        /// Stores the user's acceptance of a privacy statement.
        /// </summary>
        /// <param name="user">The current user.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task StoreUserConsentAsync(ClaimsPrincipal user);

        /// <summary>
        /// Stores the user's acceptance of a privacy statement.
        /// </summary>
        /// <param name="user">The current user.</param>
        /// <returns>A task that represents the asynchronous
        /// operation.</returns>
        Task StoreUserConsentAsync(IUser user);
    }
}
