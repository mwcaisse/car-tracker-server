﻿"use strict";

define("Components/Admin/Log/RequestLogGrid/RequestLogGrid",
    ["moment", "Service/system", "Service/util", "Service/applicationProxy", "Service/navigation",
        "Components/Common/Pager/PagedGridMixin",
        "AMD/text!Components/Admin/Log/RequestLogGrid/RequestLogGrid.html",
        "Components/Common/ColumnHeader/ColumnHeader",
        "Components/Common/Pager/Pager"],
    function (moment, system, util, proxy, navigation, pagedGridMixin, template) {


        return Vue.component("app-request-log-grid", {
            mixins: [pagedGridMixin],
            data: function () {
                return {
                    logs: [],
                    currentSort: { propertyId: "CreateDate", ascending: false }
                }
            },
            template: template,
            methods: {
                fetch: function () {
                    proxy.requestLog.getAllPaged(this.startAt, this.take, this.currentSort).then(function (data) {
                        this.update(data);
                    }.bind(this), function (error) {
                        alert("error fetching request logs");
                    });
                },
                update: function (data) {
                    this.logs = $.map(data.data, function (elm, ind) {
                        return this.augmentLog(elm);
                    }.bind(this));
                    this.totalItems = data.total;
                },
                augmentLog: function (log) {
                    log.createDate = moment(log.createDate);

                    if (log.user == null || typeof log.user === "undefined") {
                        log.user = {
                            name: ""
                        };
                    }

                    return log;
                },
                refresh: function () {
                    this.fetch();
                }
            },
            created: function () {
                this.fetch();
            }
        });

    });