"use strict";

define("Views/Trip/Trip", 
    ["moment",
     "Service/system", 
     "Service/util", 
	 "Service/navigation", 
	 "Service/applicationProxy",
	 "AMD/text!Views/Trip/Trip.html",	 
	 "Components/Trip/TripDetails/TripDetails",
	 "Components/Trip/TripMap/TripMap",
	 "Components/Trip/TripChart/TripSpeedChart",
	 "Components/Trip/TripChart/TripEngineChart",
	 "Components/Trip/TripChart/TripThrottleChart",
	 "Components/Trip/TripChart/TripMAFChart",
     "Components/Trip/TripChart/TripTemperatureChart",
     "Components/Log/ReaderLogGrid/ReaderLogGrid"], 
	 function (moment, system, util, navigation, proxy, template) {

        navigation.setActiveNavigation("Trip");

	    return function(elementId) {
		    var tripId = parseInt(util.getURLParameter("tripId", 92), 10);	
		
		    return new Vue({
			    el: elementId,
			    template: template,
			    data: {
                    tripId: tripId,
                    tripStartDate: null,
                    tripEndDate: null
                },
                methods: {
                    fetchTrip: function() {
                        return proxy.trip.get(this.tripId).then(function (data) {
                            this.tripStartDate = moment(data.startDate).subtract(5, "minutes").toDate();
                            this.tripEndDate = moment(data.endDate).add(5, "minutes").toDate();
                        }.bind(this),
                        function (error) {
                            system.showAlert(error, "error");
                        });
                    }
                },
                created: function() {
                    this.fetchTrip();
                }
		    });
		
	    };
	
});