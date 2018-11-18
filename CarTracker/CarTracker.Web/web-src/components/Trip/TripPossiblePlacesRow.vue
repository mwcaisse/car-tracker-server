<template>
    <tr class="clickable" v-on:click="setPlace">
        <td>{{ placeName }}</td>
        <td>{{ distance | kmToMi | round(2) }} mi</td>
    </tr>
</template>

<script>
    export default {  
        data: function () {
            return {
                id: -1,
                tripId: -1,
                placeId: -1,
                placeType: "",
                distance: 0,
                placeName: ""
            };
        },
        props: {
            possiblePlace: {
                type: Object,
                requred: true
            }
        },     
        methods: {
            setPlace: function () {
                if (this.placeType === system.constants.TRIP_POSSIBLE_PLACE_TYPE.START) {
                    proxy.trip.setStartingPlace(this.tripId, this.placeId);
                }
                else {
                    proxy.trip.setDestinationPlace(this.tripId, this.placeId);
                }
                this.$emit("trip:possible-place:selected", this.possiblePlace);
            },
            update: function (data) {
                this.id = data.id;
                this.tripId = data.tripId;
                this.placeId = data.placeId;
                this.placeType = data.placeType;
                this.distance = data.distance;
                this.placeName = data.place.name;
            }
        },
        created: function () {
            this.update(this.possiblePlace);
        }
    }
</script>