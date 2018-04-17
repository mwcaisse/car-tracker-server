"use strict";

define("Service/customDirectives", 
    ["moment", "Service/util", "Service/enums", "Service/constants"],
    function (moment, util, enums, constants) {


    // Register a global mixin to inject constants into each Vue component
    Vue.mixin({
        data: function () {
            return {
                constants: constants
            }
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
			var $el = $(el);
			$el.highcharts(binding.value);
		}
	});

    function formatDateFilter(value, formatString) {
        if (typeof value === "undefined" || null == value) {
            return "";
        }
        if (typeof value.format !== "function") {
            value = moment(value);
        }
        return util.formatDateTime(value, formatString);
    }

    Vue.filter("formatDateTime", function (value, formatString) {
        return formatDateFilter(value, formatString);
    });

    Vue.filter("formatDate", function (value, formatString) {
        if (util.isStringNullOrBlank(formatString)) {
            formatString = "YYYY-MM-DD";
        }
        return formatDateFilter(value, formatString);
    }); 
	
	Vue.filter("formatDuration", function (value, formatString) {
		if (typeof value === "undefined" || typeof value.format !== "function") {
			return "";
		}
		return util.formatDuration(value, formatString);
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
		return util.round(value, places);
	
	});
	
	Vue.filter("kmToMi", function (value) {
		if (typeof value === "undefined") {
			return "";
		}
		return util.convertKmToMi(value);
    });	

    Vue.filter("enum", function (value, enumName) {
        var e = enums[enumName];
        if (e) {
            return e[value] || "";
        }
        return "";
    });

    Vue.filter("prettyJson", function (value) {
        if (typeof value === "string") {
            if (util.isStringNullOrBlank(value)) {
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

    Vue.filter("prettyNumber", function(value, places) {
        if (typeof value === "undefined") {
            return "";
        }
        if (typeof places === "undefined") {
            places = 2;
        }
        return util.prettyNumber(value, places);

    });
	
	return {};	
});