using Orchard;
using System.Web;
using static Lombiq.Privacy.Constants.CookieNames;

namespace Lombiq.Privacy.Services
{
    public class ConsentService : IConsentService
    {
        private readonly IWorkContextAccessor _wca;


        public ConsentService(IWorkContextAccessor wca)
        {
            _wca = wca;
        }


        public bool UserHasConsent()
        {
            var context = _wca.GetContext();

            return context.HttpContext.Request.Cookies[HasConsent]?.Value.ToLowerInvariant() == "true" || context.CurrentUser != null;
        }

        public void SetConsentCookie()
        {
            _wca.GetContext().HttpContext.Response.Cookies.Add(
                new HttpCookie(HasConsent)
                {
                    Value = HasConsent
                });
        }
    }
}