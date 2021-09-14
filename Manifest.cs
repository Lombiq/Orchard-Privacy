using OrchardCore.Modules.Manifest;
using static Lombiq.Privacy.Constants.FeatureNames;

[assembly: Module(
    Name = "Tenant Admin",
    Author = "Lombiq Technologies",
    Website = "https://lombiq.com",
    Version = "1.0.0"
)]

[assembly: Feature(
    Id = Module,
    Name = "Lombiq Privacy",
    Description = "Various data protection/privacy-related, GDPR-ready features.",
    Category = "Privacy"
)]

[assembly: Feature(
    Id = ConsentBanner,
    Name = "Lombiq Privacy - Consent banner",
    Description = "Adds the ability to show a privacy consent banner.",
    Category = "Privacy",
    Dependencies = new[] { Module }
)]

[assembly: Feature(
    Id = RegistrationConsent,
    Name = "Lombiq Privacy - Registration consent",
    Description = "Adds a privacy consent checkbox to the registration form.",
    Category = "Privacy",
    Dependencies = new[] { Module, "OrchardCore.Users.Registration" }
)]

[assembly: Feature(
    Id = FormConsent,
    Name = "Lombiq Privacy - Form consent",
    Description = "An attachable part that displays a privacy consent checkbox.",
    Category = "Privacy",
    Dependencies = new[] { Module, "OrchardCore.Forms" }
)]
