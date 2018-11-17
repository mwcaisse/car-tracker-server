class Links {
    constructor() {
        this._rootPathPrefix = ($("#rootPathPrefix").val() || "") + "/";
    }

    get rootPathPrefix() {
        return this._rootPathPrefix;
    }

    get home() {
        return this._rootPathPrefix;
    }

    get buildToken() {
        return this._rootPathPrefix + "build-token/";
    }

    get logout() {
        return this._rootPathPrefix + "logout";
    }

    get userAuthenticationTokens() {
        return this._rootPathPrefix + "user/tokens";
    }

    get login() {
        return this._rootPathPrefix + "login";
    }

    application(id) {
        return this._rootPathPrefix + "application/?applicationId=" + id;
    }
}

export default new Links();