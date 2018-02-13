"use strict";

define("Components/Common/CollapsibleCard/CollapsibleCard",
    ["moment", "Service/system", "Service/util",
        "AMD/text!Components/Common/CollapsibleCard/CollapsibleCard.html"],
    function (moment, system, util, template) {


        return Vue.component("app-collapsible-card", {
            data: function () {
                return {
                    mexpanded: this.expanded
                }
            },
            props: {
                title: {
                    type: String,
                    required: true
                },
                expanded: {
                    type: Boolean,
                    required: false,
                    default: true
                }
            },
            template: template,
            methods: {
                collapse: function () {
                    this.mexpanded = false;
                },
                expand: function () {
                    this.mexpanded = true;
                }
            }
        });

    });