"use strict";

define("Service/constants", [], function () {

    var constants = new (function () {
        var self = this;

        self.TRIP_STATUS = {
            NEW: 1,
            STARTED: 2,
            FINISHED: 3,
            PROCESSED: 4
        };

        self.TRIP_STATUSES = [
            self.TRIP_STATUS.NEW,
            self.TRIP_STATUS.STARTED,
            self.TRIP_STATUS.FINISHED,
            self.TRIP_STATUS.PROCESSED
        ];

        self.TRIP_POSSIBLE_PLACE_TYPE = {
            START: 1,
            DESTINATION: 2
        };

        self.TRIP_POSSIBLE_PLACE_TYPES = [
            self.TRIP_POSSIBLE_PLACE_TYPE.START,
            self.TRIP_POSSIBLE_PLACE_TYPE.DESTINATION
        ];

        self.LOG_TYPE = {
            DEBUG: 1,
            INFO: 2,
            WARN: 3,
            ERROR: 4
        };

        self.LOG_TYPES = [
            self.LOG_TYPE.DEBUG,
            self.LOG_TYPE.INFO,
            self.LOG_TYPE.WARN,
            self.LOG_TYPE.ERROR
        ];

    })();

    return constants;

});