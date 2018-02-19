"use strict";

define("Service/system", ["Service/enums", "Service/constants", "Service/customDirectives"],
    function (enums, constants) {

	return new (function() {
		var self = this;
		//Vue Instance for an event bus
		self.bus = new Vue();
	
		self.ALERT_TYPE_ERROR = "ERROR";
		self.ALERT_TYPE_SUCCESS = "SUCCESS";
		self.ALERT_TYPE_WARN = "WARN";
		self.ALERT_TYPE_INFO = "INFO";
		
		self.EVENT_ALERT_SHOW = "alert:show";
		self.EVENT_ALERT_CLEAR = "alert:clear";
		
        self.isAuthenticated = $("#isAuthenticated").val() === "true";	
        self.rootPathPrefix = $("#rootPathPrefix").val();

        self.baseUrl = self.rootPathPrefix + "/";	

        self.enums = enums;
        self.constants = constants;

        self.showAlert = function(message, type) {
            self.bus.$emit(self.EVENT_ALERT_SHOW,
                {
                    message: message,
                    type: type
                }
            );
        }

	    

	})();
	
});