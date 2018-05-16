"use strict";

var consentCookieName = "HasConsent";
var consentCookieTrueValue = "true";

var ConsentCookie = {};

ConsentCookie = {
    Set: function Set(sessionCookie) {
        if (sessionCookie === consentCookieTrueValue) {
            Cookies.set(consentCookieName, consentCookieTrueValue);
        } else {
            Cookies.set(consentCookieName, consentCookieTrueValue, { expires: 1000 });
        }
    },
    HasConsentCookie: function HasConsentCookie() {
        return Cookies.get(consentCookieName) === consentCookieTrueValue;
    }
};

