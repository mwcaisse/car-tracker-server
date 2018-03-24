"use strict";

define("Components/UserPlace/UserPlaceGrid/UserPlaceGrid",
    ["moment", "Service/system", "Service/util", "Service/applicationProxy", "Service/navigation",
        "Components/Common/Pager/PagedGridMixin",
        "AMD/text!Components/UserPlace/UserPlaceGrid/UserPlaceGrid.html",
        "Components/Common/ColumnHeader/ColumnHeader",
        "Components/Common/Pager/Pager"],
    function(moment, system, util, proxy, navigation, pagedGridMixin, template) {


        return Vue.component("app-user-place-grid", {
            mixins: [pagedGridMixin],
            data: function() {
                return {
                    places: [],
                    currentSort: { propertyId: "CreateDate", ascending: true }
                }
            },
            template: template,
            methods: {
                fetch: function() {
                    proxy.userPlaces.getMinePaged(this.startAt, this.take, this.currentSort)
                        .then(function (data) {
                            this.update(data);
                        }.bind(this), function(error) {
                            system.showAlert(error, "error");
                        });
                },
                update: function (data) {
                    this.places = data.data;
                    this.totalItems = data.total;
                },
                refresh: function() {
                    this.fetch();
                },
                viewUserPlace: function(place) {
                    system.bus.$emit("userPlace:edit", place.userPlaceId);
                },
                deleteUserPlace: function(place) {
                    proxy.userPlaces.delete(place.userPlaceId).then(function() {
                        var ind = this.places.indexOf(place);
                        this.places.splice(ind, 1);
                    }.bind(this), function (error) {
                        system.showAlert(error, "error");
                    });
                }
            },
            created: function() {
                this.fetch();

                system.bus.$on("userPlace:updated", function () {
                    this.fetch();
                }.bind(this));
            }
        });

    });