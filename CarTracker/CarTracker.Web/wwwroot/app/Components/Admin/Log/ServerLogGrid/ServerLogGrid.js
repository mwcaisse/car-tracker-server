"use strict";

define("Components/Admin/Log/ServerLogGrid/ServerLogGrid",
    ["moment", "Service/system", "Service/util", "Service/applicationProxy", "Service/navigation",
        "Components/Common/Pager/PagedGridMixin",
        "AMD/text!Components/Admin/Log/ServerLogGrid/ServerLogGrid.html",
        "Components/Common/ColumnHeader/ColumnHeader",
        "Components/Common/Pager/Pager",
        "Components/Admin/Log/RequestLogModal/RequestLogModal"],
    function (moment, system, util, proxy, navigation, pagedGridMixin, template) {


        return Vue.component("app-server-log-grid", {
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
                    proxy.serverLog.getAllPaged(this.startAt, this.take, this.currentSort,
                        this.currentFilter).then(function (data) {

                        this.update(data);
                    }.bind(this), function (error) {
                        system.showAlert(error, "error");
                    });
                },
                update: function (data) {
                    this.logs = $.map(data.data, function (elm) {
                        return this.augmentLog(elm);
                    }.bind(this));
                    this.totalItems = data.total;
                },
                augmentLog: function (log) {
                    log.createDate = moment(log.createDate);
                    return log;
                },
                refresh: function () {
                    this.fetch();
                },
                viewServerLog: function (id) {
                    system.bus.$emit("serverLog:view", id);
                },
                viewRequestLog: function (uuid) {
                    system.bus.$emit("requestLog:view:uuid", uuid);
                },
                getLogTypeOptions: function () {
                    return Q.fcall(function () {
                        return $.map(system.enums.LogType, function (val, key) {
                            return {
                                value: key,
                                display: val
                            }
                        });
                    });
                }
            },
            created: function () {
                this.fetch();
            }
        });

    });