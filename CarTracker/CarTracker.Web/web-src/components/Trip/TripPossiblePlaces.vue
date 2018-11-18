<template>
    <app-modal :title="title" ref="modal">
        <div v-if="possiblePlaces.length > 0">
            <table class="table table-hover">
                <thead>
                    <tr>
                        <th><app-column-header columnId="Place.Name" columnName="Name" :enableSort="false"></app-column-header></th>
                        <th><app-column-header columnId="Distance" columnName="Distance" :currentSort="currentSort" v-on:sort:update="sortUpdated" v-on:sort:clear="sortCleared"></app-column-header></th>
                    </tr>
                </thead>
                <tbody>
                    <app-trip-possible-places-row v-for="possiblePlace in possiblePlaces"
                                                  :key="possiblePlace.placeId + '__' + possiblePlace.placeType"
                                                  :possiblePlace="possiblePlace"
                                                  v-on:trip:possible-place:selected="placeSelected">

                    </app-trip-possible-places-row>
                </tbody>
            </table>
            <app-pager :totalItems="totalItems" :currentPage="currentPage" :itemsPerPage="currentItemsPerPage" v-on:paging:update="pagingUpdated"></app-pager>
        </div>
        <div class="text-center" v-else>
            <p class="card-text">No Possible {{ typeDisplay }} are available for this trip!</p>
        </div>
    </app-modal>

</template>

<script>

    import System from "services/System.js"
    import * as FriendlyConstants from "services/FriendlyConstants.js"
    import * as Constants from "services/Constants.js"
    import { TripPossiblePlacesService } from "services/ApplicationProxy.js"

    import PagedGridMixin from "components/Common/PagedGridMixin.vue"
    import ColumnHeader from "components/Common/ColumnHeader.vue"
    import Modal from "components/Common/Modal.vue"
    import PlaceRow from "components/Trip/TripPossiblePlacesRow.vue"

    export default {
        mixins: [PagedGridMixin],
        components: {
            "app-column-header": ColumnHeader,
            "app-modal": Modal,
            "app-trip-possible-places-row": PlaceRow
        },
        data: function () {
            return {
                possiblePlaces: [],
                currentSort: {},
                type: ""
            }
        },
        props: {
            tripId: {
                type: Number,
                required: true
            }
        },
        computed: {
            typeDisplay: function () {
                if (this.type === Constants.TRIP_POSSIBLE_PLACE_TYPE.START) {
                    return "Starting Points";
                }
                else {
                    return "Destinations";
                }
            },
            title: function () {
                return "Possible " + this.typeDisplay;
            }
        },       
        methods: {
            fetchPossiblePlaces: function () {
                var type = FriendlyConstants.TRIP_POSSIBLE_PLACE_TYPE[this.type];
                return TripPossiblePlacesService.getForTrip(this.tripId, type, this.startAt,
                    this.take, this.currentSort).then(function (data) {
                        this.update(data);
                        return data;
                    }.bind(this),
                        function (error) {
                            System.showAlert(error, "error");
                            return error;
                        });
            },
            update: function (data) {
                this.possiblePlaces = data.data;
                this.totalItems = data.total;
            },
            refresh: function () {
                this.fetchPossiblePlaces();
            },
            placeSelected: function (place) {
                this.$emit("trip:possible-place:selected", {
                    selectedPlace: place,
                    placeType: this.type
                });
                this.$refs.modal.close();
            }
        },
        created: function () {
            System.events.$on("trip:possible-place:show-modal", function (type) {
                this.currentPage = 1;
                this.type = type;
                this.fetchPossiblePlaces().then(function () {
                    this.$refs.modal.open();
                }.bind(this));

            }.bind(this));
        }
    }
</script>