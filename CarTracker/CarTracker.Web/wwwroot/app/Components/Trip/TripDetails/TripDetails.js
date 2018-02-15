"use strict";

define("Components/Trip/TripDetails/TripDetails", 
		["moment", "Service/system", "Service/util", "Service/applicationProxy", "Service/navigation", 
         "AMD/text!Components/Trip/TripDetails/TripDetails.html"],
	function (moment, system, util, proxy, navigation, template) {
	
	return Vue.component("app-trip-details", {
		data: function() {
			return {
				name: "",
				carId: -1,
				startDate: moment(),
				endDate: moment(),
				averageSpeed: 0,
				maximumSpeed: 0,
				averageEngineRpm: 0,
				maxEngineRpm: 0,
				distanceTraveled: 0,
				idleTime: 0,
                status: "",
                startingPlaceName: "",
				destinationPlaceName: ""
			}
		},	
		props: {
			tripId: {
				type: Number,
				required: true
			}
		},
		computed: {
			tripLength: function () {
				var msDiff = this.endDate.diff(this.startDate);
				return moment.duration(msDiff, "ms");
			}
				
		},
		template: template,
		methods: {
			fetch: function () {
				proxy.trip.get(this.tripId).then(function (data) {
                    this.update(data);
				        system.showAlert("Successfully loaded trip!", "success");
				    }.bind(this),
				function (error) {
					alert("error fetching trip!");
				})
			},
			save: function () {
				proxy.trip.update(this.toTripObject()).then(function (data) {
					self.update(data);
				}.bind(this));
			},
			update: function(trip) {
		        this.name = trip.name;
		        this.carId = trip.carId;
		        this.startDate = moment(trip.startDate);
		        this.endDate = moment(trip.endDate);
		        this.averageSpeed = trip.averageSpeed;
		        this.maximumSpeed = trip.maximumSpeed;
		        this.averageEngineRpm = trip.averageEngineRpm;
		        this.maxEngineRpm = trip.maxEngineRpm;
		        this.distanceTraveled = trip.distanceTraveled;
		        this.idleTime = moment.duration(trip.idleTime);
		        this.status = trip.status;
		        this.startingPlaceName = (trip.startingPlace || {}).name || "";
			    this.destinationPlaceName = (trip.destinationPlace || {}).name || "";
			},	
			/** TODO: When saving probaly shouldn't save all of the data. especially the dates. could overwrite */
			toTripObject: function () { 
				return {
					id: this.tripId,				
					name: this.name,
					carId: this.carId,
					startDate: this.startDate.toDate().getTime(),
					endDate: this.endDate.toDate().getTime(),
					averageSpeed: this.averageSpeed,
					maximumSpeed: this.maximumSpeed,
                    averageEngineRpm: this.averageEngineRpm,
                    maxEngineRpm: this.maxEngineRpm,
					distanceTraveled: this.distanceTraveled,
					idleTime: this.idleTime.asMilliseconds(),
					status: this.status					
				};
			},
			refresh: function () {
				this.fetch();
			},
			process: function () {
				proxy.trip.process(this.tripId).then(function (processedTrip) {
					this.update(processedTrip);
				}.bind(this));
			}
				
		},
		created: function () {
			this.fetch();
		}
	});
	
});