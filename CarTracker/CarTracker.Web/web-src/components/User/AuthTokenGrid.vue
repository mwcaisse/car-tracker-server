<template>
    <div class="card">
        <div class="card-header">
            Authentication Tokens
            <span class="float-right">
                <i class="fa fa-refresh action-icon" aria-hidden="true" v-on:click="refresh" v-tooltip title="Refresh"></i>
            </span>
        </div>
        <div v-if="tokens.length > 0">
            <table class="table table-striped table-bordered">
                <thead>
                    <tr>
                        <th><app-column-header columnId="DeviceUuid" columnName="Device UUID" :currentSort="currentSort" v-on:sort:update="sortUpdated" v-on:sort:clear="sortCleared"></app-column-header></th>
                        <th><app-column-header columnId="Active" columnName="Active" :currentSort="currentSort" v-on:sort:update="sortUpdated" v-on:sort:clear="sortCleared"></app-column-header></th>
                        <th><app-column-header columnId="LastLogin" columnName="Last Login" :currentSort="currentSort" v-on:sort:update="sortUpdated" v-on:sort:clear="sortCleared"></app-column-header></th>
                        <th><app-column-header columnId="LastLoginAddress" columnName="Last Login Address" :currentSort="currentSort" v-on:sort:update="sortUpdated" v-on:sort:clear="sortCleared"></app-column-header></th>
                        <th><app-column-header columnId="ExpirationDate" columnName="Expiration Date" :currentSort="currentSort" v-on:sort:update="sortUpdated" v-on:sort:clear="sortCleared"></app-column-header></th>
                        <th><app-column-header columnId="CreateDate" columnName="Create Date" :currentSort="currentSort" v-on:sort:update="sortUpdated" v-on:sort:clear="sortCleared"></app-column-header></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="token in tokens">
                        <td> {{ token.deviceUuid }}</td>
                        <td> {{ token.active | formatBoolean }}</td>
                        <td> {{ token.lastLogin | formatDateTime }}</td>
                        <td> {{ token.lastLoginAddress }}</td>
                        <td> {{ token.expirationDate | formatDateTime }}</td>
                        <td> {{ token.createDate | formatDateTime }}</td>
                    </tr>
                </tbody>
            </table>
            <app-pager :totalItems="totalItems" :currentPage="currentPage" :itemsPerPage="currentItemsPerPage" v-on:paging:update="pagingUpdated"></app-pager>
        </div>
        <div class="card-body text-center" v-else>
            <p class="card-text">You do not have any active authentication tokens.</p>
        </div>
    </div>
</template>

<script>
    import System from "services/System.js"
    import { AuthTokenService } from "services/ApplicationProxy.js"

    import PagedGridMixin from "components/Common/PagedGridMixins.vue"
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
                tokens: [],
                currentSort: { propertyId: "LastLogin", ascending: false }
            }
        },
        methods: {
            fetch: function () {
                AuthTokenService.getAllActivePaged(this.startAt, this.take, this.currentSort).then(function (data) {
                    this.update(data);
                }.bind(this),
                    function (error) {
                        System.showAlert(error, "error");
                    })
            },
            update: function (data) {
                this.tokens = $.map(data.data, function (elm) {
                    return this.augmentToken(elm);
                }.bind(this));
                this.totalItems = data.total;
            },
            augmentToken: function (token) {
                token.lastLogin = moment(token.lastLogin);
                token.expirationDate = moment(token.expirationDate);
                token.createDate = moment(token.createDate);
                return token;
            },
            refresh: function () {
                this.fetch();
            }
        },
        created: function () {
            this.fetch();
        }
    }
</script>