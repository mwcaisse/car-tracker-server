import Moment from "moment";
import Highcharts from "highcharts"

import * as Util from "services/Util.js";
import * as Constants from "services/Constants.js";
import * as FriendlyConstants from "services/FriendlyConstants.js";

// Register a global mixin to let the constants be available to each vue components
Vue.mixin({
    data: function() {
        return {
            constants: Constants
        };
    }
});

Vue.directive("tooltip", {
    bind: function (el, binding) {
        $(el).tooltip(binding.value);
    }
});

/** Binding for High Charts
 * 
 * 	Uses the value as the options to initialize high charts	
 */
Vue.directive("highcharts", function (el, binding) {
    if (typeof binding.oldValue === "undefined" || binding.oldValue !== binding.value) {
        Highcharts.chart(el, binding.value);
    }
});

function formatDateFilter(value, formatString) {
    if (typeof value === "undefined" || null == value) {
        return "";
    }
    if (typeof value.format !== "function") {
        value = Moment(value);
    }
    return Util.formatDateTime(value, formatString);
}

Vue.filter("formatDateTime", function (value, formatString) {
    return formatDateFilter(value, formatString);
});

Vue.filter("formatDate", function (value, formatString) {
    if (Util.isStringNullOrBlank(formatString)) {
        formatString = "YYYY-MM-DD";
    }
    return formatDateFilter(value, formatString);
});

Vue.filter("formatDuration", function (value, formatString) {
    if (typeof value === "undefined" || typeof value.format !== "function") {
        return "";
    }
    return Util.formatDuration(value, formatString);
});

Vue.filter("formatBoolean", function (value, nullIsBlank) {
    if (value === true) {
        return "Yes";
    }
    else if (value !== false && nullIsBlank) {
        return "";
    }
    return "No";
});

Vue.filter("round", function (value, places) {
    if (typeof value === "undefined") {
        return "";
    }

    if (typeof places === "undefined") {
        places = 2;
    }
    return Util.round(value, places);
});

Vue.filter("kmToMi", function (value) {
    if (typeof value === "undefined") {
        return "";
    }
    return Util.convertKmToMi(value);
});	

Vue.filter("friendlyConstant", function (value, constantName) {
    var e = FriendlyConstants[constantName];
    if (e) {
        return e[value] || "";
    }
    return "";
});

Vue.filter("prettyJson", function (value) {
    if (typeof value === "string") {
        if (Util.isStringNullOrBlank(value)) {
            return "";
        }
        try {
            value = JSON.parse(value);
        } catch (e) {
            return value;
        }
    }
    return JSON.stringify(value, null, 2);
});

Vue.filter("prettyNumber", function (value, places) {
    if (typeof value === "undefined") {
        return "";
    }
    if (typeof places === "undefined") {
        places = 2;
    }
    return Util.prettyNumber(value, places);

});