<script>
    import { DEGREE_SYMBOL } from "services/Constants.js"
    import * as Util from "services/Util.js"
    import TripChartMixin from "components/Trip/TripChart/TripChartMixin.vue"

    export default {
        mixins: [TripChartMixin],
        data: function () {
            return {
                name: "Temperatures"
            }
        },
        computed: {
            chartOptions: function () {
                var opts = {};
                var useImperial = true;
                var units = DEGREE_SYMBOL + (useImperial ? "F" : "C");

                opts.title = {
                    text: this.name
                };

                var ambientAirTemperature = $.map(this.readings, function (elm) {
                    return this.createDataElement(elm, "ambientAirTemperature", useImperial);
                }.bind(this));

                var engineCoolantTemperature = $.map(this.readings, function (elm) {
                    return this.createDataElement(elm, "engineCoolantTemperature", useImperial);
                }.bind(this));

                var oilTemperature = $.map(this.readings, function (elm) {
                    return this.createDataElement(elm, "oilTemperature", useImperial);
                }.bind(this));

                opts.plotOptions = {
                    series: {
                        turboThreshold: 0
                    }
                };

                opts.yAxis = {
                    title: {
                        text: "Temperature " + units
                    }
                };

                opts.xAxis = {
                    type: "datetime",
                    startOfWeek: 0
                };

                opts.tooltip = {
                    valueSuffix: " " + units
                };

                opts.series = [
                    {
                        name: "Ambient Air Temperature",
                        data: ambientAirTemperature
                    },
                    {
                        name: "Engine Coolant Temperature",
                        data: engineCoolantTemperature
                    },
                    {
                        name: "Oil Temperature",
                        data: oilTemperature
                    }];

                return opts;
            }
        },
        methods: {
            createDataElement: function (reading, fieldName, useImperial) {
                var temp = reading[fieldName];
                if (useImperial) {
                    temp = Util.convertCelsiusToFah(temp);
                }
                temp = Math.round(temp);
                return { x: reading.readDate, y: temp };
            }
        }
    }
</script>