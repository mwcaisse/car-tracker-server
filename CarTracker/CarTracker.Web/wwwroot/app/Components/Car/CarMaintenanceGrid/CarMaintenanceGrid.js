"use strict";

define("Components/Car/CarMaintenanceGrid/CarMaintenanceGrid",
    ["moment", "Service/system", "Service/util", "Service/applicationProxy", "Service/navigation",
        "Components/Common/Pager/PagedGridMixin",
        "AMD/text!Components/Car/CarMaintenanceGrid/CarMaintenanceGrid.html",
        "Components/Common/ColumnHeader/ColumnHeader",
        "Components/Common/Pager/Pager",
        "Components/Car/CarMaintenanceModal/CarMaintenanceModal" ],

    function (moment, system, util, proxy, navigation, pagedGridMixin, template) {
        return Vue.component("app-car-maintenance-grid", {
            mixins: [pagedGridMixin],
            data: function () {
                return {
                    records: [],
                    currentSort: { propertyId: "Date", ascending: false }
                }
            },
            props: {
                carId: {
                    type: Number,
                    required: true
                }
            },
            template: template,
            methods: {
                fetch: function () {
                    proxy.carMaintenance.getForCar(this.carId, this.startAt, this.take, this.currentSort).then(function (data) {
                        this.update(data);
                    }.bind(this), function (error) {
                        system.showAlert(error, "error");
                    });
                },
                update: function (data) {
                    this.records = $.map(data.data, function (elm) {
                        return this.augmentRecord(elm);
                    }.bind(this));
                    this.totalItems = data.total;
                },
                augmentRecord: function (record) {
                    record.date = moment(record.date);
                    return record;
                },
                refresh: function () {
                    this.fetch();
                },
                viewRecord: function (record) {
                    system.bus.$emit("carMaintenance:edit", record.carMaintenanceId);
                },
                createRecord: function () {
                    system.bus.$emit("carMaintenance:create");
                },
                deleteRecord: function(record) {
                    proxy.carMaintenance.delete(this.carId, record.carMaintenanceId).then(function () {
                        var ind = this.records.indexOf(record);
                        this.records.splice(ind, 1);
                    }.bind(this), function (error) {
                        system.showAlert(error, "error");
                    });
                }
            },
            created: function () {
                this.fetch();
            }
        });

    });