<template>
    <div class="card">
        <div class="card-header">
            Registration Keys
            <span class="float-right">
                <i class="fa fa-plus action-icon" aria-hidden="true" v-on:click="createKey" v-tooltip title="New Registration Key"></i>
                &nbsp;
                <i class="fa fa-refresh action-icon" aria-hidden="true" v-on:click="refresh" v-tooltip title="Refresh"></i>
            </span>
        </div>
        <table class="table table-striped table-bordered">
            <thead>
                <tr>
                    <th style="width: 1%;"></th> <!-- actions column -->
                    <th><app-column-header columnId="Key" columnName="Key" :currentSort="currentSort" v-on:sort:update="sortUpdated" v-on:sort:clear="sortCleared"></app-column-header></th>
                    <th><app-column-header columnId="UsesRemaining" columnName="Uses Remaining" :currentSort="currentSort" v-on:sort:update="sortUpdated" v-on:sort:clear="sortCleared"></app-column-header></th>
                    <th><app-column-header columnId="Active" columnName="Active" :currentSort="currentSort" v-on:sort:update="sortUpdated" v-on:sort:clear="sortCleared"></app-column-header></th>
                    <th><app-column-header columnId="CreateDate" columnName="Create Date" :currentSort="currentSort" v-on:sort:update="sortUpdated" v-on:sort:clear="sortCleared"></app-column-header></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="key in keys" :class="key.rowCss">
                    <td>
                        <i class="fa fa-th-list action-icon" aria-hidden="true" v-on:click="viewKey(key.userRegistrationKeyId)" v-tooltip title="View Registration Key"></i>
                    </td>
                    <td> {{ key.key }}</td>
                    <td> {{ key.usesRemaining }}</td>
                    <td> {{ key.active | formatBoolean }}</td>
                    <td> {{ key.createDate | formatDateTime }}</td>
                </tr>
            </tbody>
        </table>
        <app-pager :totalItems="totalItems" :currentPage="currentPage" :itemsPerPage="currentItemsPerPage" v-on:paging:update="pagingUpdated"></app-pager>

        <!--  the modal for creating/viewing registration keys -->
        <app-registration-key-modal v-on:registrationKey:updated="refresh"></app-registration-key-modal>
    </div>
</template>

<script>
    import System from "services/System.js"
    import { RegistrationKeyService } from "services/ApplicationProxy.js"

    import PagedGridMixin from "components/Common/PagedGridMixin.vue"
    import Pager from "components/Common/Pager.vue"
    import ColumnHeader from "components/Common/ColumnHeader.vue"

    import RegistrationKeyModal from "components/Admin/RegistrationKeyModal.vue"

    export default {
        mixins: [PagedGridMixin],
        components: {
            "app-pager": Pager,
            "app-column-header": ColumnHeader,
            "app-registration-key-modal": RegistrationKeyModal
        },
        data: function () {
            return {
                keys: [],
                currentSort: { propertyId: "CreateDate", ascending: false }
            }
        },        
        methods: {
            fetch: function () {
                RegistrationKeyService.getAllPaged(this.startAt, this.take, this.currentSort).then(function (data) {
                    this.update(data);
                }.bind(this),
                    function (error) {
                        System.showAlert(error, "error");
                    });
            },
            update: function (data) {
                this.keys = $.map(data.data, function (elm) {
                    return this.augmentKey(elm);
                }.bind(this));
                this.totalItems = data.total;
            },
            augmentKey: function (key) {
                key.createDate = moment(key.createDate);

                var rowCss = "table-danger";
                if (key.active) {
                    if (key.usesRemaining < 1) {
                        rowCss = "table-danger";
                    }
                    else if (key.usesRemaining < 5) {
                        rowCss = "table-warning";
                    }
                    else {
                        rowCss = "table-success";
                    }
                }
                key.rowCss = rowCss;

                return key;
            },
            refresh: function () {
                this.fetch();
            },
            viewKey: function (id) {
                System.events.$emit("registrationKey:edit", id);
            },
            createKey: function () {
                System.events.$emit("registrationKey:create");
            }
        },
        created: function () {
            this.fetch();
        }
    }
</script>