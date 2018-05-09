using Orchard;

namespace Lombiq.Privacy.Services
{
    public interface ICookieService : IDependency
    {
        bool UserHasConsent();
        void SetConsentCookie();
    }
}
