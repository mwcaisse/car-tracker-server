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
            computed: {
                date: function() {
                    return this.value;
                }
            },
            props: {
                value: {
                    type: Date,
                    required: false
                }
            },
            template: template,
            methods: {
                updateValue: function(value) {
                    this.$emit("input", value);
                }
            },
            directives: {
                datepicker: {
                    bind: function (el, binding, vnode) {
                        $(el).datepicker().on("changeDate", function (e) {
                            vnode.context.updateValue(e.date);
                        });
                        $(el).datepicker("update", binding.value);
                    },
                    update: function (el, binding) {
                        $(el).datepicker("update", binding.value);
                    }
                }
            }

        });

    });