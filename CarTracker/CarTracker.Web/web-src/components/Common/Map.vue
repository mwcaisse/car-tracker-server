<template>
    <div :style="styles"></div>
</template>

<script>
    import System from "services/System.js"
    import * as Util from "services/Util.js";

    var googleMapsApiKey = $("#googleMapsApiKey").val();

    export default {
        data: function () {
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
        methods: {
            addMarker: function (position) {
                var marker = new google.maps.Marker({
                    position: position,
                    map: this.map
                });
                this.markers.push(marker);
            },
            clearMarkers: function () {
                $.each(this.markers, function (ind, marker) {
                    marker.setMap(null); // remove marker from the map by setting its position to null
                });
                this.markers = [];
            },
            setCenter: function (position) {
                this.map.panTo(position);
            },
            gmapsLoaded: function () {
                this.map = new google.maps.Map(this.$el, {
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

                System.events.$on("map:addMarker", function (position) {
                    this.addMarker(position);
                }.bind(this));

                System.events.$on("map:clearMarkers", function () {
                    this.clearMarkers();
                }.bind(this));

                System.events.$on("map:setCenter", function (position) {
                    this.setCenter(position);
                }.bind(this));
            }
        },
        mounted: function () {
            //when we are mounted, load the google maps api
            //TODO: We might be able to do this sooner ?
            var callback = "__googleMapsApiOnLoadCallback";
            var gapiurl = `https://maps.googleapis.com/maps/api/js?key=${googleMapsApiKey}&callback=${callback}`;
            Util.jsonp(gapiurl, callback).then(function () {
                this.gmapsLoaded()
            }.bind(this));
        }
    }
</script>