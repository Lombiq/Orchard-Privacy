var ConsentCookie = {};

ConsentCookie = {
    consentCookieName: function () {
        return "HasConsent";
    },
    consentCookieTrueValue: function () {
        return "true";
    },
    Set: function (sessionCookie) {
        if (sessionCookie === this.consentCookieTrueValue()) {
            Cookies.set(this.consentCookieName(), this.consentCookieTrueValue());
        }
        else {
            Cookies.set(this.consentCookieName(), this.consentCookieTrueValue(), { expires: 1000 });
        }
    },
    HasConsentCookie: function () {
        return Cookies.get(this.consentCookieName()) === this.consentCookieTrueValue();
    }
}