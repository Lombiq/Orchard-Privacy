using OrchardCore.Modules.Manifest;
using static Lombiq.Privacy.Constants.FeatureNames;

[assembly: Module(
    Name = "Lombiq Privacy",
    Author = "Lombiq Technologies",
    Website = "https://lombiq.com",
    Version = "1.0.0"
)]

[assembly: Feature(
    Id = ConsentBanner,
    Name = "Lombiq Privacy - Consent banner",
    Description = "Adds the ability to show a privacy consent banner.",
    Category = "Privacy",
    Dependencies = new[] { Module, "OrchardCore.CustomSettings" }
)]

[assembly: Feature(
    Id = RegistrationConsent,
    Name = "Lombiq Privacy - Registration consent",
    Description = "Adds a privacy consent checkbox to the registration form.",
    Category = "Privacy",
    Dependencies = new[] { Module, "OrchardCore.CustomSettings", "OrchardCore.Users.Registration" }
)]

[assembly: Feature(
    Id = FormConsent,
    Name = "Lombiq Privacy - Form consent",
    Description = "Provides Consent Checkbox widget that can attach to any form.",
    Category = "Privacy",
    Dependencies = new[] { Module, "OrchardCore.CustomSettings", "OrchardCore.Forms" }
)]
