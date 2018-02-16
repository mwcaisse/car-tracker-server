"use strict";

define("Views/Admin/Log/RequestLogDetail/RequestLogDetail",
    ["Service/util",
        "Service/navigation",
        "AMD/text!Views/Admin/Log/RequestLogDetail/RequestLogDetail.html",
        "Components/Admin/Log/RequestLogDetail/RequestLogDetail"],

    function (util, navigation, template) {

        navigation.setActiveNavigation("Admin");

        var requestGuid = $("#requestGuid").val();
        return function (elementId) {

            return new Vue({
                el: elementId,
                template: template,
                data: {
                    requestGuid: requestGuid
                }
            });
        };
    });