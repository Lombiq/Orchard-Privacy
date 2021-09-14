using Lombiq.Privacy.Models;
using Lombiq.Privacy.ViewModels;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.Views;

namespace Lombiq.Privacy.Drivers
{
    public class ConsentCheckboxPartDisplayDriver : ContentPartDisplayDriver<ConsentCheckboxPart>
    {
        public override IDisplayResult Display(ConsentCheckboxPart part, BuildPartDisplayContext context) =>
            Initialize<ConsentCheckboxPartViewModel>(
                GetDisplayShapeType(context),
                viewModel => viewModel.ConsentCheckbox = part.ConsentCheckbox)
                .Location("Detail", "Content");

        public override IDisplayResult Edit(ConsentCheckboxPart part, BuildPartEditorContext context) =>
            View(GetEditorShapeType(context), part);
    }
}
