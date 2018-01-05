"use strict";

define("Service/enums", ["Service/constants"], function (constants) {

    var enums = new (function () {
        var self = this;

        self.TripStatus = {};
        self.TripStatus[constants.TRIP_STATUS.NEW] = "New";
        self.TripStatus[constants.TRIP_STATUS.STARTED] = "Started";
        self.TripStatus[constants.TRIP_STATUS.FINISHED] = "Finished";
        self.TripStatus[constants.TRIP_STATUS.PROCESSED] = "Processed";
        self.TripStatus[constants.TRIP_STATUS.FAILED] = "Failed";

        self.TripPossiblePlaceType = {};
        self.TripPossiblePlaceType[constants.TRIP_POSSIBLE_PLACE_TYPE.START] = "Start";
        self.TripPossiblePlaceType[constants.TRIP_POSSIBLE_PLACE_TYPE.DESTINATION] = "Destination";

    })();

    return enums;

});