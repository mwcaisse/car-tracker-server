"use strict";

define("Components/Admin/Log/RequestLogModal/RequestLogModal",
    ["moment", "Service/system", "Service/util", "Service/applicationProxy", "Service/navigation",
        "AMD/text!Components/Admin/Log/RequestLogModal/RequestLogModal.html",
        "Components/Admin/Log/RequestLogMixin",
        "Components/Common/Modal/Modal"],
    function (moment, system, util, proxy, navigation, template, requestLogMixin) {


        return Vue.component("app-request-log-modal",
        {
            mixins: [requestLogMixin],
            data: function() {
                return {
                    title: "View Request Log"
                }
            },
            computed: {
                requestDetailUrl: function() {
                    return navigation.viewRequestLogDetailsLink(this.requestUuid);
                    
                }
            },
            template: template,
            created: function () {
                system.bus.$on("requestLog:view", function (requestLogId) {
                    this.requestLogId = requestLogId;
                    this.fetchLog();
                    this.$refs.modal.open();
                }.bind(this));
            }
        });

    });