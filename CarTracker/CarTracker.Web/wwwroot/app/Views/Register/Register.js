"use strict";

define("Views/Register/Register", 
	["Service/system", "Service/util", "Service/navigation",	 
	 "AMD/text!Views/Register/Register.html",
	 "Components/Auth/Registration/Registration",], 
	 
	 function (system, util, navigation, template) {
	
        return function (elementId) {	
		
            return new Vue({
	            el: elementId,
	            template: template		
            });
        };

	
});