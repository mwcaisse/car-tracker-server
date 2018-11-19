<template>
    <div>
        <div class="row py-3">
            <div class="col-md-5">
                <app-user-place-grid></app-user-place-grid>
            </div>
            <div class="col-md-7">
                <app-user-place-details></app-user-place-details>
            </div>
        </div>
    </div>
</template>

<script>
    import Links from "services/Links.js"
    import UserPlaceDetails from "components/User/UserPlaceDetails.vue"
    import UserPlaceGrid from "components/User/UserPlaceGrid.vue"

    export default {
        components: {
            "app-user-place-grid": UserPlaceGrid,
            "app-user-place-details": UserPlaceDetails
        },
        data: function () {
            return {
            }
        },      
        created: function () {
            Links.setActiveNavigation("User/CustomPlaces");
        }
    }
</script>