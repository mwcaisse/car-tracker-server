<template>
    <div class="alert alert-dismissible fade show" :class="alertClass" v-if="showAlert" role="alert">
        {{ message }}
        <button type="button" class="close" v-on:click="hide" aria-label="Close">
            <span aria-hidden="true">&times;</span>
        </button>
    </div>
</template>

<script>
    import System from "services/System.js"
    import * as Constants from "services/Constants.js"
    import * as Util from "services/Util.js"

    export default {
        data: function () {
            return {
                type: "",
                message: ""
            }
        },
        computed: {
            showAlert: function () {
                return !Util.isStringNullOrBlank(this.message);
            },
            alertClass: function () {
                if (this.type === "error") {
                    this.type = "danger";
                }
                return "alert-" + this.type;
            }
        },   
        methods: {
            show: function (message, type) {
                this.message = message;
                this.type = type || "info";
            },
            hide: function () {
                this.message = "";
                this.type = "info";
            }
        },
        created: function () {
            System.events.$on(Constants.EVENT_ALERT_SHOW, function (params) {
                this.show(params.message, params.type);
            }.bind(this));
        }
    }
</script>