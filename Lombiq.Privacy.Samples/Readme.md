# Lombiq Privacy for Orchard Core - Samples



## About

Example Orchard Core module that makes use of Lombiq Privacy for Orchard Core.

For general details about and usage instructions see the [root Readme](../Readme.md).

Do you want to quickly try out this project and see it in action? Check it out in our [Open-Source Orchard Core Extensions](https://github.com/Lombiq/Open-Source-Orchard-Core-Extensions) full Orchard Core solution and also see our other useful Orchard Core-related open-source projects!

## Training sections

Go to Recipes in the admin dashboard and select "Lombiq Privacy - Sample Content - Privacy, Forms, Workflows".

 - [Consent Banner Feature](../Readme.md#consent-banner-feature): Right after the recipe has imported, the consent banner comes up, and you can agree with it.
 - [Registration Consent Feature](../Readme.md#registration-consent-feature): Log out first, then navigate to user registration (/Register), and you can see, the registration consent checkbox at the bottom of registration form.
 - [Form Consent Feature](../Readme.md#form-consent-feature): Navigate to home or to /competitor-registration route, fill up the form, and you can't send the form until you don't accept the privacy policy.
   
   - [Workflow](Recipes/Lombiq.Privacy.Samples.WorkflowType.recipe.json): In this recipe you can find the workflow definition with `ValidatePrivacyConsentCheckboxTask` wich validates the acceptance of consent in the server side, and creates the competitor content item.
   - [Competitor registration](Recipes/Lombiq.Privacy.Samples.Content.recipe.json): In this recipe you can find the competitor registration form content item.

## Contributing and support

Bug reports, feature requests, comments, questions, code contributions, and love letters are warmly welcome, please do so via GitHub issues and pull requests. Please adhere to our [open-source guidelines](https://lombiq.com/open-source-guidelines) while doing so.

This project is developed by [Lombiq Technologies](https://lombiq.com/). Commercial-grade support is available through Lombiq.
