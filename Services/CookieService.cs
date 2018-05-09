using Orchard;
using System.Web;
using static Lombiq.Privacy.Constants.CookieNames;

namespace Lombiq.Privacy.Services
{
    public class CookieService : ICookieService
    {
        private readonly IWorkContextAccessor _wca;


        public CookieService(IWorkContextAccessor wca)
        {
            _wca = wca;
        }


        public bool UserHasConsent() =>
            UserHasConsentCookie() || UserLoggedIn();

        public void SetConsentCookie()
        {
            _wca.GetContext().HttpContext.Response.Cookies.Add(
                new HttpCookie(HasConsent)
                {
                    Value = HasConsent
                });
        }


        private bool UserHasConsentCookie() => 
            _wca.GetContext().HttpContext.Request.Cookies[HasConsent]?.Value == "true";

        private bool UserLoggedIn() =>
            _wca.GetContext().CurrentUser != null;
    }
}