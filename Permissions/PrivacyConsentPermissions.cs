using Lombiq.HelpfulLibraries.Libraries.Users;
using OrchardCore.Security.Permissions;
using System.Collections.Generic;

namespace Lombiq.Privacy.Permissions
{
    public class PrivacyConsentPermissions : AdminPermissionBase
    {
        public static readonly Permission ManagePrivacyConsent =
            new(nameof(ManagePrivacyConsent), "Manage Privacy consent settings");

        protected override IEnumerable<Permission> AdminPermissions => new[] { ManagePrivacyConsent };
    }
}
