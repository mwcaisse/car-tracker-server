"use strict";

define("Service/proxy", [], function () {
	
	var proxy = new (function() {
		var self = this;

		self.baseUrl = "/api/";
		
		self.ajax = function(options) {
			var def = Q.defer();
			var defaults = {
				async: true,
				contentType: "application/json",
				data: null,
				method: "GET",
				success: function(data, textStatus, jqXHR) {
					self.successHandler(def, data);
				},
				error: function(jqXHR, textStatus, error) {
				    self.errorHandler(def, textStatus, error);
				}				
			};
			var ajaxOptions =$.extend(defaults, options);
			
			$.ajax(ajaxOptions);
			return def.promise;
		};

        //If the server returned a 200, just return the results to the caller
		self.successHandler = function(deferred, data) {
			deferred.resolve(data);
		};

        //If the server return a non 200, return the error message to the caller
		self.errorHandler = function(deferred, textStatus, error) {
			deferred.reject(error);
		};
		
		self.getAsbsolute = function(url) {
			return self.ajax({
				url: url,
				method: "GET"
			});
		};
		
		self.get = function(relativeUrl) {
			return self.getAsbsolute(self.baseUrl + relativeUrl);
		};
		
		self.getPaged = function (relativeUrl, startAt, maxResults, sort) {
			var sortString = "";
			if (sort) {
				sortString = "&propertyId=" + sort.propertyId +"&ascending=" + (sort.ascending ? "true" : "false");
			}
			return self.get(relativeUrl + "?startAt=" + startAt + "&maxResults=" + maxResults + sortString);
		};
		
		self.postAbsolute = function(url, body) {
			return self.ajax({
				url: url,
				data: JSON.stringify(body),	
				method: "POST"
			});
		};	
		
		self.post = function(relativeUrl, body) {
			return self.postAbsolute(self.baseUrl + relativeUrl, body);
		};
		
		self.putAbsolute = function (url, body) {
			return self.ajax({
				url: url,
				data: JSON.stringify(body),		
				method: "PUT"
			});
		};
		
		self.put = function(relativeUrl, body) {
			return self.putAbsolute(self.baseUrl + relativeUrl, body);
		};
		
		
	})();
	
	return proxy;
	
});