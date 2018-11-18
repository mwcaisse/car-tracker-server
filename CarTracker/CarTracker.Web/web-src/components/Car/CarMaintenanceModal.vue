<template>
    <div>
        <app-modal :title="title" ref="modal" :onClose="clear">
            <form>
                <div class="form-group row">
                    <label class="col-md-2 col-form-label">Type</label>
                    <div class="col-md-10">
                        <select class="form-control" v-model="type">
                            <option v-for="type in constants.CAR_MAINTENANCE_TYPES" v-bind:value="type">
                                {{ type | friendlyConstant("CAR_MAINTENANCE_TYPE")}}
                            </option>
                        </select>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-2 col-form-label">Date</label>
                    <div class="col-md-10">
                        <app-datepicker v-model="date"></app-datepicker>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-2 col-form-label">Mileage</label>
                    <div class="col-md-10">
                        <input type="text" class="form-control" placeholder="Enter Mileage" v-model="mileage" />
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-2 col-form-label">Notes</label>
                    <div class="col-md-10">
                        <textarea class="form-control" rows="4" v-model="notes"></textarea>
                    </div>
                </div>
            </form>
            <button slot="footer" type="button" class="btn btn-primary" v-on:click="save">Save</button>
        </app-modal>
    </div>
</template>

<script>
    import System from "services/System.js"
    import { CarMaintenanceService } from "services/ApplicationProxy.js"

    import Modal from  "components/Common/Modal.vue"
    import DatePicker from "components/Common/DatePicker.vue"

    export default {
        data: function () {
            return {
                title: "Add Car Maintenance",
                carMaintenanceId: -1,
                type: -1,
                notes: "",
                date: new Date(),
                mileage: 0
            }
        },
        props: {
            carId: {
                type: Number,
                required: true
            }
        },
        methods: {
            fetchRecord: function () {
                CarMaintenanceService.get(this.carId, this.carMaintenanceId).then(function (data) {
                    this.update(data);
                }.bind(this), function (error) {
                    System.showAlert(error, "error");
                });
            },
            update: function (record) {
                this.carMaintenanceId = record.carMaintenanceId;
                this.type = record.type;
                this.notes = record.notes;
                this.date = new Date(record.date);
                this.mileage = record.mileage;
            },
            clear: function () {
                this.carMaintenanceId = -1;
                this.type = -1;
                this.notes = "";
                this.date = new Date();
                this.mileage = 0;
            },
            createModel: function () {
                return {
                    carMaintenanceId: this.carMaintenanceId,
                    carId: this.carId,
                    type: this.type,
                    notes: this.notes,
                    date: this.date.valueOf(),
                    mileage: this.mileage
                };
            },
            save: function () {
                var func;
                if (this.carMaintenanceId < 0) {
                    func = CarMaintenanceService.create;
                }
                else {
                    func = CarMaintenanceService.update;
                }

                func(this.carId, this.createModel()).then(function () {
                    this.$emit("carMaintenance:updated");
                    this.$refs.modal.close();
                }.bind(this), function (error) {
                    System.showAlert(error, "error");
                });
            },
            refresh: function () {
                this.fetchRecord();
            }
        },
        created: function () {
            System.events.$on("carMaintenance:create", function () {
                this.$refs.modal.open();
                this.title = "Add Maintenance Record";
            }.bind(this));

            System.events.$on("carMaintenance:edit", function (maintenanceId) {
                this.carMaintenanceId = maintenanceId;
                this.fetchRecord();
                this.title = "Edit Maintenance Record";
                this.$refs.modal.open();
            }.bind(this));
        },
        components: {
            "app-modal": Modal,
            "app-datepicker": DatePicker
        }
    }
</script>