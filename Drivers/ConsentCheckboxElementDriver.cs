using Orchard.DynamicForms.Elements;
using Orchard.Layouts.Framework.Display;
using Orchard.Layouts.Framework.Drivers;
using Orchard.Layouts.Helpers;
using Orchard.Layouts.Services;
using Orchard.Tokens;

namespace Lombiq.Privacy.Drivers
{
    public class ConsentCheckboxElementDriver : FormsElementDriver<ConsentCheckbox>
    {
        private readonly ITokenizer _tokenizer;


        public ConsentCheckboxElementDriver(IFormsBasedElementServices formsServices, ITokenizer tokenizer)
            : base(formsServices)
        {
            _tokenizer = tokenizer;
        }


        protected override void OnDisplaying(ConsentCheckbox element, ElementDisplayingContext context)
        {
            context.ElementShape.ProcessedName = _tokenizer.Replace("ConsentCheckbox", context.GetTokenData());
            // https://github.com/OrchardCMS/Orchard/issues/4123
            context.ElementShape.ProcessedValue = _tokenizer.Replace("false", context.GetTokenData());
        }
    }
}