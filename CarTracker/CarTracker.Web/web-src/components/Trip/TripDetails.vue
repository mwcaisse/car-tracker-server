<template>
    <div class="card">
        <div class="card-header">
            Details
            <span class="float-right">
                <i class="fa fa-hdd-o action-icon" aria-hidden="true" v-on:click="process" v-tooltip title="Process Trip"></i>
                &nbsp;
                <i class="fa fa-floppy-o action-icon" aria-hidden="true" v-on:click="save" v-tooltip title="Save"></i>
                &nbsp;
                <i class="fa fa-refresh action-icon" aria-hidden="true" v-on:click="refresh" v-tooltip title="Refresh"></i>
            </span>
        </div>
        <div class="card-body">
            <form>
                <div class="form-group row">
                    <label class="col-md-5 col-form-label">Name</label>
                    <div class="col-md-7">
                        <input type="text" class="form-control" placeholder="Name" v-model="name" />
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-5 col-form-label">Status</label>
                    <div class="col-md-7">
                        <p class="form-control-plaintext"> {{ status | friendlyConstant("TRIP_STATUS") }}</p>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-5 col-form-label">Start Date</label>
                    <div class="col-md-7">
                        <p class="form-control-plaintext"> {{ startDate | formatDateTime }}</p>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-5 col-form-label">End Date</label>
                    <div class="col-md-7">
                        <p class="form-control-plaintext"> {{ endDate | formatDateTime }}</p>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-5 col-form-label">Trip Length</label>
                    <div class="col-md-7">
                        <p class="form-control-plaintext"> {{ tripLength | formatDuration }}</p>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-5 col-form-label">Average Speed</label>
                    <div class="col-md-7">
                        <p class="form-control-plaintext"> {{ averageSpeed | kmToMi | round(2) }} mph</p>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-5 col-form-label">Maximum Speed</label>
                    <div class="col-md-7">
                        <p class="form-control-plaintext"> {{ maximumSpeed | kmToMi | round(2) }} mph</p>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-5 col-form-label">Average Engine Speed</label>
                    <div class="col-md-7">
                        <p class="form-control-plaintext"> {{ averageEngineRpm | round(2) }} RPM</p>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-5 col-form-label">Max Engine Speed</label>
                    <div class="col-md-7">
                        <p class="form-control-plaintext"> {{ maxEngineRpm | round(2) }} RPM</p>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-5 col-form-label">Distance Traveled</label>
                    <div class="col-md-7">
                        <p class="form-control-plaintext"> {{ distanceTraveled | kmToMi | round(2) }} mi</p>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-5 col-form-label">Idle Time</label>
                    <div class="col-md-7">
                        <p class="form-control-plaintext"> {{ idleTime | formatDuration }}</p>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-5 col-form-label">Start</label>
                    <div class="col-md-7">
                        <p class="form-control-plaintext">
                            <span v-if="startingPlaceName.length > 0">{{ startingPlaceName }}&nbsp;</span>
                            <i class="fa fa-pencil action-icon" aria-hidden="true" v-on:click="selectPossiblePlacesStart" v-tooltip title="Select Starting Place"></i>
                        </p>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-5 col-form-label">Destination</label>
                    <div class="col-md-7">
                        <p class="form-control-plaintext">
                            <span v-if="destinationPlaceName.length > 0">{{ destinationPlaceName }}&nbsp;</span>
                            <i class="fa fa-pencil action-icon" aria-hidden="true" v-on:click="selectPossiblePlacesDestination" v-tooltip title="Select Destination Place"></i>
                        </p>
                    </div>
                </div>
            </form>
        </div>
        <app-trip-possible-places :tripId="tripId" v-on:trip:possible-place:selected="placeSelected"></app-trip-possible-places>
    </div>
</template>

<script>

    import System from "services/System.js"
    import * as Constants from "services/Constants.js"
    import { TripService } from "services/ApplicationProxy.js"

    import TripPossiblePlaces from "components/Trip/TripPossiblePlaces.vue"

    export default {
        data: function () {
            return {
                name: "",
                carId: -1,
                startDate: moment(),
                endDate: moment(),
                averageSpeed: 0,
                maximumSpeed: 0,
                averageEngineRpm: 0,
                maxEngineRpm: 0,
                distanceTraveled: 0,
                idleTime: 0,
                status: "",
                startingPlaceName: "",
                destinationPlaceName: ""
            }
        },
        props: {
            tripId: {
                type: Number,
                required: true
            }
        },
        computed: {
            tripLength: function () {
                var msDiff = this.endDate.diff(this.startDate);
                return moment.duration(msDiff, "ms");
            }

        },
        methods: {
            fetch: function () {
                TripService.get(this.tripId).then(function (data) {
                    this.update(data);
                }.bind(this),
                    function (error) {
                        System.showAlert(error, "error");
                    });
            },
            save: function () {
                TripService.update(this.toTripObject()).then(function (data) {
                    self.update(data);
                }.bind(this));
            },
            update: function (trip) {
                this.name = trip.name;
                this.carId = trip.carId;
                this.startDate = moment(trip.startDate);
                this.endDate = moment(trip.endDate);
                this.averageSpeed = trip.averageSpeed;
                this.maximumSpeed = trip.maximumSpeed;
                this.averageEngineRpm = trip.averageEngineRpm;
                this.maxEngineRpm = trip.maxEngineRpm;
                this.distanceTraveled = trip.distanceTraveled;
                this.idleTime = moment.duration(trip.idleTime);
                this.status = trip.status;
                this.startingPlaceName = (trip.startingPlace || {}).name || "";
                this.destinationPlaceName = (trip.destinationPlace || {}).name || "";
            },
            /** TODO: When saving probaly shouldn't save all of the data. especially the dates. could overwrite */
            toTripObject: function () {
                return {
                    id: this.tripId,
                    name: this.name,
                    carId: this.carId,
                    startDate: this.startDate.toDate().getTime(),
                    endDate: this.endDate.toDate().getTime(),
                    averageSpeed: this.averageSpeed,
                    maximumSpeed: this.maximumSpeed,
                    averageEngineRpm: this.averageEngineRpm,
                    maxEngineRpm: this.maxEngineRpm,
                    distanceTraveled: this.distanceTraveled,
                    idleTime: this.idleTime.asMilliseconds(),
                    status: this.status
                };
            },
            refresh: function () {
                this.fetch();
            },
            process: function () {
                TripService.process(this.tripId).then(function (processedTrip) {
                    this.update(processedTrip);
                }.bind(this));
            },
            selectPossiblePlacesStart: function () {
                System.events.$emit("trip:possible-place:show-modal", Constants.TRIP_POSSIBLE_PLACE_TYPE.START);
            },
            selectPossiblePlacesDestination: function () {
                System.events.$emit("trip:possible-place:show-modal", Constants.TRIP_POSSIBLE_PLACE_TYPE.DESTINATION);
            },
            placeSelected: function (data) {
                if (data.placeType === Constants.TRIP_POSSIBLE_PLACE_TYPE.START) {
                    this.startingPlaceName = data.selectedPlace.place.name;
                } else {
                    this.destinationPlaceName = data.selectedPlace.place.name;
                }
            }
        },
        created: function () {
            this.fetch();
        },
        components: {
            "app-trip-possible-places": TripPossiblePlaces
        }
    }
</script>