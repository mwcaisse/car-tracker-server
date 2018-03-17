"use strict";

define("Views/UserPlace/UserPlace", 
    ["moment",
     "Service/system", 
     "Service/util", 
	 "Service/navigation", 
	 "Service/applicationProxy",
	 "AMD/text!Views/UserPlace/UserPlace.html",	 
	 "Components/UserPlace/UserPlaceGrid/UserPlaceGrid",
	 "Components/UserPlace/UserPlaceDetails/UserPlaceDetails"], 
	 function (moment, system, util, navigation, proxy, template) {

         navigation.setActiveNavigation("User/CustomPlaces");

	    return function(elementId) {
		
		    return new Vue({
			    el: elementId,
			    template: template,
			    data: {
                },
                methods: {
         
                },
                created: function() {
                    
                }
		    });
		
	    };
	
});