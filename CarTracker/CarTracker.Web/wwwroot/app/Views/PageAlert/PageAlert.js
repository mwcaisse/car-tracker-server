"use strict";

define("Views/PageAlert/PageAlert",
    ["Service/util",
        "AMD/text!Views/PageAlert/PageAlert.html",
        "Components/Common/PageAlert/PageAlert"],
    function (util, template) {

        return function (elementId) {

            return new Vue({
                el: elementId,
                template: template
            });

        };

    });