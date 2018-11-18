import Q from "q"

const KM_IN_MI = 0.621371;

function convertKmToMi (km) {
    return km * KM_IN_MI;
}

function convertCelsiusToFah(c) {
    return c * 1.8 + 32;
}

function getURLParameter(name, def) {
    if (typeof def === "undefined") {
        def = "";
    }
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? def : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function isStringNullOrBlank(string) {
    return (typeof string === "undefined" ||
        string === null ||
        typeof string !== "string" ||
        string.length === 0 ||
        string.trim().length === 0);
};

function formatDateTime(date, formatString) {
    if (typeof formatString === "undefined") {
        formatString = "YYYY-MM-DD HH:mm:ss";
    }
    if (date && date.isValid()) {
        return date.format(formatString);
    }
    return "";
};

function formatDuration(duration, formatString) {
    if (typeof formatString === "undefined") {
        formatString = "hh:mm:ss";
    }
    if (duration) {
        return duration.format(formatString);
    }
    return "";
}

function round(num, places) {
    if (!places) {
        places = 2;
    }
    return parseFloat(num).toFixed(places);
};

function prettyNumber(num, places) {
    if (!places) {
        places = 2;
    }
    return parseFloat(num).toLocaleString(undefined, {
        minimumFractionDigits: places,
        maximumFractionDigits: places
    });
}

function scrollToTop() {
    window.scrollTo(0, 0);
}

function generateUuid() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}

function jsonp(url, callbackName) {
    var script = document.createElement("script");
    script.type = "text/javascript";
    script.src = url;
    script.setAttribute("async", "");
    script.setAttribute("defer", ""); // if async is not supported

    var def = Q.defer();
    window[callbackName] = def.resolve;

    var s = document.getElementsByTagName("script")[0];
    s.parentNode.insertBefore(script, s);

    return def.promise;
}

export {
    convertKmToMi,
    convertCelsiusToFah,
    getURLParameter,
    isStringNullOrBlank,
    formatDateTime,
    formatDuration,
    round,
    prettyNumber,
    scrollToTop,
    generateUuid,
    jsonp
}