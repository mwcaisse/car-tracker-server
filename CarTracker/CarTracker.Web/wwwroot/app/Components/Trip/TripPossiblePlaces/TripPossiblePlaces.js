"use strict";

define("Components/Trip/TripPossiblePlaces/TripPossiblePlaces", 
        ["moment",
         "Service/system",
         "Service/applicationProxy", 	
		 "Components/Common/Pager/PagedGridMixin",
		 "AMD/text!Components/Trip/TripPossiblePlaces/TripPossiblePlaces.html",
		 "AMD/text!Components/Trip/TripPossiblePlaces/TripPossiblePlacesRow.html",
         "Components/Common/ColumnHeader/ColumnHeader",
         "Components/Common/Pager/Pager",
         "Components/Common/Modal/Modal"],
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

                    this.$emit("trip:possible-place:selected", this.possiblePlace);			
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
                currentSort: {},
                type: ""
			}
		},	
		props: {
			tripId: {
				type: Number,
				required: true
			}
		},
		computed: {
			typeDisplay: function () {
				if (this.type === system.constants.TRIP_POSSIBLE_PLACE_TYPE.START) {
					return "Starting Points";
				}
				else {
					return "Destinations";
				}
            },
            title: function() {
                return "Possible " + this.typeDisplay;
            }
		},
		template: template,
		methods: {
            fetchPossiblePlaces: function () {
                var type = system.enums.TripPossiblePlaceType[this.type];
                return proxy.tripPossiblePlaces.getForTrip(this.tripId, type, this.startAt,
                    this.take, this.currentSort).then(function (data) {

                    this.update(data);
                    return data;
				}.bind(this),
				function (error) {
                    system.showAlert(error, "error");
                    return error;
				});
			},		
			update: function (data) {
				this.possiblePlaces = data.data;	
				this.totalItems = data.total;
			},	
			refresh: function () {
				this.fetchPossiblePlaces();
            },	
            placeSelected: function (place) {
                this.$emit("trip:possible-place:selected", {
                    selectedPlace: place,
                    placeType: this.type
                });
                this.$refs.modal.close();
            }
		},
		created: function () {
            system.bus.$on("trip:possible-place:show-modal", function (type) {
                this.currentPage = 1;
                this.type = type;
                this.fetchPossiblePlaces().then(function () {
                    this.$refs.modal.open();
                }.bind(this));  
                
            }.bind(this));
		}
	});
	
});