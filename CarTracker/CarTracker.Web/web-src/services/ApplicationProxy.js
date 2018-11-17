import Proxy from "services/Proxy.js"

var CarService = {
    get: function (id) {
        return Proxy.get(`car/${id}`);
    },
    getByVin: function (vin) {
        return Proxy.get(`car/vin/${vin}`);
    },
    getAll: function () {
        return Proxy.get("car/");
    },
    getAllPaged: function (startAt, maxResults, sort) {
        return Proxy.getPaged("car/", startAt, maxResults, sort);
    },
    create: function (car) {
        return Proxy.post("car/", car);
    },
    update: function (car) {
        return Proxy.put("car/", car);
    },
    getSupportedCommands: function (id) {
        return Proxy.get(`car/${id}/supported-commands/`);
    }
};

var CarMaintenanceService = {
    get: function (carId, id) {
        return Proxy.get(`car/${carId}/maintenance/${id}`);
    },
    getForCar: function (carId, startAt, maxResults, sort) {
        return Proxy.getPaged(`car/${carId}/maintenance/`, startAt, maxResults, sort);
    },
    create: function (carId, maintenance) {
        return Proxy.post(`car/${carId}/maintenance/`, maintenance);
    },
    update: function (carId, maintenance) {
        return Proxy.put(`car/${carId}/maintenance/`, maintenance);
    },
    delete: function (carId, id) {
        return Proxy.delete(`car/${carId}/maintenance/${id}`);
    }
};

var TripService = {
    get: function (id) {
        return Proxy.get(`trip/${id}`);
    },
    getAll: function () {
        return Proxy.get("trip/");
    },
    getAllForCar: function (carId) {
        return Proxy.get(`car/${carId}/trip/`);
    },
    getAllForCarPaged: function (carId, startAt, maxResults, sort) {
        return Proxy.getPaged(`car/${carId}/trip/`, startAt, maxResults, sort);
    },
    getPreviousNext: function (id) {
        return Proxy.get(`trip/${id}/prevnext`);
    },
    update: function (trip) {
        return Proxy.put("trip/", trip);
    },
    process: function (id) {
        return Proxy.post(`trip/${id}/process`);
    },
    processUnprocessed: function () {
        return Proxy.post("trip/process/unprocessed");
    },
    setStartingPlace: function (id, placeId) {
        return Proxy.post(`trip/${id}/starting-place?placeId=${placeId}`);
    },
    setDestinationPlace: function (id, placeId) {
        return Proxy.post(`trip/${id}/destination-place?placeId=${placeId}`);
    }
};

var TripPossiblePlacesService = {
    getForTrip: function (tripId, type, startAt, maxResults, sort) {
        return Proxy.getPaged(`trip/${tripId}/possible-places/${type}`, startAt, maxResults, sort);
    }
};

var UserPlacesService = {
    get: function(id) {
        return Proxy.get(`place/user/${id}`);
    },
    getMinePaged: function(startAt, maxResults, sort) {
        return Proxy.getPaged("place/user/", startAt, maxResults, sort);
    },
    create: function(toCreate) {
        return Proxy.post("place/user/", toCreate);
    },
    update: function(toUpdate) {
        return Proxy.put("place/user", toUpdate);
    },
    delete: function(id) {
        return Proxy.delete(`place/user/${id}`);
    }
};

var ReadingService = {
    get: function (id) {
        return Proxy.get(`reading/${id}`);
    },
    getAllForTrip: function (tripId) {
        return Proxy.get(`trip/${tripId}/reading/`);
    }
};

var UserService = {
    register: function (registration) {
        return Proxy.post("user/register", registration);
    },
    available: function (username) {
        return Proxy.get(`user/available?username=${username}`);
    },
    me: function () {
        return Proxy.get("user/me", {});
    }
};

var RegistrationKeyService = {
    get: function (id) {
        return Proxy.get(`registration-key/${id}`);
    },
    getAllPaged: function (startAt, maxResults, sort) {
        return Proxy.getPaged("registration-key/", startAt, maxResults, sort);
    },
    create: function (toCreate) {
        return Proxy.post("registration-key/", toCreate);
    },
    update: function (toUpdate) {
        return Proxy.put("registration-key/", toUpdate);
    }
};

var RequestLogService = {
    get: function (id) {
        return Proxy.get(`log/request/${id}`);
    },
    getAllPaged: function (startAt, maxResults, sort, filter) {
        return Proxy.getPaged("log/request/", startAt, maxResults, sort, filter);
    },
    getByEventId: function (eventId) {
        return Proxy.get(`log/event/${eventId}/request/`);
    },
    getMethodFilters: function () {
        return Proxy.get("log/request/filters/method");
    },
    getStatusFilters: function () {
        return Proxy.get("log/request/filters/status");
    }
};

var ServerLogService = {
    get: function (id) {
        return Proxy.get(`log/server/${id}`);
    },
    getAllPaged: function (startAt, maxResults, sort, filter) {
        return Proxy.getPaged("log/server/", startAt, maxResults,
            sort, filter);
    },
    getForEvent: function (eventId, startAt, maxResults, sort, filter) {
        return Proxy.getPaged(`log/event/${eventId}/server/`,
            startAt, maxResults, sort, filter);
    }
};

var AuthTokenService = {
    getAllActivePaged: function (startAt, maxResults, sort) {
        return Proxy.getPaged("user/token/active", startAt, maxResults, sort);
    }
};

var ReaderLogService = {
    getAllPaged: function (startAt, maxResults, sort, filter) {
        return Proxy.getPaged("log/reader/", startAt, maxResults, sort, filter);
    }
};

export {
    CarService,
    CarMaintenanceService,
    TripService,
    TripPossiblePlacesService,
    UserPlacesService,
    ReadingService,
    UserService,
    RegistrationKeyService,
    RequestLogService,
    ServerLogService,
    AuthTokenService,
    ReaderLogService
};