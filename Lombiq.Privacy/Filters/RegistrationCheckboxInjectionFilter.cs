using Lombiq.Privacy.ViewModels;
using Microsoft.AspNetCore.Mvc.Filters;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Layout;
using OrchardCore.Modules;
using OrchardCore.Mvc.Core.Utilities;
using OrchardCore.Users.Controllers;
using System.Threading.Tasks;

namespace Lombiq.Privacy.Filters;

public class RegistrationCheckboxInjectionFilter(
    ILayoutAccessor layoutAccessor,
    IShapeFactory shapeFactory) : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var routeValues = context.ActionDescriptor.RouteValues;
        if (context.IsNotFullViewRendering() ||
            !routeValues["Area"].EqualsOrdinalIgnoreCase($"{nameof(OrchardCore)}.{nameof(OrchardCore.Users)}") ||
            !routeValues["Controller"].EqualsOrdinalIgnoreCase(typeof(RegistrationController).ControllerName()) ||
            !routeValues["Action"].EqualsOrdinalIgnoreCase(nameof(RegistrationController.Register)))
        {
            await next();
            return;
        }

        var layout = await layoutAccessor.GetLayoutAsync();
        var afterRegisterZone = layout.Zones["AfterRegister"];
        var shape = await shapeFactory.CreateAsync<PrivacyRegistrationConsentCheckboxViewModel>(
            "Lombiq_Privacy_RegistrationCheckbox",
            viewModel => viewModel.RegistrationCheckbox = false);

        await afterRegisterZone.AddAsync(shape);

        await next();
    }
}
