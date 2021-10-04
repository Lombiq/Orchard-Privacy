using Lombiq.Privacy.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Layout;
using System.Threading.Tasks;

namespace Lombiq.Privacy.Filters
{
    public class PrivacyConsentBannerInjectionFilter : IAsyncResultFilter
    {
        private readonly ILayoutAccessor _layoutAccessor;
        private readonly IShapeFactory _shapeFactory;
        private readonly IPrivacyConsentService _consentService;
        private readonly IHttpContextAccessor _hca;

        public PrivacyConsentBannerInjectionFilter(
            ILayoutAccessor layoutAccessor,
            IShapeFactory shapeFactory,
            IPrivacyConsentService consentService,
            IHttpContextAccessor hca)
        {
            _layoutAccessor = layoutAccessor;
            _shapeFactory = shapeFactory;
            _consentService = consentService;
            _hca = hca;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.IsNotFullViewRendering() || !await _consentService.IsConsentBannerNeededAsync(_hca.HttpContext))
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
