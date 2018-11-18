<template>
    <tr :class="rowCss">
        <td>
            <a :href="viewLink" class="no-color">
                <i class="fa fa-th-list action-icon" aria-hidden="true" v-tooltip title="View Trip"></i>
            </a>
            <i class="fa fa-hdd-o action-icon" aria-hidden="true" v-tooltip title="Process Trip" v-if="canProcess" v-on:click="process"></i>
        </td>
        <td> {{ name }} </td>
        <td> {{ startDate | formatDateTime }} </td>
        <td> {{ endDate | formatDateTime}} </td>
        <td> {{ status | friendlyConstant("TRIP_STATUS") }} </td>
        <td> {{ distanceTraveled | kmToMi | round(2) }} mi</td>
        <td> {{ destinationName }} </td>
    </tr>
</template>

<script>
    import * as Constants from "services/Constants.js"
    import Links from "services/Links.js"
    import { TripService } from "services/ApplicationProxy.js"

    export default {
        data: function () {
            return {
                id: -1,
                name: "",
                startDate: moment(),
                endDate: moment(),
                status: "",
                distanceTraveled: 0,
                start: "",
                destination: ""
            };
        },
        props: {
            trip: {
                type: Object,
                requred: true
            }
        },
        computed: {
            canProcess: function () {
                return this.status !== Constants.TRIP_STATUS.PROCESSED;
            },
            rowCss: function () {
                switch (this.status) {
                    case Constants.TRIP_STATUS.NEW:
                        return "table-danger";
                    case Constants.TRIP_STATUS.STARTED:
                        return "table-warning";
                    case Constants.TRIP_STATUS.FINISHED:
                        return "table-info";
                    case Constants.TRIP_STATUS.PROCESSED:
                        return "";
                    default:
                        return "table-danger";
                }
            },
            viewLink: function () {
                return Links.viewTrip(this.id);
            },
            destinationName: function () {
                if (null == this.destinationPlace || typeof this.destinationPlace == "undefined") {
                    return "";
                }
                return this.destinationPlace.name;
            }
        },
        methods: {
            process: function () {
                TripService.process(this.id).then(function (processedTrip) {
                    this.update(processedTrip);
                }.bind(this));
            },
            update: function (data) {
                this.id = data.id;
                this.name = data.name;
                this.startDate = moment(data.startDate);
                this.endDate = moment(data.endDate);
                this.status = data.status;
                this.distanceTraveled = data.distanceTraveled;
                this.startPlace = data.startPlace;
                this.destinationPlace = data.destinationPlace;
            }
        },
        created: function () {
            this.update(this.trip);
        }
    }
</script>