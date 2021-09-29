using Lombiq.Privacy.ViewModels;
using Microsoft.AspNetCore.Mvc.Filters;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Layout;
using OrchardCore.Mvc.Core.Utilities;
using OrchardCore.Users.Controllers;
using System;
using System.Threading.Tasks;

namespace Lombiq.Privacy.Filters
{
    public class RegistrationCheckboxInjectionFilter : IAsyncResultFilter
    {
        private readonly ILayoutAccessor _layoutAccessor;
        private readonly IShapeFactory _shapeFactory;

        public RegistrationCheckboxInjectionFilter(
            ILayoutAccessor layoutAccessor,
            IShapeFactory shapeFactory)
        {
            _layoutAccessor = layoutAccessor;
            _shapeFactory = shapeFactory;
        }

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

            var layout = await _layoutAccessor.GetLayoutAsync();
            var afterRegisterZone = layout.Zones["AfterRegister"];
            var shape = await _shapeFactory.CreateAsync<PrivacyRegistrationConsentCheckboxViewModel>(
                "Lombiq_Privacy_RegistrationCheckbox",
                viewModel => viewModel.RegistrationCheckbox = false);

            await afterRegisterZone.AddAsync(shape);

            await next();
        }
    }
}
