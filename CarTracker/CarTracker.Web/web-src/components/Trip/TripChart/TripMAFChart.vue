<script>
    import TripChartMixin from "components/Trip/TripChart/TripChartMixin.vue"

    export default {
        mixins: [TripChartMixin],
        data: function () {
            return {
                name: "Mass Air Flow"
            }
        },
        computed: {
            chartOptions: function () {
                var opts = {};
                var units = "";

                opts.title = {
                    text: this.name
                };

                var data = $.map(this.readings, function (elm) {
                    return { x: elm.readDate, y: elm.massAirFlow };
                });

                opts.plotOptions = {
                    series: {
                        turboThreshold: 0
                    }
                };

                opts.yAxis = {
                    title: {
                        text: "MAF " + units
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
                    name: "MAF",
                    data: data
                }];

                return opts;
            }
        }
    }
</script>