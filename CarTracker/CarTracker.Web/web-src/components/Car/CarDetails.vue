<template>
    <div class="card">
        <div class="card-header">
            Details
            <span class="float-right">
                <i class="fa fa-floppy-o action-icon" aria-hidden="true" v-on:click="save" v-tooltip title="Save"></i>
                &nbsp;
                <i class="fa fa-refresh action-icon" aria-hidden="true" v-on:click="refresh" v-tooltip title="Refresh"></i>
            </span>
        </div>
        <div class="card-body">
            <form>
                <div class="form-group row">
                    <label class="col-md-2 col-form-label">Name</label>
                    <div class="col-md-10">
                        <input type="text" class="form-control" placeholder="Name" v-model="name" />
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-2 col-form-label">VIN</label>
                    <div class="col-md-10">
                        <p class="form-control-plaintext"> {{ vin }}</p>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-2 col-form-label">Mileage</label>
                    <div class="col-md-10">
                        <p class="form-control-plaintext"> {{ mileage | prettyNumber(2) }}</p>
                    </div>
                </div>
            </form>
        </div>
    </div>
</template>

<script>

    import System from "services/System.js"
    import { CarService } from "services/ApplicationProxy.js"


    export default {
        data: function () {
            return {
                name: "",
                vin: "",
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
            fetchCar: function () {
                CarService.get(this.carId).then(function (data) {
                    this.update(data);
                }.bind(this), function (error) {
                    System.showAlert(error, "error");
                });
            },
            update: function (car) {
                this.name = car.name;
                this.vin = car.vin;
                this.mileage = car.mileage;
            },
            save: function () {
                CarService.update(this.toCarObject()).then(function (data) {
                    this.update(data);
                }.bind(this), function (error) {
                    System.showAlert(error, "error");
                }.bind(this));
            },
            toCarObject: function () {
                return {
                    id: this.carId,
                    name: this.name,
                    vin: this.vin
                };
            },
            refresh: function () {
                this.fetchCar();
            }
        },
        created: function () {
            this.fetchCar();
        }
    }
        
</script>