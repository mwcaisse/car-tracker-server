"use strict";

define("Components/Admin/Log/RequestLogModal/RequestLogModal",
    ["moment", "Service/system", "Service/util", "Service/applicationProxy", "Service/navigation",
        "AMD/text!Components/Admin/Log/RequestLogModal/RequestLogModal.html",
        "Components/Common/Modal/Modal"],
    function (moment, system, util, proxy, navigation, template) {


        return Vue.component("app-request-log-modal", {
            data: function () {
                return {
                    title: "View Request Log",
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
            template: template,
            methods: {
                fetchLog: function () {
                    proxy.requestLog.get(this.requestLogId).then(function(data) {
                        this.update(data);
                    }.bind(this), function(error) {
                        alert("error fetching request log!");
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
            },
            created: function () {
                system.bus.$on("requestLog:view", function (requestLogId) {
                    this.requestLogId = requestLogId;
                    this.fetchLog();
                    this.$refs.modal.open();
                }.bind(this));
            }
        });

    });