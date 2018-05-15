using Lombiq.Privacy.Models;
using Lombiq.Privacy.Services;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using static Lombiq.Privacy.Constants.FeatureNames;

namespace Lombiq.Privacy.Drivers
{
    [OrchardFeature(FormConsent)]
    public class ConsentCheckboxPartDriver : ContentPartDriver<ConsentCheckboxPart>
    {
        private readonly ICookieService _cookieService;


        public ConsentCheckboxPartDriver(ICookieService cookieService)
        {
            _cookieService = cookieService;
        }


        protected override DriverResult Editor(ConsentCheckboxPart part, dynamic shapeHelper) =>
            ContentShape("Parts_ConsentCheckbox_Edit", () =>
            {
                if (!_cookieService.UserHasConsent())
                    return shapeHelper.Parts_ConsentCheckbox_Edit();

                return null;
            });
    }
}