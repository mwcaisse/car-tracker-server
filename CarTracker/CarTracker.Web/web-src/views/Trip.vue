<template>
    <div>
        <div class="row py-3">
            <div class="col-md-5">
                <app-trip-details :tripId="tripId"></app-trip-details>
            </div>
            <div class="col-md-7">
                <app-trip-map :tripId="tripId"></app-trip-map>
            </div>
        </div>
        <div class="row py-3">
            <div class="col-md-12">
                <app-reader-log-grid :startDate="tripStartDate" :endDate="tripEndDate"></app-reader-log-grid>
            </div>
        </div>
        <div class="row py-3">
            <div class="col-md-12">
                <app-trip-speed-chart :tripId="tripId"></app-trip-speed-chart>
            </div>
        </div>
        <div class="row py-3">
            <div class="col-md-12">
                <app-trip-engine-chart :tripId="tripId"></app-trip-engine-chart>
            </div>
        </div>
        <div class="row py-3">
            <div class="col-md-12">
                <app-trip-throttle-chart :tripId="tripId"></app-trip-throttle-chart>
            </div>
        </div>
        <div class="row py-3">
            <div class="col-md-12">
                <app-trip-maf-chart :tripId="tripId"></app-trip-maf-chart>
            </div>
        </div>
        <div class="row py-3">
            <div class="col-md-12">
                <app-trip-temperature-chart :tripId="tripId"></app-trip-temperature-chart>
            </div>
        </div>
    </div>
</template>

<script>
    import System from "services/System.js"
    import { getURLParameter } from "services/Util.js"
    import Links from "services/Links.js"
    import { TripService } from "services/ApplicationProxy.js"

    import TripDetails from "components/Trip/TripDetails.vue"
    import TripMap from "components/Trip/TripMap.vue"
    import TripSpeedChart from "components/Trip/TripChart/TripSpeedChart.vue"
    import TripEngineChart from "components/Trip/TripChart/TripEngineChart.vue"
    import TripThrottleChart from "components/Trip/TripChart/TripThrottleChart.vue"
    import TripMAFChart from "components/Trip/TripChart/TripMAFChart.vue"
    import TripTemperatureChart from "components/Trip/TripChart/TripTemperatureChart.vue"
    import ReaderLogGrid from "components/ReaderLog/ReaderLogGrid.vue"

    var tripId = parseInt(getURLParameter("tripId", 92), 10);	

    export default {
        data: function () {
            return {
                tripId: tripId,
                tripStartDate: null,
                tripEndDate: null
            }
        },
        methods: {
            fetchTrip: function () {
                return TripService.get(this.tripId).then(function (data) {
                    this.tripStartDate = moment(data.startDate).subtract(5, "minutes").toDate();
                    this.tripEndDate = moment(data.endDate).add(5, "minutes").toDate();
                }.bind(this), function (error) {
                    System.showAlert(error, "error");
                });
            }
        },
        created: function () {
            this.fetchTrip();
        },
        mounted: function () {
            Links.setActiveNavigation("Trip");
        },
        components: {
            "app-trip-details": TripDetails,
            "app-trip-map": TripMap,
            "app-trip-speed-chart": TripSpeedChart,
            "app-trip-engine-chart": TripEngineChart,
            "app-trip-throttle-chart": TripThrottleChart,
            "app-trip-maf-chart": TripMAFChart,
            "app-trip-temperature-chart": TripTemperatureChart,
            "app-reader-log-grid": ReaderLogGrid
        }
    }
</script>