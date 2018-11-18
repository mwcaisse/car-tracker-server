<script>
    import TripChartMixin from "components/Trip/TripChart/TripChartMixin.vue"

    export default {
        mixins: [TripChartMixin],
        data: function () {
            return {
                name: "Engine Speed"
            }
        },
        computed: {
            chartOptions: function () {
                var opts = {};

                opts.title = {
                    text: this.name
                };

                var data = $.map(this.readings, function (elm) {
                    return { x: elm.readDate, y: elm.engineRpm };
                });

                opts.plotOptions = {
                    series: {
                        turboThreshold: 0
                    }
                };

                opts.yAxis = {
                    title: {
                        text: "Engine Speed (RPM)"
                    }
                };

                opts.xAxis = {
                    type: "datetime",
                    startOfWeek: 0
                };

                opts.tooltip = {
                    valueSuffix: " RPM"
                };

                opts.series = [{
                    name: "Engine Speed",
                    data: data
                }];

                return opts;
            }
        }
    }
</script>