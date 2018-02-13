"use strict";

define("Components/Common/DatePicker/DatePicker",
    ["moment", "Service/system", "Service/util",
        "AMD/text!Components/Common/DatePicker/DatePicker.html"],
    function (moment, system, util, template) {

        return Vue.component("app-datepicker", {
            data: function () {
                return {
                   
                }
            },
            props: {
            },
            template: template,
            methods: {
            },
            directives: {
                datepicker: {
                    bind: function (el, binding) {
                        $(el).datepicker();
                    }
                }
            }

        });

    });