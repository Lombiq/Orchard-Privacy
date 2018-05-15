using Orchard;

namespace Lombiq.Privacy.Services
{
    /// <summary>
    /// Managing consent-related cookies.
    /// </summary>
    public interface ICookieService : IDependency
    {
        /// <summary>
        /// Check if the user has consent.
        /// </summary>
        /// <returns>True if the user accepts the privacy policy.</returns>
        bool UserHasConsent();

        /// <summary>
        /// Creating the consent cookie for the current user.
        /// </summary>
        void SetConsentCookie();
    }
}
