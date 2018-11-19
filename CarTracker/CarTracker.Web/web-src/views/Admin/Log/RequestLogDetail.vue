<template>
    <div>
        <div class="row">
            <div class="col-md-12">
                <h2>Request Log</h2>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12 py-3">
                <app-request-log-detail :eventUuid="requestGuid"></app-request-log-detail>
            </div>
        </div>
    </div>
</template>

<script>
    import Links from "services/Links.js"
    import RequestLogDetail from "components/Admin/Log/RequestLogDetail.vue"

    var requestGuid = $("#requestGuid").val();

    export default {
        data: function () {
            return {
                requestGuid: requestGuid
            }
        },
        components: {
            "app-request-log-detail": RequestLogDetail
        },
        created: function () {
            Links.setActiveNavigation("Admin");
        }
    }
</script>