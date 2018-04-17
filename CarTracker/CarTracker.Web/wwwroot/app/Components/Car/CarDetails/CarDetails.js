"use strict";

define("Components/Car/CarDetails/CarDetails", 
		["moment", "Service/system", "Service/util", "Service/applicationProxy", "Service/navigation", 
         "AMD/text!Components/Car/CarDetails/CarDetails.html"],
	function (moment, system, util, proxy, navigation, template) {
	
	return Vue.component("app-car-details", {
		data: function() {
			return {
				name: "",
				vin: "",
                mileage: 0
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
			fetchCar: function () {
				proxy.car.get(this.carId).then(function (data) {
					this.update(data);
				}.bind(this),
				function (error) {
				    system.showAlert(error, "error");
				});
			},
			update: function (car) {
				this.name = car.name;
                this.vin = car.vin;
			    this.mileage = car.mileage;
			},
			save: function () {
				proxy.car.update(this.toCarObject()).then(function (data) {
					this.update(data);
				}.bind(this),
				function (error) {
				    system.showAlert(error, "error");
				}.bind(this));
			},
			toCarObject: function () {
				return {
					id: this.carId,
					name: this.name,
					vin: this.vin
				};
			},
			refresh: function () {
				this.fetchCar();
			}				
		},
		created: function () {
			this.fetchCar();
		}
	});
	
});