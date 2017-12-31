"use strict";

var googleMapsApiKey = $("#googleMapsApiKey").val();

require.config({
	
	baseUrl: "/cartracker/app/",	
	paths: {
		"q": "../lib/q/js/q",
		"moment": "../lib/moment/js/moment",
		"moment-duration-format": "../lib/moment-duration-format/js/moment-duration-format"
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