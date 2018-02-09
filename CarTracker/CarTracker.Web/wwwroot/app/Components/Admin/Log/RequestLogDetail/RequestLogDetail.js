"use strict";

define("Components/Admin/Log/RequestLogDetail/RequestLogDetail",
    ["moment", "Service/system", "Service/util", "Service/applicationProxy", "Service/navigation",
        "AMD/text!Components/Admin/Log/RequestLogDetail/RequestLogDetail.html",
        "Components/Admin/Log/RequestLogMixin",
        "Components/Common/CollapsibleCard/CollapsibleCard"],
    function (moment, system, util, proxy, navigation, template, requestLogMixin) {

        return Vue.component("app-request-log-detail", {
            mixins: [requestLogMixin],
            data: function () {
                return {};
            },
            props: {
                eventUuid: {
                    type: String,
                    requred: false
                }
            },
            template: template,
            created: function () {
                this.requestUuid = this.eventUuid;
                this.refresh();
            }
        });

    });