"use strict";

define("Components/Trip/TripPossiblePlaces/TripPossiblePlaces", 
        ["moment",
         "Service/system",
         "Service/applicationProxy", 	
		 "Components/Common/Pager/PagedGridMixin",
		 "AMD/text!Components/Trip/TripPossiblePlaces/TripPossiblePlaces.html",
		 "AMD/text!Components/Trip/TripPossiblePlaces/TripPossiblePlacesRow.html",
         "Components/Common/ColumnHeader/ColumnHeader",
         "Components/Common/Pager/Pager"],
	function (moment, system, proxy, pagedGridMixin, template, rowTemplate) {
	
	var placeRow = {
			template: rowTemplate,	
			data: function () {
				return {
					id: -1,
					tripId: -1,
					placeId: -1,
					placeType: "",
					distance: 0,
					placeName: ""
				};
			},
			props: {
				possiblePlace: {
					type: Object,
					requred: true
				}
			},
			computed: {
				placeTypeDisplay: function() {
				    return system.enums.TripPossiblePlaceType[this.placeType] || "";
				}
	        },
			methods: {
				setPlace: function () {
					if (this.placeType === system.constants.TRIP_POSSIBLE_PLACE_TYPE.START) {
						proxy.trip.setStartingPlace(this.tripId, this.placeId);
					}
					else {
						proxy.trip.setDestinationPlace(this.tripId, this.placeId);
					}
				},
				update: function (data) {
					this.id = data.id;
					this.tripId = data.tripId;
					this.placeId = data.placeId;
					this.placeType = data.placeType;
					this.distance = data.distance;
					this.placeName = data.place.name;
				}
			},
			created: function() {
				this.update(this.possiblePlace);
			}
		};
		
	
	return Vue.component("app-trip-possible-places", {
		mixins: [pagedGridMixin],
		components: {
			"app-trip-possible-places-row": placeRow
		},
		data: function() {
			return {
				possiblePlaces: [],
				currentSort: { }			
			}
		},	
		props: {
			tripId: {
				type: Number,
				required: true
			},
			type: {
				type: String,
				required: true
			}
		},
		computed: {
			typeDisplay: function () {
				if (this.type === "START") {
					return "Starting Points";
				}
				else {
					return "Destinations";
				}
			}
		},
		template: template,
		methods: {
			fetchPossiblePlaces: function () {				
				proxy.tripPossiblePlaces.getForTrip(this.tripId, this.type, this.startAt, this.take, this.currentSort).then(function (data) {
					this.update(data);
				}.bind(this),
				function (error) {
				    system.showAlert(error, "error");
				})
			},		
			update: function (data) {
				this.possiblePlaces = data.data;	
				this.totalItems = data.total;
			},	
			refresh: function () {
				this.fetchPossiblePlaces();
			}			
		},
		created: function () {
			this.fetchPossiblePlaces();
		}
	});
	
});