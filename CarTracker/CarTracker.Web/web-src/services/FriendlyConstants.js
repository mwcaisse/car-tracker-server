import * as Constants from "services/Constants.js"

const TRIP_STATUS = {
    [Constants.TRIP_STATUS.NEW]: "New",
    [Constants.TRIP_STATUS.STARTED]: "Started",
    [Constants.TRIP_STATUS.FINISHED]: "Finished",
    [Constants.TRIP_STATUS.PROCESSED]: "Processed",
    [Constants.TRIP_STATUS.FAILED]: "Failed" 
};


const CAR_MAINTENANCE_TYPE = {
    [Constants.CAR_MAINTENANCE_TYPE.OIL_CHANGE]: "Oil Change",
    [Constants.CAR_MAINTENANCE_TYPE.TIRE_ROTATION]: "Tire Rotation",
    [Constants.CAR_MAINTENANCE_TYPE.AIR_FILTER_CHANGE]: "Air Filter Change",
    [Constants.CAR_MAINTENANCE_TYPE.CABIN_AIR_FILTER_CHANGE]: "Cabin Air Filter Change",
    [Constants.CAR_MAINTENANCE_TYPE.OTHER]: "Other"
};

const TRIP_POSSIBLE_PLACE_TYPE = { 
    [Constants.TRIP_POSSIBLE_PLACE_TYPE.START]: "Start",
    [Constants.TRIP_POSSIBLE_PLACE_TYPE.DESTINATION]: "Destination"
};


const LOG_TYPE = {
    [Constants.LOG_TYPE.DEBUG]: "Debug",
    [Constants.LOG_TYPE.INFO]: "Info",
    [Constants.LOG_TYPE.WARN]: "Warn",
    [Constants.LOG_TYPE.ERROR]: "Error"
};

export {
    TRIP_STATUS,
    CAR_MAINTENANCE_TYPE,
    TRIP_POSSIBLE_PLACE_TYPE,
    LOG_TYPE
};
