using Lombiq.Privacy.Models;
using Lombiq.Privacy.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Mvc.Filters;
using System.Web.Mvc;
using static Lombiq.Privacy.Constants.FeatureNames;

namespace Lombiq.Privacy.Filters
{
    [OrchardFeature(Lombiq_Privacy_Consent_Banner)]
    public class ConsentBannerInjectionFilter : FilterProvider, IResultFilter
    {
        private readonly ICookieService _cookieService;
        private readonly IOrchardServices _orchardServices;

        public ConsentBannerInjectionFilter(
            ICookieService cookieService,
            IOrchardServices orchardServices)
        {
            _cookieService = cookieService;
            _orchardServices = orchardServices;
        }


        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (Orchard.UI.Admin.AdminFilter.IsApplied(filterContext.RequestContext)) return;

            if (filterContext.Result is PartialViewResult) return;

            var workContext = _orchardServices.WorkContext;

            if (!workContext.CurrentSite.As<ConsentBannerSettingsPart>().EnableConsentBanner) return;

            if (!_cookieService.UserHasConsent())
                workContext.Layout.Content.Add(_orchardServices.New.Lombiq_Privacy_ConsentBanner(), "after");
        }

        public void OnResultExecuted(ResultExecutedContext filterContext) { }
    }
}