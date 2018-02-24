"use strict";

define("Components/Trip/TripGrid/TripGrid", 
		["moment", "Service/system", "Service/util", "Service/applicationProxy", "Service/navigation", 	
		 "Components/Common/Pager/PagedGridMixin",
		 "AMD/text!Components/Trip/TripGrid/TripGrid.html",
         "AMD/text!Components/Trip/TripGrid/TripRow.html",
         "Components/Common/ColumnHeader/ColumnHeader",
         "Components/Common/Pager/Pager"],
	function (moment, system, util, proxy, navigation, pagedGridMixin, template, tripRowTemplate) {
	
	var tripRow = {
		template: tripRowTemplate,	
		data: function () {
			return {
				id: -1,
				name: "",
				startDate: moment(),
				endDate: moment(),
				status: "",
				distanceTraveled: 0,
				start: "",
				destination: ""
			};
		},
		props: {
			trip: {
				type: Object,
				requred: true
			}
		},
		computed: {
			canProcess: function() {
				return this.status !== system.constants.TRIP_STATUS.PROCESSED;
			},
			rowCss: function () {
				switch (this.status) {
					case system.constants.TRIP_STATUS.NEW:
						return "table-danger";
                    case system.constants.TRIP_STATUS.STARTED:
						return "table-warning";				
                    case system.constants.TRIP_STATUS.FINISHED:
						return "table-info";			
                    case system.constants.TRIP_STATUS.PROCESSED:
						return "";			
					default:
						return "table-danger";				
				}
			},
			viewLink: function () {
				return navigation.viewTripLink(this.id);
			},
			destinationName: function () {
                if (null == this.destinationPlace || typeof this.destinationPlace == "undefined") {
					return "";
				}
                return this.destinationPlace.name;
			}
		},
		methods: {
			process: function () {
				proxy.trip.process(this.id).then(function (processedTrip) {
					this.update(processedTrip);
				}.bind(this));
			},
			update: function (data) {
				this.id = data.id;
				this.name = data.name;
				this.startDate = moment(data.startDate);
				this.endDate = moment(data.endDate);
				this.status = data.status;
				this.distanceTraveled = data.distanceTraveled;
				this.startPlace = data.startPlace;
                this.destinationPlace = data.destinationPlace;
			}
		},
		created: function() {
			this.update(this.trip);
		}
	};
	
	return Vue.component("app-trip-grid", {
		mixins: [pagedGridMixin],	
		components: {
			"app-trip-row": tripRow
		},	
		data: function() {
			return {
				trips: [],
				currentSort: { propertyId: "StartDate", ascending: false}			
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
			fetchTrips: function () {				
				proxy.trip.getAllForCarPaged(this.carId, this.startAt, this.take, this.currentSort).then(function (data) {					
					this.update(data);
				}.bind(this),
				function (error) {
				    system.showAlert(error, "error");
				});
			},
			processAll: function () {
				proxy.trip.processUnprocessed().then(function () {
					this.refresh();
				}.bind(this));
			},
			createTrip: function (data) {
				return new (function() {
					var trip = this;
					
					trip.id = data.id;
					trip.name = data.name;
				})();	
			},	
			update: function (data) {
				this.trips = data.data;	
				this.totalItems = data.total;
			},	
			refresh: function () {
				this.fetchTrips();
			}			
		},
		created: function () {
			this.fetchTrips();
		}
	});
	
});