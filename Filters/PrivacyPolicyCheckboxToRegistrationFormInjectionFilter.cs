using Orchard;
using Orchard.Environment.Extensions;
using Orchard.Mvc.Filters;
using System.Web.Mvc;
using static Lombiq.Privacy.Constants.FeatureNames;

namespace Lombiq.Privacy.Filters
{
    [OrchardFeature(RegistrationConsent)]
    public class PrivacyPolicyCheckboxToRegistrationFormInjectionFilter : FilterProvider, IResultFilter
    {
        private readonly IOrchardServices _orchardServices;


        public PrivacyPolicyCheckboxToRegistrationFormInjectionFilter(IOrchardServices orchardServices)
        {
            _orchardServices = orchardServices;
        }


        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (Orchard.UI.Admin.AdminFilter.IsApplied(filterContext.RequestContext)) return;

            if (filterContext.Result is PartialViewResult) return;

            var workContext = _orchardServices.WorkContext;

            var routeDataValues = workContext.HttpContext.Request.RequestContext.RouteData.Values;

            if (routeDataValues["area"]?.ToString() == $"{nameof(Orchard)}.{nameof(Orchard.Users)}" &&
                routeDataValues["controller"]?.ToString() == "Account" &&
                routeDataValues["action"]?.ToString() == nameof(Orchard.Users.Controllers.AccountController.Register))
            {
                workContext.Layout.Content.Add(_orchardServices.New.Lombiq_Privacy_RegistrationCheckbox(), "before");
            }
        }

        public void OnResultExecuted(ResultExecutedContext filterContext) { }
    }
}