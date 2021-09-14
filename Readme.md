# Lombiq Privacy Orchard module



## About

Orchard module containing features related to data protection/privacy and the EU law on it, the [General Data Protection Regulation](http://eur-lex.europa.eu/legal-content/EN/TXT/?qid=1462439808430&uri=CELEX:32016R0679) (GDPR).

Do not forget to create a privacy policy page that you need to link to from the various consent-asking features.

**Important!** Using this module will not make your site GDPR-compliant alone.

The module is also available for [DotNest](https://dotnest.com/) sites.

**NOTE:** This module has an Orchard 1 version in the [dev-orchard-1 branch](https://github.com/Lombiq/Orchard-Privacy/tree/dev-orchard-1).

## Features

The module consists of the following independent features:

### Consent Banner Feature

Shows a banner where unauthenticated users can accept the privacy policy. 

### Registration Consent Feature

Shows a privacy consent checkbox on the registration form that needs to be checked by the users to be able to register. After registration, the user's consent is stored in the Privacy section of the user's properties.

**NOTE:** If the user registered before this feature was enabled then they can accept the consent with the consent banner (if it's enabled). The consent will be stored in this case as well. 

### Form Consent Feature

Adds a new `ConsentCheckbox` content type that can be attached to Dynamic Forms. In this case the users must accept the privacy policy before they can post content to the site. You can validate the consent wiht `Validate Consent Checkbox` workflow activity on the following way:

![Consent Checkbox Workflow](Docs/ConsentCheckboxWorkflow.png)
 

## Contributing and support

Bug reports, feature requests, comments, questions, code contributions, and love letters are warmly welcome, please do so via GitHub issues and pull requests. Please adhere to our [open-source guidelines](https://lombiq.com/open-source-guidelines) while doing so.

This project is developed by [Lombiq Technologies](https://lombiq.com/). Commercial-grade support is available through Lombiq.