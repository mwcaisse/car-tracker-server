"use strict";

define("Views/Admin/Log/ServerLogs/ServerLogs",
    ["Service/util",
        "Service/navigation",
        "AMD/text!Views/Admin/Log/ServerLogs/ServerLogs.html",
        "Components/Admin/Log/ServerLogGrid/ServerLogGrid"],

    function (util, navigation, template) {

        navigation.setActiveNavigation("Admin");

        return function (elementId) {

            return new Vue({
                el: elementId,
                template: template
            });
        };
    });