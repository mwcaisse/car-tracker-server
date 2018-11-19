<template>
    <div>
        <div class="row">
            <div class="col-md-12">
                <app-car-details :carId="carId"></app-car-details>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <app-trip-grid :carId="carId"></app-trip-grid>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <app-car-maintenance-grid :carId="carId"></app-car-maintenance-grid>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <app-car-supported-commands :carId="carId"></app-car-supported-commands>
            </div>
        </div>
    </div>
</template>

<script>
    import System from "services/System.js"
    import * as Util from "services/Util.js"
    import Links from "services/Links.js"

    import CarDetails from "components/Car/CarDetails.vue"
    import CarMaintenanceGrid from "components/Car/CarMaintenanceGrid.vue"
    import CarSupportedCommands from "components/Car/CarSupportedCommands.vue"
    import TripGrid from "components/Trip/TripGrid.vue"

    var carId = parseInt(Util.getURLParameter("carId", -1), 10);	

    export default {
        data: function () {
            return {
                carId: carId
            }
        },
        components: {
            "app-car-details": CarDetails,
            "app-car-maintenance-grid": CarMaintenanceGrid,
            "app-car-supported-commands": CarSupportedCommands,
            "app-trip-grid": TripGrid
        },
        created: function () {
            Links.setActiveNavigation("Car");
        }
    }
</script>