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
                    currentSort: { propertyId: "CreateDate", ascending: false }
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
                update: function(data) {
                    this.logs = data.data;
                    this.totalItems = data.total;
                },
                refresh: function() {
                    this.fetch();
                }
            },
            created: function() {
                this.fetch();
            }
        });

    });