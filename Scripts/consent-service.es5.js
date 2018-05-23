"use strict";

var ConsentCookie = {};

ConsentCookie = {
    consentCookieName: function consentCookieName() {
        return "HasConsent";
    },
    consentCookieTrueValue: function consentCookieTrueValue() {
        return "true";
    },
    Set: function Set(sessionCookie) {
        if (sessionCookie === this.consentCookieTrueValue()) {
            Cookies.set(this.consentCookieName(), this.consentCookieTrueValue());
        } else {
            Cookies.set(this.consentCookieName(), this.consentCookieTrueValue(), { expires: 1000 });
        }
    },
    HasConsentCookie: function HasConsentCookie() {
        return Cookies.get(this.consentCookieName()) === this.consentCookieTrueValue();
    }
};

