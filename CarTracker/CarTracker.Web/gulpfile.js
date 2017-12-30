/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. https://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var gulpClean = require("gulp-clean");
var gulpData = require("gulp-data");
var gulpRename = require("gulp-rename");
var merge = require("merge-stream");
var path = require("path");
var environments = require("gulp-environments");

var libs = './wwwroot/lib/';


gulp.task('default', function () {
    // place code for your default task here
});

gulp.task('restore:bootstrap', function () {
    return gulp.src([
        'node_modules/bootstrap/dist/**/*.*'
    ]).pipe(gulp.dest(libs + 'bootstrap'));
});

gulp.task('restore:font-awesome', function () {
    var css = gulp.src([
        'node_modules/font-awesome/css/*.*'
    ]).pipe(gulp.dest(libs + 'font-awesome/css'));

    var other = gulp.src([
        'node_modules/font-awesome/fonts/*.*'
    ]).pipe(gulp.dest(libs + 'font-awesome/fonts'));

    return merge(css, other);
});

gulp.task('restore:highcharts', function () {
    var css = gulp.src([
        'node_modules/highcharts/*.css'
    ]).pipe(gulp.dest(libs + 'highcharts/css'));

    var js = gulp.src([
        'node_modules/highcharts/*.js'
    ]).pipe(gulp.dest(libs + 'highcharts/js'));

    return merge(css, js);
});

gulp.task('restore:jquery', function () {
    return gulp.src([
        'node_modules/jquery/dist/*.js'
    ]).pipe(gulp.dest(libs + 'jquery/js'));
});

gulp.task('restore:jquery-ui', function () {
    var css = gulp.src([
        'node_modules/jquery-ui-dist/*.css'
    ]).pipe(gulp.dest(libs + 'jquery-ui/css'));

    var js = gulp.src([
        'node_modules/jquery-ui-dist/*.js'
    ]).pipe(gulp.dest(libs + 'jquery-ui/js'));

    return merge(css, js);
});

gulp.task('restore:lodash', function () {
    return gulp.src([
        'node_modules/lodash/lodash.js',
        'node_modules/lodash/lodash.min.js'
    ]).pipe(gulp.dest(libs + 'lodash/js'));
});

gulp.task('restore:moment', function () {
    return gulp.src([
        'node_modules/moment/moment.js',
        'node_modules/moment/min/moment.min.js'
    ]).pipe(gulp.dest(libs + 'moment/js'));
});

gulp.task('restore:moment-duration-format', function () {
    return gulp.src([
        'node_modules/moment-duration-format/lib/moment-duration-format.js'
    ]).pipe(gulp.dest(libs + 'moment-duration-format/js'));
});

gulp.task('restore:moment-timezone', function () {
    return gulp.src([
        'node_modules/moment-timezone/moment-timezone*.js'
    ]).pipe(gulp.dest(libs + 'moment-timezone/js'));
});

gulp.task('restore:popperjs', function () {
    return gulp.src([
        'node_modules/popper.js/dist/*.js'
    ]).pipe(gulp.dest(libs + 'popperjs/js'));
});

gulp.task('restore:q', function () {
    return gulp.src([
        'node_modules/q/q.js'
    ]).pipe(gulp.dest(libs + 'q/js'));
});

gulp.task('restore:requirejs', function () {
    return gulp.src([
        'node_modules/requirejs/require.js'
    ]).pipe(gulp.dest(libs + 'require/js'));
});

gulp.task('restore:vue', function () {
    return gulp.src([
        'node_modules/vue/dist/vue.js',
        'node_modules/vue/dist/vue.min.js'
    ]).pipe(gulp.dest(libs + 'vue/js'));
});

gulp.task('restore', [
    'restore:bootstrap',
    'restore:font-awesome',
    'restore:highcharts', 
    'restore:jquery',
    'restore:jquery-ui',
    'restore:lodash',
    'restore:moment',
    'restore:moment-duration-format',
    'restore:moment-timezone',
    'restore:popperjs',
    'restore:q',
    'restore:requirejs',
    'restore:vue'
]);