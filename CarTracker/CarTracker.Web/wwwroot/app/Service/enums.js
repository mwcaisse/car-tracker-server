"use strict";

define("Service/enums", ["Service/constants"], function (constants) {

    return new (function () {
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

        self.LogType = {};
        self.LogType[constants.LOG_TYPE.DEBUG] = "Debug";
        self.LogType[constants.LOG_TYPE.INFO] = "Info";
        self.LogType[constants.LOG_TYPE.WARN] = "Warn";
        self.LogType[constants.LOG_TYPE.ERROR] = "Error";

    })();

});