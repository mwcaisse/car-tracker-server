"use strict";

define("Components/Common/ColumnHeader/ColumnHeader", 
		["moment", "Service/system", "Service/util", "Service/applicationProxy", "Service/navigation", 
         "AMD/text!Components/Common/ColumnHeader/ColumnHeader.html"],
	function (moment, system, util, proxy, navigation, template) {
	
	var SORT_ORDER_ASC = "ASC";
	var SORT_ORDER_DESC = "DESC";
	
	return Vue.component("app-column-header", {
		data: function() {
			return {
				sort: false,
                sortOrder: SORT_ORDER_DESC,
                mFilterOptions: [],
                currentFilter: ""
			}
        },	
		computed: {
			sortIcon: function() {
				return {
					"fa-sort-amount-asc"  : this.sortOrder === SORT_ORDER_ASC,
					"fa-sort-amount-desc" : this.sortOrder === SORT_ORDER_DESC
				}			
            },
            isFiltered: function() {
                return !util.isStringNullOrBlank(this.currentFilter);
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
                default: "DROPDOWN",
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
            currentFilter: function (val) {
                var event = "filter:update";
                if (util.isStringNullOrBlank(val)) {
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
				if (null === newSort || typeof newSort === "undefined") {
					this.sort = false;
				}
				else if (!(null === newSort || typeof newSort === "undefined") && newSort.propertyId === this.columnId) {
					this.sort = true;
					this.sortOrder = newSort.ascending === true ? SORT_ORDER_ASC : SORT_ORDER_DESC;
				}
				else {
					this.sort = false;
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
            if (this.enableFilter && this.filterType === "DROPDOWN") {
                this.populateFilterOptions();
            }
		}
	});
	
});