using Lombiq.Privacy.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Layout;
using System.Threading.Tasks;

namespace Lombiq.Privacy.Filters;

public class PrivacyConsentBannerInjectionFilter(
    ILayoutAccessor layoutAccessor,
    IShapeFactory shapeFactory,
    IPrivacyConsentService consentService,
    IHttpContextAccessor hca) : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.IsNotFullViewRendering() || !await consentService.IsConsentBannerNeededAsync(hca.HttpContext))
        {
            await next();
            return;
        }

        var layout = await layoutAccessor.GetLayoutAsync();
        var contentZone = layout.Zones["Content"];
        await contentZone.AddAsync(await shapeFactory.CreateAsync("Lombiq_Privacy_ConsentBanner"));

        await next();
    }
}
