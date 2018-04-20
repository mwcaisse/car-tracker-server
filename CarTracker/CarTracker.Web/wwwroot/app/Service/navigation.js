"use strict";

define("Service/navigation", ["Service/system"], function (system) {
	
	return new (function() {
		var self = this;

		self.navigateTo = function(url) {
			window.location = url;
		};		
		
		self.homeLink = function() {
			return system.baseUrl;
        };

		self.readerLogLink = function () {
			return system.baseUrl + "log/reader";
		};
		
		self.viewTripLink = function (tripId) {
			return system.baseUrl + "trip/?tripId=" + tripId;
		};
		
		self.viewCarLink = function (carId) {
			return system.baseUrl + "car/?carId=" + carId;
		};
		
		self.logoutLink = function () {
			return system.baseUrl + "logout";
		};
		
		self.adminRegistrationKeyLink = function () {
			return system.baseUrl + "admin/registration-keys";
        };

	    self.adminRequestLogsLink = function() {
	        return system.baseUrl + "admin/log/request";
        };

        self.viewRequestLogDetailsLink = function(requestUuid) {
            return system.baseUrl + "admin/log/request/" + requestUuid + "/";
        }

        self.adminServerLogsLink = function () {
            return system.baseUrl + "admin/log/server";
        };
		
		self.loginLink = function (param) {
			return system.baseUrl + "login" + (param ? ("?" + param) : "");
		};
		
		self.userAuthenticationTokensLink = function () {
			return system.baseUrl + "user/tokens";
        };

        self.userCustomPlacesLink = function() {
            return system.baseUrl + "user/places";
        }
		
		self.navigateToHome = function () {
			self.navigateTo(self.homeLink());
		};
		
		self.navigateToViewTrip = function (tripId) {
			self.navigateTo(self.viewTripLink(tripId));
		};
		
		self.navigateToViewCar = function (carId) {
			self.navigateTo(self.viewCarLink(carId));
		};
		
		self.navigateToLogout = function () {
			return self.navigateTo(self.logoutLink());
		};
		
		self.navigateToLogin = function (param) {
			return self.navigateTo(self.loginLink(param));
		};
		
		self.navigateToAdminRegistrationKey = function () {
			return self.navigateTo(self.adminRegistrationKeyLink());
		};
		
		self.EVENT_NAVIGATION_ACTIVE_CHANGED = "navigation:activeChanged";
		
		self.setActiveNavigation = function (navigationId) {
            system.bus.$emit(self.EVENT_NAVIGATION_ACTIVE_CHANGED, { id: navigationId });
		};
	
		
	})();
	
});