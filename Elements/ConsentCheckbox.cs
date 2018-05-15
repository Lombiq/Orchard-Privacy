using Orchard.Localization;

namespace Orchard.DynamicForms.Elements
{
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