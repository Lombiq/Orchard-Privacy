using Orchard.Environment.Extensions;
using Orchard.Localization;
using static Lombiq.Privacy.Constants.FeatureNames;

namespace Orchard.DynamicForms.Elements
{
    [OrchardFeature(Lombiq_Privacy_Form_Consent)]
    public class ConsentCheckbox : FormElement
    {
        public override string Name => "ConsentCheckbox";
        public override string Category => "Forms";
        public override LocalizedString Description => T("Add a consent checkbox to your layout.");
        public override LocalizedString DisplayText => T("Consent Checkbox");
        public override bool HasEditor => false;
        public override string ToolboxIcon => "\uf046";
    }
}