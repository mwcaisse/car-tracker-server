<template>
    <div>
        <div class="card">
            <div class="card-header">
                {{ title }}
                <span class="float-right">
                    <i class="fa fa-ban action-icon" aria-hidden="true" v-on:click="clear" v-tooltip title="Discard"></i>
                    &nbsp;
                    <i class="fa fa-save action-icon" aria-hidden="true" v-on:click="save" v-tooltip title="Save"></i>
                </span>
            </div>
            <div class="card-body">
                <form>
                    <div class="form-group row">
                        <label class="col-md-2 col-form-label">Name</label>
                        <div class="col-md-10">
                            <input type="text" class="form-control" placeholder="Enter Name" v-model="name" />
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="col-md-12">
                            <app-map v-on:map:loaded="mapInitialized" v-on:map:click="mapClicked"></app-map>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script>
    import System from "services/System.js";
    import { UserPlaceService } from "services/ApplicationProxy.js"

    import Map from "components/Common/Map.vue"

    export default {
        components: {
            "app-man": Map
        },
        data: function () {
            return {
                map: {},
                userPlaceId: -1,
                name: "",
                latitude: 0,
                longitude: 0
            }
        },
        computed: {
            placeCoordinate: function () {
                return {
                    latitude: this.latitude,
                    longitude: this.longitude
                };
            },
            editing: function () {
                return this.userPlaceId !== -1;
            },
            title: function () {
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
                System.events.$emit("map:clearMarkers");
                if (position.lat && position.lng) {
                    System.events.$emit("map:addMarker", position);
                    System.events.$emit("map:setCenter", position);
                }
            }
        },
        methods: {
            fetch: function () {
                UserPlaceService.get(this.userPlaceId).then(function (data) {
                    this.update(data);
                }.bind(this), function (error) {
                    System.showAlert(error, "error");
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
                    func = UserPlaceService.update;
                }
                else {
                    func = UserPlaceService.create;
                }

                func(this.createModel()).then(function (results) {
                    this.update(results);
                    System.events.$emit("userPlace:updated");
                }.bind(this), function (error) {
                    System.showAlert(error, "error");
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
            System.events.$on("userPlace:create", function () {
                this.userPlaceId = -1;
                this.clear();
            }.bind(this));

            System.events.$on("userPlace:edit", function (userPlaceId) {
                this.userPlaceId = userPlaceId;
                this.fetch();
            }.bind(this));
        }
    }
</script>