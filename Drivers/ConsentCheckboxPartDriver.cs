using Lombiq.Privacy.Models;
using Orchard;
using Orchard.ContentManagement.Drivers;

namespace Lombiq.Privacy.Drivers
{
    public class ConsentCheckboxPartDriver : ContentPartDriver<ConsentCheckboxPart>
    {
        private readonly IWorkContextAccessor _wca;


        public ConsentCheckboxPartDriver(IWorkContextAccessor wca)
        {
            _wca = wca;
        }


        protected override DriverResult Editor(ConsentCheckboxPart part, dynamic shapeHelper) =>
            ContentShape("Parts_ConsentCheckbox_Edit", () =>
            {
                if (_wca.GetContext()?.CurrentUser == null)
                    return shapeHelper.Parts_ConsentCheckbox_Edit();

                return null;
            });
    }
}