;"use strict";

define("Components/Admin/Log/RequestLogMixin",
    ["moment", "Service/system", "Service/util", "Service/applicationProxy"],
    function (moment, system, util, proxy) {

        return {
            data: function () {
                return {
                    requestLogId: -1,
                    requestUuid: "",
                    type: 0,
                    clientAddress: "",
                    requestUrl: "",
                    requestMethod: "",
                    requestHeaders: "",
                    requestBody: "",
                    responseStatus: "",
                    responseHeaders: "",
                    responseBody: "",
                    createDate: "",
                    userId: -1,
                    user: {}
                }
            },
            methods: {
                fetchLog: function () {
                    var promise;
                    if (this.requestLogId !== -1) {
                        promise = proxy.requestLog.get(this.requestLogId);
                    } else {
                        promise = proxy.requestLog.getByEventId(this.requestUuid);
                    }
                    promise.then(function (data) {
                        this.update(data);
                    }.bind(this), function (error) {
                        system.showAlert(error, "error");
                    });
                },
                update: function (log) {
                    this.requestLogId = log.requestLogId;
                    this.requestUuid = log.requestUuid;
                    this.type = log.type;
                    this.clientAddress = log.clientAddress;
                    this.requestUrl = log.requestUrl;
                    this.requestMethod = log.requestMethod;
                    this.requestHeaders = log.requestHeaders;
                    this.requestBody = log.requestBody;
                    this.responseStatus = log.responseStatus;
                    this.responseHeaders = log.responseHeaders;
                    this.responseBody = log.responseBody;
                    this.createDate = log.createDate;
                    this.userId = log.userId;
                    this.user = log.user || {};
                },
                clear: function () {
                    this.requestLogId = -1;
                    this.requestUuid = "";
                    this.type = 0;
                    this.clientAddress = "";
                    this.requestUrl = "";
                    this.requestMethod = "";
                    this.requestHeaders = "";
                    this.requestBody = "";
                    this.responseStatus = "";
                    this.responseHeaders = "";
                    this.responseBody = "";
                    this.createDate = "";
                    this.userId = -1;
                    this.user = {};
                },
                refresh: function () {
                    this.fetchLog();
                }
            }
        };

    });