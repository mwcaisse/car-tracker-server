<template>
    <div>
        <div class="row">
            <div class="col-md-12">
                <h2>Server Logs</h2>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12 py-3">
                <app-server-log-grid></app-server-log-grid>
            </div>
        </div>
    </div>
</template>

<script>
    import Links from "services/Links.js"
    import ServerLogGrid from "components/Admin/Log/ServerLogGrid.vue"

    export default {
        data: function () {
            return {
            }
        },
        components: {
            "app-server-log-grid": ServerLogGrid
        },
        created: function () {
            Links.setActiveNavigation("Admin");
        }
    }
</script>