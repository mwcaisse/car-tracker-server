"use strict";

define("Components/Common/ColumnHeader/ColumnHeader", 
		["moment", "Service/system", "Service/util", "Service/applicationProxy", "Service/navigation", 
        "AMD/text!Components/Common/ColumnHeader/ColumnHeader.html",
        "Components/Common/DatePicker/DatePicker"],
	function (moment, system, util, proxy, navigation, template) {
	
	return Vue.component("app-column-header", {
		data: function() {
			return {
				sort: false,
                sortOrder: system.constants.SORT_ORDER.ASC,
                mFilterOptions: [],
                currentFilter: "",
                startDate: null,
                endDate: null
			}
        },	
		computed: {
			sortIcon: function() {
				return {
					"fa-sort-amount-asc"  : this.sortOrder === system.constants.SORT_ORDER.ASC,
					"fa-sort-amount-desc" : this.sortOrder === system.constants.SORT_ORDER.DESC
				}			
            },
            isFiltered: function() {
                return !util.isStringNullOrBlank(this.currentFilter);
            },
            filter: function () {
                if (!this.enableFilter) {
                    return null;
                }

                var filterObj = {};
                if (this.filterType === system.constants.FILTER_TYPE.DATE) {
                    if (this.startDate !== null) {
                        filterObj[system.constants.FILTER_OPERATION.GTE] = this.startDate;
                    }
                    if (this.endDate !== null) {
                        filterObj[system.constants.FILTER_OPERATION.LTE] = this.endDate;
                    }
                }
                else if (this.filterType === system.constants.FILTER_TYPE.TEXT) {
                    if (!util.isStringNullOrBlank(this.currentFilter)) {
                        filterObj[system.constants.FILTER_OPERATION.CONT] = this.currentFilter;
                    } 
                }
                else {
                    if (!util.isStringNullOrBlank(this.currentFilter)) {
                        filterObj[system.constants.FILTER_OPERATION.EQ] = this.currentFilter;
                    } 
                }
                //check if we added any properties to the filter object, and return it if so
                if (Object.keys(filterObj).length) {
                    return filterObj;
                }
                // no properties were added, no filtering is being done, return null
                return null;
            }
		},
		props: {
			columnId: {
				type: String,
				required: true
			},
			columnName: {
				type: String,
				required: true
			},
			enableSort: {
				type: Boolean,
				default: true
            },
			currentSort: {
				type: Object,
				required: false
            },
            enableFilter: {
                type: Boolean,
                default: false
            },
            filterType: {
                type: String,
                default: system.constants.FILTER_TYPE.DROPDOWN,
                required: false
            },
            useFilterOptions: {
                type: Boolean,
                default: true
            },
            filterOptions: {
                type: Function,
                required: false
            }
		},
		watch: {
			currentSort: function(newSort) {	
                this.updateSort(newSort);
            },
            filter: function(val) {
                var event = "filter:update";
                if (null == val) {
                    event = "filter:clear";
                } 
                var eventData = {
                    propertyId: this.columnId,
                    fitlerValue: val
                }
                this.$emit(event, eventData);
            }
		},
		template: template,
		methods: {
			sortAscending: function () {
				var eventData = {
					propertyId: this.columnId,
					ascending: true
				};
				this.$emit("sort:update", eventData);
			},
			sortDescending: function() {
				var eventData = {
					propertyId: this.columnId,
					ascending: false
				};
				this.$emit("sort:update", eventData);
			},
			clearSort: function () {
				this.$emit("sort:clear");
			},
			updateSort: function (newSort) {
                if (null === newSort || typeof newSort === "undefined" || newSort.propertyId !== this.columnId) {
					this.sort = false;
				}
				else {
					this.sort = true;
				}
            },
            populateFilterOptions: function() {
                if (typeof this.filterOptions === "function") {
                    this.filterOptions().then(function(data) {
                        this.mFilterOptions = data;
                    }.bind(this));
                }
            },
            selectFilter: function(filterVal) {
                this.currentFilter = filterVal;
            },
            clearFilter: function() {
                this.currentFilter = "";
            }
		},
		created: function () {
			if (typeof this.currentSort !== "undefined") {
				this.updateSort(this.currentSort);
            }
            if (this.enableFilter && this.filterType === system.constants.FILTER_TYPE.DROPDOWN) {
                this.populateFilterOptions();
            }
		}
	});
	
});