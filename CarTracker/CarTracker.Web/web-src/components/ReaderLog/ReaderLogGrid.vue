<template>
    <div class="card">
        <div class="card-header">
            Reader Logs
            <span class="float-right">
                <i class="fa fa-refresh action-icon" aria-hidden="true" v-on:click="refresh" v-tooltip title="Refresh"></i>
            </span>
        </div>
        <table class="table table-striped table-bordered">
            <thead>
                <tr>

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
                        <app-column-header columnId="Date"
                                           columnName="Date"
                                           :currentSort="currentSort"
                                           v-on:sort:update="sortUpdated"
                                           v-on:sort:clear="sortCleared"
                                           :enableFilter="true"
                                           filterType="DATE"
                                           :filterStartDate="startDate"
                                           :filterEndDate="endDate"
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
                </tr>
            </thead>
            <tbody>
                <tr v-for="log in logs">
                    <td> {{ log.type | friendlyConstant("LOG_TYPE") }} </td>
                    <td> {{ log.date | formatDateTime }} </td>
                    <td> {{ log.message }} </td>
                </tr>
            </tbody>
        </table>
        <app-pager :totalItems="totalItems" :currentPage="currentPage" :itemsPerPage="currentItemsPerPage" v-on:paging:update="pagingUpdated"></app-pager>
    </div>
</template>

<script>
    import System from "services/System.js"
    import * as FriendlyConstants from "services/FriendlyConstants.js"
    import { ReaderLogService } from "services/ApplicationProxy.js"
    import PagedGridMixin from "components/Common/PagedGridMixin.vue"
    import Pager from "components/Common/Pager.vue"
    import ColumnHeader from "components/Common/ColumnHeader.vue"

    export default {
        mixins: [PagedGridMixin],
        components: {
            "app-pager": Pager,
            "app-column-header": ColumnHeader
        },
        data: function () {
            return {
                logs: [],
                currentSort: { propertyId: "Date", ascending: false }
            }
        },
        props: {
            startDate: {
                type: Date,
                required: false,
                default: null
            },
            endDate: {
                type: Date,
                required: false,
                default: null
            }
        },
        methods: {
            fetch: function () {
                ReaderLogService.getAllPaged(this.startAt, this.take, this.currentSort,
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
                log.date = moment(log.date);
                return log;
            },
            refresh: function () {
                this.fetch();
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