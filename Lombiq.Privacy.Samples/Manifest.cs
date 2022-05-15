using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "Lombiq Privacy - Samples",
    Author = "Lombiq Technologies",
    Website = "https://github.com/Lombiq/Orchard-Privacy",
    Version = "3.1.0-alpha",
    Description = "Samples for Lombiq Privacy.",
    Category = "Privacy",
    Dependencies = new[]
    {
        Lombiq.Privacy.Constants.FeatureNames.Module,
    }
)]
