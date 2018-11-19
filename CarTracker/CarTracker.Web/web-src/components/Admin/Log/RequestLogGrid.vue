<template>
    <div class="card">
        <div class="card-header">
            Request Logs
            <span class="float-right">
                <i class="fa fa-refresh action-icon" aria-hidden="true" v-on:click="refresh" v-tooltip title="Refresh"></i>
            </span>
        </div>
        <table class="table table-striped table-bordered">
            <thead>
                <tr>
                    <th style="width: 1%;"></th> <!-- actions column -->
                    <th>
                        <app-column-header columnId="RequestUrl"
                                           columnName="URL"
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
                        <app-column-header columnId="RequestMethod"
                                           columnName="Method"
                                           :currentSort="currentSort"
                                           v-on:sort:update="sortUpdated"
                                           v-on:sort:clear="sortCleared"
                                           :enableFilter="true"
                                           :filterOptions="getMethodFilterOptions"
                                           v-on:filter:update="filterUpdated"
                                           v-on:filter:clear="filterCleared">
                        </app-column-header>
                    </th>
                    <th>
                        <app-column-header columnId="ResponseStatus"
                                           columnName="Status"
                                           :currentSort="currentSort"
                                           v-on:sort:update="sortUpdated"
                                           v-on:sort:clear="sortCleared"
                                           :enableFilter="true"
                                           :filterOptions="getStatusFilterOptions"
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
                    <th>
                        <app-column-header columnId="User"
                                           columnName="User"
                                           :enableSort="false">
                        </app-column-header>
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="log in logs">
                    <td>
                        <i class="fa fa-th-list action-icon" aria-hidden="true" v-on:click="viewLog(log.requestLogId)" v-tooltip title="View Request Log"></i>
                    </td>
                    <td> {{ log.requestUrl }}</td>
                    <td> {{ log.requestMethod }}</td>
                    <td> {{ log.responseStatus  }}</td>
                    <td> {{ log.createDate | formatDateTime }}</td>
                    <td> {{ log.user.name }}</td>
                </tr>
            </tbody>
        </table>
        <app-pager :totalItems="totalItems" :currentPage="currentPage" :itemsPerPage="currentItemsPerPage" v-on:paging:update="pagingUpdated"></app-pager>

        <app-request-log-modal></app-request-log-modal>
    </div>
</template>

<script>
    import System from "services/System.js"
    import { RequestLogService } from "services/ApplicationProxy.js"

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
                RequestLogService.getAllPaged(this.startAt, this.take, this.currentSort,
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

                if (log.user == null || typeof log.user === "undefined") {
                    log.user = {
                        name: ""
                    };
                }

                return log;
            },
            refresh: function () {
                this.fetch();
            },
            viewLog: function (id) {
                System.events.$emit("requestLog:view", id);
            },
            getMethodFilterOptions: function () {
                return RequestLogService.getMethodFilters();
            },
            getStatusFilterOptions: function () {
                return RequestLogService.getStatusFilters();
            }
        },
        created: function () {
            this.fetch();
        }
    }
</script>