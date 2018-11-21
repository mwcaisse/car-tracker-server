<template>
    <div class="card">
        <div class="card-header">
            Route Map
            <span class="float-right">
                <i class="fa fa-refresh action-icon" aria-hidden="true" v-on:click="refresh" v-tooltip title="Refresh"></i>
            </span>
        </div>
        <div class="card-body">
            <app-map v-on:map:loaded="mapInitialized"></app-map>
        </div>
    </div>
</template>

<script>
    import System from "services/System.js"
    import { ReadingService } from  "services/ApplicationProxy.js"
    import Map from "components/Common/Map.vue"

    export default {
        data: function () {
            return {
                map: {},
                routePathes: [],
                markers: []
            }
        },
        props: {
            tripId: {
                type: Number,
                required: true
            }
        },
        watch: {
            tripId: function (val) {
                this.clearMap();
                this.refresh()
            }
        },
        methods: {
            clearMap: function () {
                //clear any existing route paths and/or markers from the map
                if (this.routePathes.length > 0) {
                    $.each(this.routePathes, function (ind, elm) {
                        elm.setMap(null);
                    });
                }
                if (this.markers.length > 0) {
                    $.each(this.markers, function (ind, elm) {
                        elm.setMap(null);
                    });
                }
            },
            fetch: function () {
                ReadingService.getAllForTrip(this.tripId).then(function (data) {                    
                    this.clearMap();
                    
                    var coords = $.map(data, function (elm) {
                        if (elm.longitude === 0 && elm.latitude === 0) {
                            return null; // ignore any coordinates that have 0 as lat an long (data issue)
                        }
                        return {
                            speed: elm.speed,
                            coords: {
                                lat: elm.latitude,
                                lng: elm.longitude
                            }
                        };
                    });
                    
                    var mapBounds = new google.maps.LatLngBounds();
                    $.each(coords, function (ind, elm) {
                        mapBounds.extend(elm.coords);
                    });

                    for (var i = 0; i < coords.length - 1; i++) {
                        var color;

                        if (coords[i].speed < 60) {
                            color = "#FF0000";
                        }
                        else if (coords[i].speed < 100) {
                            color = "#E59400";
                        }
                        else {
                            color = "#00FF00";
                        }
                        var subPath = new google.maps.Polyline({
                            path: [coords[i].coords, coords[i + 1].coords],
                            geodesic: true,
                            strokeColor: color,
                            strokeOpacity: 1.0,
                            strokeWeight: 2.0
                        });

                        subPath.setMap(this.map);

                        this.routePathes.push(subPath);
                    }

                    this.map.fitBounds(mapBounds);

                    if (coords.length > 1) {
                        var endMarker = new google.maps.Marker({
                            position: coords[coords.length - 1].coords,
                            map: this.map,
                            title: "End"
                        });

                        this.markers.push(endMarker);
                    }
                }.bind(this),
                    function (error) {
                        System.showAlert(error, "error");
                    });
            },
            refresh: function () {
                this.fetch();
            },
            mapInitialized: function (map) {
                this.map = map;
                this.fetch();
            }
        },
        components: {
            "app-map": Map
        }
    }
</script>