<template>
    <div>
        <app-modal :title="title" ref="modal" :onClose="clear">
            <app-collapsible-card title="Details">
                <div>
                    <div class="form-group row">
                        <label class="col-md-2 col-form-label">Event UUID</label>
                        <div class="col-md-10">
                            <p class="form-control-plaintext">
                                <a :href="requestDetailUrl">{{ requestUuid }}</a>
                            </p>
                        </div>
                    </div>
                </div>
                <div>
                    <div class="form-group row">
                        <label class="col-md-2 col-form-label">Type</label>
                        <div class="col-md-10">
                            <p class="form-control-plaintext">{{ type | friendlyConstant("LOG_TYPE") }}</p>
                        </div>
                    </div>
                </div>
                <div>
                    <div class="form-group row">
                        <label class="col-md-2 col-form-label">Client Address</label>
                        <div class="col-md-10">
                            <p class="form-control-plaintext">{{ clientAddress }}</p>
                        </div>
                    </div>
                </div>
                <div>
                    <div class="form-group row">
                        <label class="col-md-2 col-form-label">URL</label>
                        <div class="col-md-10">
                            <p class="form-control-plaintext">{{ requestUrl }}</p>
                        </div>
                    </div>
                </div>
                <div>
                    <div class="form-group row">
                        <label class="col-md-2 col-form-label">Method</label>
                        <div class="col-md-10">
                            <p class="form-control-plaintext">{{ requestMethod }}</p>
                        </div>
                    </div>
                </div>
                <div>
                    <div class="form-group row">
                        <label class="col-md-2 col-form-label">Response Status</label>
                        <div class="col-md-10">
                            <p class="form-control-plaintext">{{ responseStatus }}</p>
                        </div>
                    </div>
                </div>
                <div>
                    <div class="form-group row">
                        <label class="col-md-2 col-form-label">Date</label>
                        <div class="col-md-10">
                            <p class="form-control-plaintext">{{ createDate | formatDateTime }}</p>
                        </div>
                    </div>
                </div>
                <div>
                    <div class="form-group row">
                        <label class="col-md-2 col-form-label">User</label>
                        <div class="col-md-10">
                            <p class="form-control-plaintext">{{ user.name }}</p>
                        </div>
                    </div>
                </div>
            </app-collapsible-card>

            <br />
            <app-collapsible-card title="Request Headers" :expanded="false">
                <pre>{{ requestHeaders | prettyJson }}</pre>
            </app-collapsible-card>
            <br />

            <app-collapsible-card title="Request Body" :expanded="false">
                <pre>{{ requestBody | prettyJson }}</pre>
            </app-collapsible-card>
            <br />

            <app-collapsible-card title="Response Headers" :expanded="false">
                <pre>{{ responseHeaders | prettyJson }}</pre>
            </app-collapsible-card>
            <br />

            <app-collapsible-card title="Response Body" :expanded="false">
                <pre>{{ responseBody | prettyJson }}</pre>
            </app-collapsible-card>


        </app-modal>
    </div>
</template>

<script>
    import System from "services/System.js"
    import Links from "services/Links.js"
    
    import Modal from "components/Common/Modal.vue"
    import CollapsibleCard from "components/Common/CollapsibleCard.vue"
    import RequestLogMixin from "components/Admin/Log/RequestLogMixin.vue"

    export default {
        mixins: [RequestLogMixin],
        components: {
            "app-collapsible-card": CollapsibleCard,
            "app-modal": Modal
        },
        data: function () {
            return {
                title: "View Request Log"
            }
        },
        computed: {
            requestDetailUrl: function () {
                return Links.adminRequestLogsDetails(this.requestUuid);
            }
        },
        created: function () {
            System.events.$on("requestLog:view", function (requestLogId) {
                this.clear();
                this.requestLogId = requestLogId;
                this.fetchLog();
                this.$refs.modal.open();
            }.bind(this));

            System.events.$on("requestLog:view:uuid", function (requestUuid) {
                this.clear();
                this.requestUuid = requestUuid;
                this.fetchLog();
                this.$refs.modal.open();
            }.bind(this));
        }
    }
</script>