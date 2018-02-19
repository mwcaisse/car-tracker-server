"use strict";

define("Service/proxy", ["Service/system"], function (system) {
	
	return new (function() {
		var self = this;

		self.baseUrl = system.baseUrl + "api/";
		
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

	    self.getUrlValue = function(val) {
	        if (val instanceof Date) {
	            return val.valueOf();
	        }
	        return val;
	    };
		
		self.getPaged = function (relativeUrl, skip, take, sort, filter) {
            var sortString = "";
		    var filterString = "";
			if (sort && sort.propertyId) {
				sortString = "&columnName=" + sort.propertyId +"&ascending=" + (sort.ascending ? "true" : "false");
            }
            if (filter) {
                $.each(filter, function (property, filters) {
                    $.each(filters, function (op, val) {
                        filterString += "&" + property + "__" + op + "=" + self.getUrlValue(val);
                    });
                });
            }
            return self.get(relativeUrl + "?skip=" + skip + "&take=" + take + sortString + filterString);
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
	
});