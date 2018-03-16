"use strict";

define("Components/UserPlace/UserPlaceDetails/UserPlaceDetails",
    ["moment", "Service/system", "Service/util", "Service/applicationProxy", "Service/navigation",
        "AMD/text!Components/UserPlace/UserPlaceDetails/UserPlaceDetails.html"],

    function (moment, system, util, proxy, navigation, template) {
        return Vue.component("app-user-place-details", {
            data: function () {
                return {
                    userPlaceId: -1,
                    name: "",
                    latitude: 0,
                    longitude: 0
                }
            },
            template: template,
            methods: {
                fetchKey: function () {
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
                    if (this.userPlaceId < 0) {
                        func = proxy.userPlaces.create;
                    }
                    else {
                        func = proxy.userPlaces.update;
                    }

                    func(this.createModel()).then(function () {
                        this.$emit("userPlace:updated");
                        this.$refs.modal.close();
                    }.bind(this),
                        function (error) {
                            system.showAlert(error, "error");
                        });
                },
                refresh: function () {
                    this.fetchKey();
                },
                generateKey: function () {
                    this.key = util.generatkeUuid();
                }
            },
            created: function () {
                system.bus.$on("userPlace:create", function () {
                    this.$refs.modal.open();
                }.bind(this));

                system.bus.$on("userPlace:edit", function (keyId) {
                    this.userRegistrationKeyId = keyId;
                    this.fetchKey();
                    this.$refs.modal.open();
                }.bind(this));
            }
        });

    });