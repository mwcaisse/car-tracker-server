<template>
    <div class="card">
        <div class="card-header">
            Supported Commands
            <span class="float-right">
                <i class="fa fa-refresh action-icon" aria-hidden="true" v-on:click="refresh" v-tooltip title="Refresh"></i>
            </span>
        </div>

        <div v-if="supportedCommands.length > 0">
            <table class="table">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>PID</th>
                        <th>Supported</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="supportedCommand in supportedCommands"
                        :class="{ 'table-success': supportedCommand.supported, 'table-danger': !supportedCommand.supported }">
                        <td>{{ supportedCommand.name }}</td>
                        <td>{{ supportedCommand.pid }}</td>
                        <td>{{ supportedCommand.supported | formatBoolean }}</td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="card-body text-center" v-else>
            <p class="card-text">
                No supported command information is available for this car yet.
                They can be added by selecting the "Check Available Commands" option from the application menu of the reader.
            </p>
        </div>
    </div>
</template>

<script>
    import System from "services/System.js"
    import { CarService } from "services/ApplicationProxy.js"

    export default {
        data: function () {
            return {
                supportedCommands: []
            };
        },
        props: {
            carId: {
                type: Number,
                required: true
            }
        },      
        methods: {
            fetchSupportedCommands: function () {
                CarService.getSupportedCommands(this.carId).then(function (data) {
                    this.update(data);
                }.bind(this),
                    function (error) {
                        System.showAlert(error, "error");
                    })
            },
            update: function (data) {
                if (data) {
                    this.supportedCommands = data;
                }
                else {
                    this.supportedCommands = [];
                }
            },
            refresh: function () {
                this.fetchSupportedCommands();
            }
        },
        created: function () {
            this.fetchSupportedCommands();
        }
    }
</script>