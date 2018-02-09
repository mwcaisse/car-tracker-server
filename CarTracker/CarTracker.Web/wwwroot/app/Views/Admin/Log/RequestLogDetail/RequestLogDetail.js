"use strict";

define("Views/Admin/Log/RequestLogDetail/RequestLogDetail",
    ["Service/util",
        "Service/navigation",
        "AMD/text!Views/Admin/Log/RequestLogDetail/RequestLogDetail.html",
        "Components/Admin/Log/RequestLogDetail/RequestLogDetail"],

    function (util, navigation, template) {

        var requestGuid = $("#requestGuid").val();

        var vm = function (elementId) {

            return new Vue({
                el: elementId,
                template: template,
                data: {
                    requestGuid: requestGuid
                }
            });
        };

        navigation.setActiveNavigation("Admin");

        return vm;

    });