<template>
    <div class="card">
        <div class="card-header">
            Maintenance Records
            <span class="float-right">
                <i class="fa fa-plus action-icon" aria-hidden="true" v-on:click="createRecord" v-tooltip title="Add Maintenance Record"></i>
                &nbsp;
                <i class="fa fa-refresh action-icon" aria-hidden="true" v-on:click="refresh" v-tooltip title="Refresh"></i>
            </span>
        </div>
        <div v-if="records.length > 0">
            <table class="table table-striped table-bordered">
                <thead>
                    <tr>
                        <th style="width: 1%;"></th> <!-- actions column -->
                        <th><app-column-header columnId="Date" columnName="Date" :currentSort="currentSort" v-on:sort:update="sortUpdated" v-on:sort:clear="sortCleared"></app-column-header></th>
                        <th><app-column-header columnId="Type" columnName="Type" :currentSort="currentSort" v-on:sort:update="sortUpdated" v-on:sort:clear="sortCleared"></app-column-header></th>
                        <th><app-column-header columnId="Mileage" columnName="Mileage" :currentSort="currentSort" v-on:sort:update="sortUpdated" v-on:sort:clear="sortCleared"></app-column-header></th>
                        <th><app-column-header columnId="Notes" columnName="Notes" :currentSort="currentSort" v-on:sort:update="sortUpdated" v-on:sort:clear="sortCleared"></app-column-header></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="record in records">
                        <td>
                            <i class="fa fa-th-list action-icon" aria-hidden="true" v-on:click="viewRecord(record)" v-tooltip title="View Maintenance Record"></i>
                            <i class="fa fa-trash action-icon" aria-hidden="true" v-on:click="deleteRecord(record)" v-tooltip title="Delete"></i>
                        </td>
                        <td> {{ record.date | formatDate }}</td>
                        <td> {{ record.type | friendlyConstant("CAR_MAINTENANCE_TYPE") }}</td>
                        <td> {{ record.mileage | prettyNumber(2) }}</td>
                        <td> {{ record.notes }}</td>
                    </tr>
                </tbody>
            </table>
            <app-pager :totalItems="totalItems" :currentPage="currentPage" :itemsPerPage="currentItemsPerPage" v-on:paging:update="pagingUpdated"></app-pager>
        </div>
        <div class="card-body text-center" v-else>
            <p class="card-text">No maintenance records have been created for this car yet.</p>
        </div>

        <app-car-maintenance-modal :carId="carId" v-on:carMaintenance:updated="refresh"></app-car-maintenance-modal>
    </div>
</template>

<script>
    import System from "services/System.js"
    import { CarMaintenanceService } from "services/ApplicationProxy.js"
    import PagedGridMixin from "components/Common/PagedGridMixin.vue"
    import Pager from "components/Common/Pager.vue"
    import ColumnHeader from "components/Common/ColumnHeader.vue"
    import CarMaintenanceModal from "components/Car/CarMaintenanceModal.vue"
   
    export default {
        mixins: [PagedGridMixin],
        data: function () {
            return {
                records: [],
                currentSort: { propertyId: "Date", ascending: false }
            }
        },
        props: {
            carId: {
                type: Number,
                required: true
            }
        },
        methods: {
            fetch: function () {
                CarMaintenanceService.getForCar(this.carId, this.startAt, this.take, this.currentSort).then(function (data) {
                    this.update(data);
                }.bind(this), function (error) {
                    System.showAlert(error, "error");
                });
            },
            update: function (data) {
                this.records = $.map(data.data, function (elm) {
                    return this.augmentRecord(elm);
                }.bind(this));
                this.totalItems = data.total;
            },
            augmentRecord: function (record) {
                record.date = moment(record.date);
                return record;
            },
            refresh: function () {
                this.fetch();
            },
            viewRecord: function (record) {
                System.events.$emit("carMaintenance:edit", record.carMaintenanceId);
            },
            createRecord: function () {
                System.events.$emit("carMaintenance:create");
            },
            deleteRecord: function (record) {
                CarMaintenanceService.delete(this.carId, record.carMaintenanceId).then(function () {
                    var ind = this.records.indexOf(record);
                    this.records.splice(ind, 1);
                }.bind(this), function (error) {
                    System.showAlert(error, "error");
                });
            }
        },
        created: function () {
            this.fetch();
        },
        components: {
            "app-pager": Pager,
            "app-column-header": ColumnHeader,
            "app-car-maintenance-modal": CarMaintenanceModal
        }
    }

</script>