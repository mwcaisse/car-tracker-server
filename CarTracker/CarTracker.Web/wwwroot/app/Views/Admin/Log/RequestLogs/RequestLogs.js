"use strict";

define("Views/Admin/Log/RequestLogs/RequestLogs",
    ["Service/util",
        "Service/navigation",
        "AMD/text!Views/Admin/Log/RequestLogs/RequestLogs.html",
        "Components/Admin/Log/RequestLogGrid/RequestLogGrid"],

    function (util, navigation, template) {

        navigation.setActiveNavigation("Admin");

        return function (elementId) {

            return new Vue({
                el: elementId,
                template: template
            });
        };
    });