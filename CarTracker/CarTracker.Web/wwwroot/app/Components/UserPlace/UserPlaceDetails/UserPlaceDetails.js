"use strict";

define("Components/UserPlace/UserPlaceDetails/UserPlaceDetails",
    ["AMD/googlemaps!", "moment", "Service/system", "Service/util", "Service/applicationProxy", "Service/navigation",
        "AMD/text!Components/UserPlace/UserPlaceDetails/UserPlaceDetails.html",
        "Components/Common/Map/Map"],

    function (gmaps, moment, system, util, proxy, navigation, template) {
        return Vue.component("app-user-place-details", {
            data: function () {
                return {
                    map: {},
                    userPlaceId: -1,
                    name: "",
                    latitude: 0,
                    longitude: 0
                }
            },
            template: template,
            computed: {
                placeCoordinate: function() {
                    return {
                        latitude: this.latitude,
                        longitude: this.longitude
                    };
                },
                editing: function() {
                    return this.userPlaceId !== -1;
                },
                title: function() {
                    if (!this.editing) {
                        return "Create Place";
                    }
                    else {
                        return "Edit Place";
                    }
                }
            },
            watch: {
                placeCoordinate: function (newCoordinate) {
                    var position = {
                        lat: newCoordinate.latitude,
                        lng: newCoordinate.longitude
                    }
                    system.bus.$emit("map:clearMarkers");
                    if (position.lat && position.lng) {
                        system.bus.$emit("map:addMarker", position);
                        system.bus.$emit("map:setCenter", position);
                    }
                }  
            },
            methods: {
                fetch: function () {
                    proxy.userPlaces.get(this.userPlaceId).then(function(data) {
                            this.update(data);
                        }.bind(this),
                        function(error) {
                            system.showAlert(error, "error");
                        });
                },
                update: function (place) {
                    this.userPlaceId = place.userPlaceId;
                    this.name = place.name;
                    this.latitude = place.latitude;
                    this.longitude = place.longitude;
                },
                clear: function () {
                    this.userPlaceId = -1;
                    this.name = "";
                    this.latitude = 0;
                    this.longitude = 0;
                },
                createModel: function () {
                    return {
                        userPlaceId: this.userPlaceId,
                        name: this.name,
                        latitude: this.latitude,
                        longitude: this.longitude
                    };
                },
                save: function () {
                    var func;
                    if (this.editing) {
                        func = proxy.userPlaces.update;
                    }
                    else {
                        func = proxy.userPlaces.create;
                    }

                    func(this.createModel()).then(function (results) {
                        this.update(results);
                        system.bus.$emit("userPlace:updated");
                    }.bind(this), function (error) {
                        system.showAlert(error, "error");
                    });
                },
                refresh: function () {
                    this.fetch();
                },
                mapInitialized: function (map) {
                    this.map = map;
                },
                mapClicked: function (param) {
                    var position = param.event.latLng;
                    this.latitude = position.lat();
                    this.longitude = position.lng();
                }
            },
            created: function () {
                system.bus.$on("userPlace:create", function () {
                    this.userPlaceId = -1;
                    this.clear();
                }.bind(this));

                system.bus.$on("userPlace:edit", function (userPlaceId) {
                    this.userPlaceId = userPlaceId;
                    this.fetch();
                }.bind(this));
            }
        });

    });