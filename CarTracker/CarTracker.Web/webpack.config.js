"use strict";

const { VueLoaderPlugin } = require("vue-loader");
const path = require("path");

const glob = require("glob");
var mypath = path.resolve(__dirname, "wwwroot");

var views = {};
for (var file of glob.sync("./web-src/views/**/*.vue")) {
    views[file.replace("./web-src/", "").replace(".vue", "")] = file;    
}

//console.log(JSON.stringify(views));

module.exports = {
    mode: "development",
    entry: views,
    output: {
        path: path.resolve(__dirname, "wwwroot"),
        filename: "[name].js",
        libraryTarget: "amd",
        library: "[name]",
        umdNamedDefine: true
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