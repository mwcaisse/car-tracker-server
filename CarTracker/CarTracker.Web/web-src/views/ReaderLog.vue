<template>
    <div>
        <div class="row">
            <h2>Reader Logs</h2>
        </div>

        <div class="row py-3">
            <div class="col-md-12">
                <app-reader-log-grid></app-reader-log-grid>
            </div>
        </div>
    </div>
</template>

<script>
    import Links from "services/Links.js"
    import ReaderLogGrid from "components/ReaderLog/ReaderLogGrid.vue"

    export default {
        data: function () {
            return {
            }
        },
        components: {
            "app-reader-log-grid": ReaderLogGrid
        },
        created: function () {
            Links.setActiveNavigation("Log");
        }
    }
</script>