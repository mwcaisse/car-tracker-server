<template>
    <div>
        <h3>Welcome to Car Tracker</h3>

        <div class="row">
            <div class="col-md-12">
                <p>Please select a car below to get started.</p>
            </div>
        </div>

        <app-car-selection></app-car-selection>
    </div>
</template>

<script>
    import Links from "services/Links.js"    
    import CarSelection from "components/Car/CarSelection.vue"

    export default {
        data: function () {
            return {
            }
        },
        components: {
            "app-car-selection": CarSelection
        },
        created: function () {
            Links.setActiveNavigation("Home");
        }
    }
</script>