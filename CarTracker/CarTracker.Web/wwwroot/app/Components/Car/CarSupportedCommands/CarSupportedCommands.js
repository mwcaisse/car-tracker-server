"use strict";

define("Components/Car/CarSupportedCommands/CarSupportedCommands", 
		["moment", "Service/system", "Service/util", "Service/applicationProxy", "Service/navigation", 
         "AMD/text!Components/Car/CarSupportedCommands/CarSupportedCommands.html"],
	function (moment, system, util, proxy, navigation, template) {
	
	return Vue.component("app-car-supported-commands", {
		data: function() {
			return {
				supportedCommands: []
			}
		},	
		props: {
			carId: {
				type: Number,
				required: true
			}
		},
		template: template,
		methods: {
			fetchSupportedCommands: function () {
				proxy.car.getSupportedCommands(this.carId).then(function (data) {				
					this.update(data);
				}.bind(this),
				function (error) {
				    system.showAlert(error, "error");
				})
			},	
			update: function (data) {
				if (data) {
					this.supportedCommands = data;
				}
				else {
					this.supportedCommands = [];
				}
			},
			refresh: function () {
				this.fetchSupportedCommands();
			}				
		},
		created: function () {
			this.fetchSupportedCommands();
		}
	});
	
});