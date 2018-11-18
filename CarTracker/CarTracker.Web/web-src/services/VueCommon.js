import Vue from "vue"
import Highcharts from "highcharts"


function config() {

    //Highcharts options
    Highcharts.setOptions({
        global: {
            useUTC: false
        }
    });

}

export default config