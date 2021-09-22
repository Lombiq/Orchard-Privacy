using Microsoft.AspNetCore.Http;
using OrchardCore.Users.Models;
using System.Threading.Tasks;

namespace Lombiq.Privacy.Services
{
    /// <summary>
    /// Service to manage user privacy consent.
    /// </summary>
    public interface IConsentService
    {
        /// <summary>
        /// An asynchronous operation that decides for the given <see cref="HttpContext"/> whether to display the consent banner.
        /// </summary>
        /// <param name="httpContext">The current HttpContext.</param>
        /// <returns>
        /// <see langword="true"/> if it needs to display the banner, <see langword="false"/> otherwise.
        /// </returns>
        Task<bool> IsConsentBannerNeededAsync(HttpContext httpContext);

        /// <summary>
        /// An asynchronous operation that decides for the given <see cref="HttpContext"/> whether the user must have privacy consent.
        /// </summary>
        /// <param name="httpContext">The current HttpContext.</param>
        /// <returns><see langword="true"/> if the user must have privacy consent, <see langword="false"/> otherwise.</returns>
        Task<bool> IsConsentNeededAsync(HttpContext httpContext);

        /// <summary>
        /// Determines whether the given user already accepted the privacy consent.
        /// </summary>
        /// <param name="user">The current user.</param>
        /// <returns>
        /// <see langword="true"/> if the given user already accepted the privacy consent,
        /// <see langword="false"/> otherwise.
        /// </returns>
        bool IsUserAcceptedConsent(User user);

        /// <summary>
        /// An asynchronous operation that stores the user's acceptance of a privacy statement.
        /// </summary>
        /// <param name="user">The current user.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task StoreUserConsentAsync(User user);
    }
}
