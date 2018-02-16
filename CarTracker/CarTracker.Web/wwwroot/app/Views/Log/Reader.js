"use strict";

define("Views/Log/Reader", 
	["Service/util", 
	 "Service/navigation", 
	 "AMD/text!Views/Log/Reader.html",
	 "Components/Log/ReaderLogGrid/ReaderLogGrid"], function (util, navigation, template) {

        navigation.setActiveNavigation("Log");

	    return function(elementId) {
		
		    return new Vue({			
			    el: elementId,
			    template: template			
		    });	
	};
});