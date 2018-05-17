using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Mvc.Filters;
using System.Web.Mvc;
using static Lombiq.Privacy.Constants.FeatureNames;

namespace Lombiq.Privacy.Filters
{
    [OrchardFeature(RegistrationConsent)]
    public class PrivacyPolicyCheckboxToRegistrationFormValidationFilter : FilterProvider, IActionFilter
    {
        public Localizer T { get; set; }


        public PrivacyPolicyCheckboxToRegistrationFormValidationFilter()
        {
            T = NullLocalizer.Instance;
        }


        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var routeDataValues = filterContext.HttpContext.Request.RequestContext.RouteData.Values;

            if (routeDataValues["area"]?.ToString() == $"{nameof(Orchard)}.{nameof(Orchard.Users)}" &&
                routeDataValues["controller"]?.ToString() == "Account" &&
                routeDataValues["action"]?.ToString() == nameof(Orchard.Users.Controllers.AccountController.Register) &&
                filterContext.HttpContext.Request.HttpMethod.ToString().Equals("POST") &&
                !filterContext.HttpContext.Request.Form["RegistrationCheckbox"].ToLowerInvariant().Contains("true"))
            {
                filterContext.Controller.ViewData.ModelState.AddModelError("RegistrationCheckbox", T("You have to accept the privacy policy.").Text);
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext) {}
    }
}