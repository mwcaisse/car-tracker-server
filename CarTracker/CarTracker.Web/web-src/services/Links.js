import System from "services/System.js";
import { EVENT_NAVIGATION_ACTIVE_CHANGED } from "services/Constants.js";

class Links {
    constructor() {
        this._rootPathPrefix = ($("#rootPathPrefix").val() || "") + "/";
    }

    setActiveNavigation(navigationId) {
        System.events.$emit(EVENT_NAVIGATION_ACTIVE_CHANGED, { id: navigationId });
    }

    get rootPathPrefix() {
        return this._rootPathPrefix;
    }

    navigateTo(url) {
        window.location = url;
    }

    home() {
        return this._rootPathPrefix;
    }

    // Reader Links

    readerLog() {
        return this._rootPathPrefix + "log/reader";
    }

    // Trip Links

    viewTrip(tripId) {
        return this._rootPathPrefix + "trip/?tripId=" + tripId;
    }

    // Car Links

    viewCar(carId) {
        return this._rootPathPrefix + "car/?carId=" + carId;
    }

    // Admin Links

    adminRegistrationKey() {
        return this._rootPathPrefix + "admin/registration-keys";
    }

    adminRequestLogs() {
        return this._rootPathPrefix + "admin/log/request";
    }

    adminRequestLogsDetails(requestUuid) {
        return this._rootPathPrefix + "admin/log/request/" + requestUuid + "/";
    }

    adminServerLogs() {
        return this._rootPathPrefix + "admin/log/server";
    }

    // User Links

    userAuthenticationTokens() {
        return this._rootPathPrefix + "user/tokens";
    }

    userCustomPlaces() {
        return this._rootPathPrefix + "user/places";
    }

    login(param) {
        return this._rootPathPrefix + "login" + (param ? ("?" + param) : "");
    }

    logout() {
        return this._rootPathPrefix + "logout";
    }
    
}

export default new Links();