"use strict";

define("Components/Log/ReaderLogGrid/ReaderLogGrid", 
		["moment", "Service/system", "Service/util", "Service/applicationProxy", "Service/navigation", 	
		 "Components/Common/Pager/PagedGridMixin",
		 "AMD/text!Components/Log/ReaderLogGrid/ReaderLogGrid.html",     
         "Components/Common/ColumnHeader/ColumnHeader",
         "Components/Common/Pager/Pager"],
	function (moment, system, util, proxy, navigation, pagedGridMixin, template) {	
	
	
	return Vue.component("app-reader-log-grid", {
		mixins: [pagedGridMixin],
		data: function() {
			return {
				logs: [],
                currentSort: { propertyId: "Date", ascending: false }
			}
        },
        props: {
            startDate: {
                type: Date,
                required: false,
                default: null
            },
            endDate: {
                type: Date,
                required: false,
                default: null
            }
        },
		template: template,
		methods: {
            fetch: function () {
 
                proxy.readerLog.getAllPaged(this.startAt, this.take, this.currentSort,
                    this.currentFilter).then(function (data) {	
					this.update(data);
				}.bind(this), function (error) {
				    system.showAlert(error, "error");
				});
			},		
			update: function (data) {			
				this.logs = $.map(data.data, function (elm) {
					return this.augmentLog(elm);				
				}.bind(this));
				this.totalItems = data.total;
			},	
			augmentLog: function (log) {	
				log.date = moment(log.date);
				return log;
			},
			refresh: function () {
				this.fetch();
            },
            getLogTypeOptions: function () {
                return Q.fcall(function() {
                    return $.map(system.enums.LogType, function(val, key) {
                        return {
                            value: key,
                            display: val
                        }
                    });
                });
            }
		},
		created: function () {
			this.fetch();
		}
	});
	
});