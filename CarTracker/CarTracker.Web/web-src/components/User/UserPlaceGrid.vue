<template>
    <div class="card">
        <div class="card-header">
            Custom Places
            <span class="float-right">
                <i class="fa fa-refresh action-icon" aria-hidden="true" v-on:click="refresh" v-tooltip title="Refresh"></i>
            </span>
        </div>
        <div v-if="places.length > 0">
            <table class="table table-striped table-bordered">
                <thead>
                    <tr>
                        <th style="width: 20%;">
                        </th>
                        <th>
                            <app-column-header columnId="Name"
                                               columnName="Name"
                                               :currentSort="currentSort"
                                               v-on:sort:update="sortUpdated"
                                               v-on:sort:clear="sortCleared">

                            </app-column-header>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="place in places">
                        <td>
                            <i class="fa fa-th-list action-icon" aria-hidden="true" v-on:click="viewUserPlace(place)" v-tooltip title="View"></i>
                            <i class="fa fa-trash action-icon" aria-hidden="true" v-on:click="deleteUserPlace(place)" v-tooltip title="Delete"></i>
                        </td>
                        <td> {{ place.name }} </td>
                    </tr>
                </tbody>
            </table>
            <app-pager :totalItems="totalItems" :currentPage="currentPage" :itemsPerPage="currentItemsPerPage" v-on:paging:update="pagingUpdated"></app-pager>
        </div>
        <div class="card-body text-center" v-else>
            <p class="card-text">You haven't created any custom places yet.</p>
        </div>
    </div>
</template>

<script>
    import System from "services/System.js"
    import { UserPlaceService } from "services/ApplicationProxy.js"

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
                places: [],
                currentSort: { propertyId: "CreateDate", ascending: true }
            }
        },
        methods: {
            fetch: function () {
                UserPlaceService.getMinePaged(this.startAt, this.take, this.currentSort)
                    .then(function (data) {
                        this.update(data);
                    }.bind(this), function (error) {
                        System.showAlert(error, "error");
                    });
            },
            update: function (data) {
                this.places = data.data;
                this.totalItems = data.total;
            },
            refresh: function () {
                this.fetch();
            },
            viewUserPlace: function (place) {
                System.events.$emit("userPlace:edit", place.userPlaceId);
            },
            deleteUserPlace: function (place) {
                UserPlaceService.delete(place.userPlaceId).then(function () {
                    var ind = this.places.indexOf(place);
                    this.places.splice(ind, 1);
                }.bind(this), function (error) {
                    System.showAlert(error, "error");
                });
            }
        },
        created: function () {
            this.fetch();

            System.events.$on("userPlace:updated", function () {
                this.fetch();
            }.bind(this));
        }
    }
</script>