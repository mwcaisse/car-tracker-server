﻿<template>
    <div class="row">
        <div class="col-md-4 col-bottom" v-for="car in cars">
            <a class="no-color" :href="getLinkForCar(car)">
                <div class="card card-hover">
                    <i class="fa fa-car" style="font-size: 200px; text-align: center;"></i>
                    <div class="card-body">
                        <h3 class="card-title">{{ car.name }}</h3>
                        <p class="card-text">{{ car.vin }}</p>
                    </div>
                </div>
            </a>
        </div>
    </div>
</template>

<script>
    import System from "services/System.js"
    import Links from "services/Links.js"
    import { CarService } from "services/ApplicationProxy.js"

    export default {
        data: function () {
            return {
                cars: []
            }
        },
        methods: {
            fetchCars: function () {
                CarService.getAll().then(function (data) {
                    this.cars = data.data;
                }.bind(this),
                    function (error) {
                        System.showAlert(error, "error");
                    });
            },
            getLinkForCar: function (car) {
                return Links.viewCar(car.id);
            }
        },
        created: function () {
            this.fetchCars();
        }
    };
    
</script>