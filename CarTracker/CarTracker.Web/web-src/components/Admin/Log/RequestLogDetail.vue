﻿<template>
    <div>
        <div class="row py-3">
            <div class="col-md-12">
                <app-collapsible-card title="Details">
                    <div class="form-group row">
                        <label class="col-md-2 col-form-label">URL</label>
                        <div class="col-md-10">
                            <p class="form-control-plaintext">{{ requestUrl }}</p>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-md-2 col-form-label">Client Address</label>
                        <div class="col-md-10">
                            <p class="form-control-plaintext">{{ clientAddress }}</p>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-md-2 col-form-label">Method</label>
                        <div class="col-md-4">
                            <p class="form-control-plaintext">{{ requestMethod }}</p>
                        </div>
                        <label class="col-md-2 col-form-label">Response Status</label>
                        <div class="col-md-4">
                            <p class="form-control-plaintext">{{ responseStatus }}</p>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-md-2 col-form-label">Type</label>
                        <div class="col-md-4">
                            <p class="form-control-plaintext">{{ type | friendlyConstant("LOG_TYPE") }}</p>
                        </div>
                        <label class="col-md-2 col-form-label">Event UUID</label>
                        <div class="col-md-4">
                            <p class="form-control-plaintext">{{ requestUuid }}</p>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-md-2 col-form-label">Date</label>
                        <div class="col-md-4">
                            <p class="form-control-plaintext">{{ createDate | formatDateTime }}</p>
                        </div>
                        <label class="col-md-2 col-form-label">User</label>
                        <div class="col-md-4">
                            <p class="form-control-plaintext">{{ user.name }}</p>
                        </div>
                    </div>
                </app-collapsible-card>
            </div>
        </div>

        <div class="row py-3">
            <div class="col-md-6">
                <app-collapsible-card title="Request Headers" :expanded="true">
                    <pre>{{ requestHeaders | prettyJson }}</pre>
                </app-collapsible-card>
            </div>
            <div class="col-md-6">
                <app-collapsible-card title="Response Headers" :expanded="true">
                    <pre>{{ responseHeaders | prettyJson }}</pre>
                </app-collapsible-card>
            </div>
        </div>

        <div class="row">
            <div class="col-md-6">
                <app-collapsible-card title="Request Body" :expanded="true">
                    <pre>{{ requestBody | prettyJson }}</pre>
                </app-collapsible-card>
            </div>
            <div class="col-md-6">
                <app-collapsible-card title="Response Body" :expanded="true">
                    <pre>{{ responseBody | prettyJson }}</pre>
                </app-collapsible-card>

            </div>
        </div>
    </div>
</template>

<script>
    import CollapsibleCard from "components/Common/CollapsibleCard.vue"
    import RequestLogMixin from "components/Admin/Log/RequestLogMixin.vue"
    

    export default {
        mixins: [RequestLogMixin],
        components: {
            "app-collapsible-card": CollapsibleCard
        },
        data: function () {
            return {};
        },
        props: {
            eventUuid: {
                type: String,
                requred: false
            }
        },
        created: function () {
            this.requestUuid = this.eventUuid;
            this.refresh();
        }
    }
</script>