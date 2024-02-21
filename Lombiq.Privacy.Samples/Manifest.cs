using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "Lombiq Privacy - Samples",
    Author = "Lombiq Technologies",
    Website = "https://github.com/Lombiq/Orchard-Privacy",
    Version = "0.0.1",
    Description = "Samples for Lombiq Privacy.",
    Category = "Privacy",
    Dependencies =
    [
        Lombiq.HelpfulExtensions.FeatureIds.ContentTypes,
        Lombiq.Privacy.Constants.FeatureNames.ConsentBanner,
        Lombiq.Privacy.Constants.FeatureNames.FormConsent,
        Lombiq.Privacy.Constants.FeatureNames.RegistrationConsent,
        "OrchardCore.ContentFields",
        "OrchardCore.Workflows.Http",
    ]
)]
