"use strict";

define("Views/Admin/Log/RequestLogs/RequestLogs",
    ["Service/util",
        "Service/navigation",
        "AMD/text!Views/Admin/Log/RequestLogs/RequestLogs.html",
        "Components/Admin/Log/RequestLogGrid/RequestLogGrid"],

    function (util, navigation, template) {


        var vm = function (elementId) {

            return new Vue({
                el: elementId,
                template: template
            });
        };

        navigation.setActiveNavigation("Admin");

        return vm;

    });