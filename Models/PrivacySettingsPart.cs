﻿using Orchard.ContentManagement;

namespace Lombiq.Privacy.Models
{
    public class PrivacySettingsPart : ContentPart
    {
        public bool EnableConsentBanner
        {
            get { return this.Retrieve(x => x.EnableConsentBanner); }
            set { this.Store(x => x.EnableConsentBanner, true); }
        }
    }
}