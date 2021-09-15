namespace Lombiq.Privacy.Settings
{
    public class ConsentBannerSettings
    {
        public string ConsentBannerSettingsText { get; set; } =
            "<div>Our site uses browser cookies to personalize your experience. " +
            "Regarding this issue, please find our information on data control and processing " +
            "<a href='/privacy-policy' target='_blank'>here</a> " +
            "and our information on server logging and usage of so-called 'cookies' " +
            "<a href='/privacy-policy' target='_blank'>here</a>. " +
            "By clicking here You confirm your approval of the data processing activities mentioned in these documents. " +
            "Please note that You may only use this website efficiently if you agree to these conditions.</div>";
    }
}