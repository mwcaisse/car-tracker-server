"use strict";

define("Components/Common/Map/Map", 
        ["AMD/googlemaps!", "Service/system",
		 "AMD/text!Components/Common/Map/Map.html"],
	function (gmaps, system, template) {
	
	return Vue.component("app-map", {
		data: function() {
			return {	
                map: {},
                makers: []
			}
		},	
		computed: {
			styles: function () {
				return {
					width: this.width + "px",
					height: this.height + "px"
				};
			}
		},	
		props: {
			width: {
				type: Number,
				default: 600,
				required: false
			},
			height: {
				type: Number,
				default: 600,
				required: false
			},
			zoom: {
				type: Number,
				default: 12,
				required: false
			},
			center: {
				type: Object,
				required: false,
				default: function () {
					return {
						lat: 42.710291, 
						lng: -71.442039
					};
				}
			}
		},		
		template: template,
		methods: {			
            addMarker: function (position) {
                var marker = new gmaps.Marker({
                    position: position,
                    map: this.map
                });
                this.markers.push(marker);
            },
            clearMarkers: function() {
                $.each(this.markers, function(ind, marker) {
                    marker.setMap(null); // remove marker from the map by setting its position to null
                });
                this.markers = [];
            },
            setCenter: function (position) {
                this.map.panTo(position);
            }
		},
		mounted: function () {
			this.map = new gmaps.Map(this.$el, {
				zoom: this.zoom,
				center: this.center
			});
			
            this.$emit("map:loaded", this.map);		

            this.map.addListener("click", function (event) {
                this.$emit("map:click",
                {
                    map: this.map,
                    event: event
                });
            }.bind(this));

            system.bus.$on("map:addMarker", function (position) {
                this.addMarker(position);
            }.bind(this));

            system.bus.$on("map:clearMarkers", function () {
                this.clearMarkers();
            }.bind(this));

            system.bus.$on("map:setCenter", function (position) {
                this.setCenter(position);
            }.bind(this));
        }
	});
	
});