import Vue from "vue";
import VueConfig from "services/VueCommon.js";
import "services/CustomDirectives.js";
import { EVENT_ALERT_SHOW } from "services/Constants.js";

class System {
    constructor() {
        this._rootPathPrefix = $("#rootPathPrefix").val();
        this._isAuthenticated = $("#isAuthenticated").val() === "true";	
        this._events = new Vue();

        VueConfig();
    }

    get events() {
        return this._events;
    }

    get rootPathPrefix() {
        return this._rootPathPrefix;
    }

    get isAuthenticated() {
        return this._isAuthenticated;
    }

    showAlert(message, type) {
        this._events.$emit(EVENT_ALERT_SHOW,
        {
            message: message,
            type: type
        });
    }
}

export default new System();