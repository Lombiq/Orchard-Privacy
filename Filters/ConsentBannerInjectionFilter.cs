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
        private readonly IOrchardServices _orchardServices;


        public ConsentBannerInjectionFilter(IOrchardServices orchardServices)
        {
            _orchardServices = orchardServices;
        }


        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (Orchard.UI.Admin.AdminFilter.IsApplied(filterContext.RequestContext)) return;

            if (filterContext.Result is PartialViewResult) return;

            var workContext = _orchardServices.WorkContext;

            if (workContext.CurrentUser == null)
                workContext.Layout.Content.Add(_orchardServices.New.Lombiq_Privacy_ConsentBanner(), "after");
        }

        public void OnResultExecuted(ResultExecutedContext filterContext) { }
    }
}