<script>
    import TripChartMixin from "components/Trip/TripChart/TripChartMixin.vue"

    export default {
        mixins: [TripChartMixin],
        data: function () {
            return {
                name: "Throttle Position"
            }
        },
        computed: {
            chartOptions: function () {
                var opts = {};
                var units = "%";

                opts.title = {
                    text: this.name
                };

                var data = $.map(this.readings, function (elm) {
                    var throttlePosition = Math.round(elm.throttlePosition);
                    return { x: elm.readDate, y: throttlePosition };
                });

                opts.plotOptions = {
                    series: {
                        turboThreshold: 0
                    }
                };

                opts.yAxis = {
                    title: {
                        text: "Throttle Position (" + units + ")"
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
                    name: "Throttle Position",
                    data: data
                }];

                return opts;
            }
        }
    }
</script>