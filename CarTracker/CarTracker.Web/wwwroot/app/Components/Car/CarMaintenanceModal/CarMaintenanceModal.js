"use strict";

define("Components/Car/CarMaintenanceModal/CarMaintenanceModal",
    ["moment", "Service/system", "Service/util", "Service/applicationProxy", "Service/navigation",
        "AMD/text!Components/Car/CarMaintenanceModal/CarMaintenanceModal.html",
        "Components/Common/Modal/Modal",
        "Components/Common/DatePicker/DatePicker" ],
    function (moment, system, util, proxy, navigation, template) {

        return Vue.component("app-car-maintenance-modal", {
            data: function () {
                return {
                    title: "Add Car Maintenance",
                    carMaintenanceId: -1,
                    type: -1,
                    notes: "",
                    date: new Date(),
                    mileage: 0
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
                fetchRecord: function () {
                    proxy.carMaintenance.get(this.carId, this.carMaintenanceId).then(function (data) {
                        this.update(data);
                    }.bind(this), function (error) {
                        system.showAlert(error, "error");
                    });
                },
                update: function (record) {
                    this.carMaintenanceId = record.carMaintenanceId;
                    this.type = record.type;
                    this.notes = record.notes;
                    this.date = new Date(record.date);
                    this.mileage = record.mileage;
                },
                clear: function () {
                    this.carMaintenanceId = -1;
                    this.type = -1;
                    this.notes = "";
                    this.date = new Date();
                    this.mileage = 0;
                },
                createModel: function () {
                    return {
                        carMaintenanceId: this.carMaintenanceId,
                        carId: this.carId,
                        type: this.type,
                        notes: this.notes,
                        date: this.date.valueOf(),
                        mileage: this.mileage
                    };
                },
                save: function () {
                    var func;
                    if (this.carMaintenanceId < 0) {
                        func = proxy.carMaintenance.create;
                    }
                    else {
                        func = proxy.carMaintenance.update;
                    }

                    func(this.carId, this.createModel()).then(function () {
                        this.$emit("carMaintenance:updated");
                        this.$refs.modal.close();
                    }.bind(this), function (error) {
                        system.showAlert(error, "error");
                    });
                },
                refresh: function () {
                    this.fetchRecord();
                }
            },
            created: function () {
                system.bus.$on("carMaintenance:create", function () {
                    this.$refs.modal.open();
                    this.title = "Add Maintenance Record";
                }.bind(this));

                system.bus.$on("carMaintenance:edit", function (maintenanceId) {
                    this.carMaintenanceId = maintenanceId;
                    this.fetchRecord();
                    this.title = "Edit Maintenance Record";
                    this.$refs.modal.open();
                }.bind(this));
            }
        });

    });