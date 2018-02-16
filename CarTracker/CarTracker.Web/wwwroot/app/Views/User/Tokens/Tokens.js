"use strict";

define("Views/User/Tokens/Tokens", 
	["Service/util", 
	 "Service/navigation", 	 
	 "AMD/text!Views/User/Tokens/Tokens.html",
	 "Components/User/AuthTokenGrid/AuthTokenGrid" ], function (util, navigation, template) {


        navigation.setActiveNavigation("User");

	    return function(elementId) {
		
		    return new Vue({
			    el: elementId,
			    template: template
		    });
	    };
	
	

	
});