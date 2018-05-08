using Lombiq.Privacy.Models;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Mvc.Filters;
using System.Web.Mvc;

namespace Lombiq.Privacy.Filters
{
    public class ConsentBannerInjectionFilter : FilterProvider, IResultFilter
    {
        private readonly IOrchardServices _orchardServices;


        public ConsentBannerInjectionFilter(
            IOrchardServices orchardServices)
        {
            _orchardServices = orchardServices;
        }


        public void OnResultExecuted(ResultExecutedContext filterContext) { }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (Orchard.UI.Admin.AdminFilter.IsApplied(filterContext.RequestContext)) return;

            if (filterContext.Result is PartialViewResult) return;

            var workContext = _orchardServices.WorkContext;

            if (!workContext.CurrentSite.As<PrivacySettingsPart>().EnableConsentBanner) return;

            if (workContext.CurrentUser == null)
            {
                workContext.Layout.Content.Add(_orchardServices.New.Lombiq_Privacy_ConsentBanner(), "after");
            }
        }
    }
}