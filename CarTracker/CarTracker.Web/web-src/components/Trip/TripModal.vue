<template>
    <app-modal title="Trip Details" ref="modal" width="1200px">
        <div class="row py-3" v-if="tripId != -1">
            <div class="col-md-5">
                <app-trip-details :tripId="tripId"></app-trip-details>
            </div>
            <div class="col-md-7">
                <app-trip-map :tripId="tripId"></app-trip-map>
            </div>
        </div>  
        <button slot="footer" type="button" class="btn btn-primary" v-on:click="viewDetails">View Details</button>
    </app-modal>
</template>

<script>
    import System from "services/System.js"
    import Links from "services/Links.js"
    import Modal from "components/Common/Modal.vue"
    import TripDetails from "components/Trip/TripDetails.vue"
    import TripMap from "components/Trip/TripMap.vue"

    export default {
        components: {
            "app-modal": Modal,
            "app-trip-details": TripDetails,
            "app-trip-map": TripMap
        },
        data: function () {
            return {
                tripId: -1
            }
        },
        methods: {
            viewDetails: function () {
                Links.navigateTo(Links.viewTrip(this.tripId));
            }
        },
        created: function () {
            System.events.$on("tripModal:show", function (tripId) {
                this.tripId = tripId;
                this.$refs.modal.open();
            }.bind(this));
        }
    }

</script>