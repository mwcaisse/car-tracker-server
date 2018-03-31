"use strict";

define("Views/Car/Car", 
	["Service/util",
     "Service/navigation", 
     "AMD/text!Views/Car/Car.html",
	 "Components/Car/CarDetails/CarDetails",
	 "Components/Car/CarSupportedCommands/CarSupportedCommands",
	 "Components/Car/CarMaintenanceGrid/CarMaintenanceGrid",
	 "Components/Trip/TripGrid/TripGrid"], 
	 
	 function (util, navigation, template) {

        navigation.setActiveNavigation("Car");

		return function (elementId) {
			var carId = parseInt(util.getURLParameter("carId", -1), 10);		
			
			return new Vue({			
				el: elementId,
				template: template,
				data: {
					carId: carId
				}
			});
		};
	
});