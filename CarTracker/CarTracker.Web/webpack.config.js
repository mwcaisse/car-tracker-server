"use strict";

const { VueLoaderPlugin } = require("vue-loader");
const path = require("path");


module.exports = {
    mode: "development",
    entry: {
        "navigation": "./web-src/views/Navigation/Navigation.js",
        "home": "./web-src/views/Home/Home.js",
        "car": "./web-src/views/Car/Car.js",
        "trip": "./web-src/views/Trip/Trip.js"
    },
    output: {
        path: path.resolve(__dirname, "wwwroot"),
        filename: "views/[name].js"
    },
    module: {
        rules: [
            { test: /\.vue$/, use: "vue-loader" },
            {
                test: /\.css$/, use: [
                    "vue-style-loader",
                    "css-loader"
                ]
            }
        ]
    },
    plugins: [
        new VueLoaderPlugin()
    ],
    resolve: {
        modules: [
            path.resolve("./web-src"),
            path.resolve("./node_modules")
        ]
    }
};