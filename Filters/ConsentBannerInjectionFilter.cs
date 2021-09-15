using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Layout;
using System.Threading.Tasks;

namespace Lombiq.Privacy.Filters
{
    public class ConsentBannerInjectionFilter : IAsyncResultFilter
    {
        private readonly ILayoutAccessor _layoutAccessor;
        private readonly IShapeFactory _shapeFactory;
        private readonly IHttpContextAccessor _hca;
        private readonly IOptions<CookiePolicyOptions> _cookiePolicyOptions;

        public ConsentBannerInjectionFilter(
            ILayoutAccessor layoutAccessor,
            IShapeFactory shapeFactory,
            IHttpContextAccessor hca,
            IOptions<CookiePolicyOptions> cookiePolicyOptions)
        {
            _layoutAccessor = layoutAccessor;
            _shapeFactory = shapeFactory;
            _hca = hca;
            _cookiePolicyOptions = cookiePolicyOptions;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var consentFeature = _hca.HttpContext.Features.Get<ITrackingConsentFeature>();
            var consentCookie = _hca.HttpContext.Request.Cookies[_cookiePolicyOptions.Value.ConsentCookie.Name];

            if (context.IsNotFullViewRendering() || !consentFeature.IsConsentNeeded || consentCookie != null)
            {
                await next();
                return;
            }

            var layout = await _layoutAccessor.GetLayoutAsync();
            var contentZone = layout.Zones["Content"];
            await contentZone.AddAsync(await _shapeFactory.CreateAsync("Lombiq_Privacy_ConsentBanner"));

            await next();
        }
    }
}
