<template>
    <div class="card">
        <div class="card-header">
            Trips
            <span class="float-right">
                <i class="fa fa-hdd-o action-icon" aria-hidden="true" v-tooltip title="Process All" v-on:click="processAll"></i>
                &nbsp;
                <i class="fa fa-refresh action-icon" aria-hidden="true" v-on:click="refresh" v-tooltip title="Refresh"></i>
            </span>
        </div>
        <div v-if="trips.length > 0">
            <table class="table table-striped table-bordered">
                <thead>
                    <tr>
                        <th></th> <!--  action -->
                        <th><app-column-header columnId="Name" columnName="Name" :currentSort="currentSort" v-on:sort:update="sortUpdated" v-on:sort:clear="sortCleared"></app-column-header></th>
                        <th><app-column-header columnId="StartDate" columnName="Start Date" :currentSort="currentSort" v-on:sort:update="sortUpdated" v-on:sort:clear="sortCleared"></app-column-header></th>
                        <th><app-column-header columnId="EndDate" columnName="End Date" :currentSort="currentSort" v-on:sort:update="sortUpdated" v-on:sort:clear="sortCleared"></app-column-header></th>
                        <th><app-column-header columnId="Status" columnName="Status" :currentSort="currentSort" v-on:sort:update="sortUpdated" v-on:sort:clear="sortCleared"></app-column-header></th>
                        <th><app-column-header columnId="DistanceTraveled" columnName="Distance Traveled" :currentSort="currentSort" v-on:sort:update="sortUpdated" v-on:sort:clear="sortCleared"></app-column-header></th>
                        <th><app-column-header columnId="DestinationPlace.Name" columnName="Destination" :enableSort="false"></app-column-header></th>
                    </tr>
                </thead>
                <tbody>
                    <app-trip-row v-for="trip in trips" :key="trip.id" :trip="trip"></app-trip-row>
                </tbody>
            </table>
            <app-pager :totalItems="totalItems" :currentPage="currentPage" :itemsPerPage="currentItemsPerPage" v-on:paging:update="pagingUpdated"></app-pager>
        </div>
        <div class="card-body text-center" v-else>
            <p class="card-text">No trips have been recorded for this car yet.</p>
        </div>
    </div>
</template>

<script>
    import System from "services/System.js"
    import { TripService } from "services/ApplicationProxy.js"

    import PagedGridMixin from "components/Common/PagedGridMixin.vue"
    import TripRow from "components/Trip/TripRow.vue"
    import ColumnHeader from "components/Common/ColumnHeader.vue"
    import Pager from "components/Common/Pager.vue"
    

    export default {
        mixins: [PagedGridMixin],
        components: {
            "app-trip-row": TripRow,
            "app-column-header": ColumnHeader,
            "app-pager": Pager
        },
        data: function () {
            return {
                trips: [],
                currentSort: { propertyId: "StartDate", ascending: false }
            }
        },
        props: {
            carId: {
                type: Number,
                required: true
            }
        },       
        methods: {
            fetchTrips: function () {
                TripService.getAllForCarPaged(this.carId, this.startAt, this.take, this.currentSort).then(function (data) {
                    this.update(data);
                }.bind(this), function (error) {
                    System.showAlert(error, "error");
                });
            },
            processAll: function () {
                TripService.processUnprocessed().then(function () {
                    this.refresh();
                }.bind(this));
            },
            createTrip: function (data) {
                return new (function () {
                    var trip = this;

                    trip.id = data.id;
                    trip.name = data.name;
                })();
            },
            update: function (data) {
                this.trips = data.data;
                this.totalItems = data.total;
            },
            refresh: function () {
                this.fetchTrips();
            }
        },
        created: function () {
            this.fetchTrips();
        }
    }
</script>