<script>
    import * as Util from "services/Util.js"
    import TripChartMixin from "components/Trip/TripChart/TripChartMixin.vue"

    export default {
        mixins: [TripChartMixin],
        data: function () {
            return {
                name: "Speed"
            }
        },
        computed: {
            chartOptions: function () {
                var opts = {};
                var useImperial = true;
                var units = useImperial ? "mph" : "km/h";

                opts.title = {
                    text: this.name
                };

                var data = $.map(this.readings, function (elm) {
                    var speed = elm.speed;
                    if (useImperial) {
                        speed = Util.convertKmToMi(speed);
                    }
                    speed = Math.round(speed);
                    return { x: elm.readDate, y: speed };
                });

                opts.plotOptions = {
                    series: {
                        turboThreshold: 0
                    }
                };

                opts.yAxis = {
                    title: {
                        text: "Speed " + units
                    }
                };

                opts.xAxis = {
                    type: "datetime",
                    startOfWeek: 0
                };

                opts.tooltip = {
                    valueSuffix: " " + units
                };

                opts.series = [{
                    name: "Speed",
                    data: data
                }];

                return opts;
            }
        }
    }
</script>