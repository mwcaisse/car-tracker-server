<template>
    <div class="card">
        <div class="card-header">
            Server Logs
            <span class="float-right">
                <i class="fa fa-refresh action-icon" aria-hidden="true" v-on:click="refresh" v-tooltip title="Refresh"></i>
            </span>
        </div>
        <table class="table table-striped table-bordered">
            <thead>
                <tr>
                    <th style="width: 1%;"></th> <!-- actions column -->
                    <th>
                        <app-column-header columnId="RequestUuid"
                                           columnName="Request UUID"
                                           :currentSort="currentSort"
                                           v-on:sort:update="sortUpdated"
                                           v-on:sort:clear="sortCleared"
                                           :enableFilter="true"
                                           filterType="TEXT"
                                           v-on:filter:update="filterUpdated"
                                           v-on:filter:clear="filterCleared">
                        </app-column-header>
                    </th>
                    <th>
                        <app-column-header columnId="Type"
                                           columnName="Type"
                                           :currentSort="currentSort"
                                           v-on:sort:update="sortUpdated"
                                           v-on:sort:clear="sortCleared"
                                           :enableFilter="true"
                                           filterType="DROPDOWN_COMPLEX"
                                           :filterOptions="getLogTypeOptions"
                                           v-on:filter:update="filterUpdated"
                                           v-on:filter:clear="filterCleared">
                        </app-column-header>
                    </th>
                    <th>
                        <app-column-header columnId="Message"
                                           columnName="Message"
                                           :currentSort="currentSort"
                                           v-on:sort:update="sortUpdated"
                                           v-on:sort:clear="sortCleared"
                                           :enableFilter="true"
                                           filterType="TEXT"
                                           v-on:filter:update="filterUpdated"
                                           v-on:filter:clear="filterCleared">
                        </app-column-header>
                    </th>
                    <th>
                        <app-column-header columnId="ExceptionMessage"
                                           columnName="Exception Message"
                                           :currentSort="currentSort"
                                           v-on:sort:update="sortUpdated"
                                           v-on:sort:clear="sortCleared"
                                           :enableFilter="true"
                                           filterType="TEXT"
                                           v-on:filter:update="filterUpdated"
                                           v-on:filter:clear="filterCleared">
                        </app-column-header>
                    </th>
                    <th>
                        <app-column-header columnId="CreateDate"
                                           columnName="Date"
                                           :currentSort="currentSort"
                                           v-on:sort:update="sortUpdated"
                                           v-on:sort:clear="sortCleared"
                                           :enableFilter="true"
                                           filterType="DATE"
                                           v-on:filter:update="filterUpdated"
                                           v-on:filter:clear="filterCleared">
                        </app-column-header>
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="log in logs">
                    <td>
                        <i class="fa fa-th-list action-icon" aria-hidden="true" v-on:click="viewServerLog(log.serverLogId)" v-tooltip title="View Server Log"></i>
                    </td>
                    <td> <a href="#" v-on:click.prevent="viewRequestLog(log.requestUuid)">{{ log.requestUuid }}</a></td>
                    <td> {{ log.type | friendlyConstant("LOG_TYPE") }}</td>
                    <td> {{ log.message  }}</td>
                    <td> {{ log.exceptionMessage  }}</td>
                    <td> {{ log.createDate | formatDateTime }}</td>
                </tr>
            </tbody>
        </table>
        <app-pager :totalItems="totalItems" :currentPage="currentPage" :itemsPerPage="currentItemsPerPage" v-on:paging:update="pagingUpdated"></app-pager>

        <app-request-log-modal></app-request-log-modal>
    </div>
</template>

<script>
    import System from "services/System.js"
    import * as FriendlyConstants from "services/FriendlyConstants.js"
    import { ServerLogService } from "services/ApplicationProxy.js"

    import PagedGridMixin from "components/Common/PagedGridMixin.vue"
    import Pager from "components/Common/Pager.vue"
    import ColumnHeader from "components/Common/ColumnHeader.vue"

    import RequestLogModal from "components/Admin/Log/RequestLogModal.vue"

    export default {
        mixins: [PagedGridMixin],
        components: {
            "app-pager": Pager,
            "app-column-header": ColumnHeader,
            "app-request-log-modal": RequestLogModal
        },
        data: function () {
            return {
                logs: [],
                currentSort: { propertyId: "CreateDate", ascending: false }
            }
        },
        methods: {
            fetch: function () {
                ServerLogService.getAllPaged(this.startAt, this.take, this.currentSort,
                    this.currentFilter).then(function (data) {

                        this.update(data);
                    }.bind(this), function (error) {
                        System.showAlert(error, "error");
                    });
            },
            update: function (data) {
                this.logs = $.map(data.data, function (elm) {
                    return this.augmentLog(elm);
                }.bind(this));
                this.totalItems = data.total;
            },
            augmentLog: function (log) {
                log.createDate = moment(log.createDate);
                return log;
            },
            refresh: function () {
                this.fetch();
            },
            viewServerLog: function (id) {
                System.events.$emit("serverLog:view", id);
            },
            viewRequestLog: function (uuid) {
                System.events.$emit("requestLog:view:uuid", uuid);
            },
            getLogTypeOptions: function () {
                return Q.fcall(function () {
                    return $.map(FriendlyConstants.LOG_TYPE, function (val, key) {
                        return {
                            value: key,
                            display: val
                        }
                    });
                });
            }
        },
        created: function () {
            this.fetch();
        }
    }
</script>