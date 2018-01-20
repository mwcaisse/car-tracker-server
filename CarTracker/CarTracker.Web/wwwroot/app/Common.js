"use strict";

var googleMapsApiKey = $("#googleMapsApiKey").val();
var rootPathPrefix = $("#rootPathPrefix").val();

require.config({
	
    baseUrl: rootPathPrefix + "/app/",	
	paths: {
        "q": rootPathPrefix + "/lib/q/js/q",
        "moment": rootPathPrefix +"/lib/moment/js/moment",
        "moment-duration-format": rootPathPrefix + "/lib/moment-duration-format/js/moment-duration-format"
	},
	googlemaps: {
		params: {
			key: googleMapsApiKey
		}
	}
});

//Highcharts options
Highcharts.setOptions({
	global: {
		useUTC: false
	}
});