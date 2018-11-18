<template>
    <app-collapsible-card :title="name">
        <span slot="header-icons">
            <i class="fa fa-refresh action-icon" aria-hidden="true" v-on:click="refresh" v-tooltip title="Refresh"></i>
        </span>
        <div v-highcharts="chartOptions"></div>
    </app-collapsible-card>
</template>

<script>
    import System from "services/System.js"
    import { ReadingService } from "services/ApplicationProxy.js"
    import CollapsibleCard from "components/Common/CollapsibleCard.vue";

    export default {
        data: function () {
            return {
                name: "Trip Chart",
                readings: []
            }
        },
        props: {
            tripId: {
                type: Number,
                required: true
            }
        },   
        methods: {
            fetch: function () {
                ReadingService.getAllForTrip(this.tripId).then(function (data) {
                    this.readings = data;
                }.bind(this),
                    function (error) {
                        System.showAlert(error, "error");
                    });
            },
            refresh: function () {
                this.fetch();
            }
        },
        created: function () {
            this.fetch();
        },
        components: {
            "app-collapsible-card": CollapsibleCard
        }
    }
</script>