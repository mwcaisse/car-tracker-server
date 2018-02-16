"use strict";

define("Views/Admin/RegistrationKeys/RegistrationKeys", 
	["Service/util", 
	 "Service/navigation", 
	 "AMD/text!Views/Admin/RegistrationKeys/RegistrationKeys.html",
	 "Components/Admin/RegistrationKeyGrid/RegistrationKeyGrid"], 
	 
    function (util, navigation, template) {	

        navigation.setActiveNavigation("Admin");	
	
		return function (elementId) {	
			
			return new Vue({			
				el: elementId,
				template: template			
			});
		};
		
});