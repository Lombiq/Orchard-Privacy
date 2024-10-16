﻿using Lombiq.HelpfulLibraries.OrchardCore.Contents;
using Lombiq.Privacy.ViewModels;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Users.Models;

namespace Lombiq.Privacy.Drivers;

public sealed class RegistrationCheckboxDriver : DisplayDriver<RegisterUserForm>
{
    public override IDisplayResult Edit(RegisterUserForm model, BuildEditorContext context) =>
        Initialize<PrivacyRegistrationConsentCheckboxViewModel>("Lombiq_Privacy_RegistrationCheckbox", _ => { })
            .Location(CommonLocationNames.Content + ":after");
}
