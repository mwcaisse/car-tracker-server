"use strict";

define("Components/Common/PageAlert/PageAlert",
    ["moment", "Service/system", "Service/util",
        "AMD/text!Components/Common/PageAlert/PageAlert.html"],
    function (moment, system, util, template) {


        return Vue.component("app-page-alert", {
            data: function () {
                return {
                    type: "",
                    message: ""
                }
            },
            computed: {
                showAlert: function() {
                    return !util.isStringNullOrBlank(this.message);
                },
                alertClass: function() {
                    return "alert-" + this.type;
                }
            },
            template: template,
            methods: {
                show: function(message, type) {
                    this.message = message;
                    this.type = type || "info";
                },
                hide: function() {
                    this.message = "";
                    this.type = "info";
                }
            },
            created: function() {
                system.bus.$on(system.EVENT_ALERT_SHOW, function (params) {
                    this.show(params.message, params.type);
                }.bind(this));
            }
        });

    });