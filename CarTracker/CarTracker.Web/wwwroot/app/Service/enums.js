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

        self.CarMaintenanceType = {}
        self.CarMaintenanceType[constants.CAR_MAINTENANCE_TYPE.OIL_CHANGE] = "Oil Change";
        self.CarMaintenanceType[constants.CAR_MAINTENANCE_TYPE.TIRE_ROTATION] = "Tire Rotation";
        self.CarMaintenanceType[constants.CAR_MAINTENANCE_TYPE.AIR_FILTER_CHANGE] = "Air Filter Change";
        self.CarMaintenanceType[constants.CAR_MAINTENANCE_TYPE.CABIN_AIR_FILTER_CHANGE] = "Cabin Air Filter Change";
        self.CarMaintenanceType[constants.CAR_MAINTENANCE_TYPE.OTHER] = "Other";

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