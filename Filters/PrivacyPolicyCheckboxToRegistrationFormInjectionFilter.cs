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
            if (filterContext.Result is PartialViewResult || 
                Orchard.UI.Admin.AdminFilter.IsApplied(filterContext.RequestContext)) return;

            var routeDataValues = filterContext.HttpContext.Request.RequestContext.RouteData.Values;

            if (routeDataValues["area"]?.ToString() == $"{nameof(Orchard)}.{nameof(Orchard.Users)}" &&
                routeDataValues["controller"]?.ToString() == "Account" &&
                routeDataValues["action"]?.ToString() == nameof(Orchard.Users.Controllers.AccountController.Register))
            {
                _orchardServices.WorkContext.Layout.Content.Add(_orchardServices.New.Lombiq_Privacy_RegistrationCheckbox(), "before");
            }
        }

        public void OnResultExecuted(ResultExecutedContext filterContext) { }
    }
}