var consentCookieName = "HasConsent";
var consentCookieTrueValue = "true";

var ConsentCookie = {};

ConsentCookie = {
    Set: function (sessionCookie) {
        if (sessionCookie === consentCookieTrueValue) {
            Cookies.set(consentCookieName, consentCookieTrueValue);
        }
        else {
            Cookies.set(consentCookieName, consentCookieTrueValue, { expires: 1000 });
        }
    },
    HasConsentCookie: function () {
        return Cookies.get(consentCookieName) === consentCookieTrueValue;
    }
}