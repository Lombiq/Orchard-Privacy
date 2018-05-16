using Lombiq.Privacy.Services;
using Orchard;
using Orchard.Environment.Extensions;
using Orchard.Mvc.Filters;
using System.Web.Mvc;
using static Lombiq.Privacy.Constants.FeatureNames;

namespace Lombiq.Privacy.Filters
{
    [OrchardFeature(ConsentBanner)]
    public class ConsentBannerInjectionFilter : FilterProvider, IResultFilter
    {
        private readonly IConsentService _consentService;
        private readonly IOrchardServices _orchardServices;

        public ConsentBannerInjectionFilter(
            IConsentService consentService,
            IOrchardServices orchardServices)
        {
            _consentService = consentService;
            _orchardServices = orchardServices;
        }


        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (Orchard.UI.Admin.AdminFilter.IsApplied(filterContext.RequestContext)) return;

            if (filterContext.Result is PartialViewResult) return;

            var workContext = _orchardServices.WorkContext;

            if (!_consentService.UserHasConsent())
                workContext.Layout.Content.Add(_orchardServices.New.Lombiq_Privacy_ConsentBanner(), "after");
        }

        public void OnResultExecuted(ResultExecutedContext filterContext) { }
    }
}