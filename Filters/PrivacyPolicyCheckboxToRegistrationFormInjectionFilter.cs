using Lombiq.Privacy.Models;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Layouts.Services;
using Orchard.Mvc.Filters;
using System.Web.Mvc;
using static Lombiq.Privacy.Constants.FeatureNames;

namespace Lombiq.Privacy.Filters
{
    [OrchardFeature(RegistrationConsent)]
    public class PrivacyPolicyCheckboxToRegistrationFormInjectionFilter : FilterProvider, IResultFilter
    {
        private readonly IOrchardServices _orchardServices;
        private readonly ICurrentControllerAccessor _currentControllerAccessor;


        public PrivacyPolicyCheckboxToRegistrationFormInjectionFilter(
            IOrchardServices orchardServices,
            ICurrentControllerAccessor currentControllerAccessor)
        {
            _orchardServices = orchardServices;
            _currentControllerAccessor = currentControllerAccessor;
        }


        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (Orchard.UI.Admin.AdminFilter.IsApplied(filterContext.RequestContext)) return;

            if (filterContext.Result is PartialViewResult) return;

            var workContext = _orchardServices.WorkContext;

            if (!workContext.CurrentSite.As<RegistrationConsentSettingsPart>().EnablePrivacyCheckboxOnRegistrationPage) return;

            if (_currentControllerAccessor.CurrentController == null) return;

            if (workContext.HttpContext.Request.Path ==
                $"/{nameof(Orchard.Users)}/Account/{nameof(Orchard.Users.Controllers.AccountController.Register)}")
            {
                workContext.Layout.Content.Add(_orchardServices.New.Lombiq_Privacy_RegistrationCheckbox(), "before");
            }
        }

        public void OnResultExecuted(ResultExecutedContext filterContext) { }
    }
}