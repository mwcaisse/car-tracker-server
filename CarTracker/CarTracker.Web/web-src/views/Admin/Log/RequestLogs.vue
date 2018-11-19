<template>
    <div>
        <div class="row">
            <div class="col-md-12">
                <h2>Request Logs</h2>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12 py-3">
                <app-request-log-grid></app-request-log-grid>
            </div>
        </div>
    </div>
</template>

<script>
    import Links from "services/Links.js"
    import RequestLogGrid from "components/Admin/Log/RequestLogGrid.vue"

    export default {
        data: function () {
            return {
            }
        },
        components: {
            "app-request-log-grid": RequestLogGrid
        },
        created: function () {
            Links.setActiveNavigation("Admin");
        }
    }
</script>