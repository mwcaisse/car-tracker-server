<template>
    <div class="dropdown" style="display: block;" v-if="enableSort || enableFilter">
        <a href="#" class="dropdown-toggle no-color" data-toggle="dropdown" v-on:click.prevent>{{ columnName }}</a>
        <i class="fa float-right" :class="sortIcon" v-if="sort" style="vertical-align:middle;"></i>
        <i class="fa float-right fa-filter" v-if="isFiltered" style="vertical-align:middle;"></i>
        <div class="dropdown-menu">
            <a class="dropdown-item" href="#" v-on:click.prevent="sortAscending"><i class="fa fa-sort-amount-asc"></i>&nbsp;Sort Ascending</a>
            <a class="dropdown-item" href="#" v-on:click.prevent="sortDescending"><i class="fa fa-sort-amount-desc"></i>&nbsp;Sort Descending</a>
            <a class="dropdown-item" href="#" v-on:click.prevent="clearSort"><i class="fa fa-close"></i>&nbsp;Clear Sort</a>

            <div class="dropdown-divider" v-if="enableSort && enableFilter"></div>
            <div v-if="enableFilter && filterType === constants.FILTER_TYPE.DROPDOWN">
                <a class="dropdown-item" href="#" v-for="filterOption in mFilterOptions" v-on:click.prevent="selectFilter(filterOption)" :class="{ active: currentFilter == filterOption }">&nbsp;{{ filterOption }}</a>
                <a class="dropdown-item" href="#" v-on:click.prevent="clearFilter"><i class="fa fa-close"></i>&nbsp;Clear Filter</a>
            </div>
            <div v-if="enableFilter && filterType === constants.FILTER_TYPE.DROPDOWN_COMPLEX">
                <a class="dropdown-item" href="#" v-for="filterOption in mFilterOptions" v-on:click.prevent="selectFilter(filterOption.value)" :class="{ active: currentFilter == filterOption.value }">&nbsp;{{ filterOption.display }}</a>
                <a class="dropdown-item" href="#" v-on:click.prevent="clearFilter"><i class="fa fa-close"></i>&nbsp;Clear Filter</a>
            </div>
            <div v-if="enableFilter && filterType === constants.FILTER_TYPE.TEXT">
                <div class="px-2 py-1">
                    <label>Filter</label>
                    <input class="form-control" type="text" v-model.lazy="currentFilter" />
                </div>
            </div>
            <div v-if="enableFilter && filterType === constants.FILTER_TYPE.DATE">
                <div class="px-2 py-1">
                    <label>Start Date</label>
                    <app-datepicker v-model="startDate"></app-datepicker>
                </div>
                <div class="px-2 py-1">
                    <label>End Date</label>
                    <app-datepicker v-model="endDate"></app-datepicker>
                </div>
            </div>
        </div>
    </div>
    <span v-else>
        {{ columnName }}
    </span>
</template>

<script>
    import * as Constants from "services/Constants.js"
    import * as Util from "services/Util.js"

    import DatePicker from "components/Common/DatePicker.vue"

    export default {
        data: function() {
            return {
                sort: false,
                sortOrder: Constants.SORT_ORDER.ASC,
                mFilterOptions: [],
                currentFilter: "",
                startDate: null,
                endDate: null
            }
        },
        computed: {
            sortIcon: function() {
                return {
                    "fa-sort-amount-asc": this.sortOrder === Constants.SORT_ORDER.ASC,
                    "fa-sort-amount-desc": this.sortOrder === Constants.SORT_ORDER.DESC
                }
            },
            isFiltered: function() {
                return Util.isStringNullOrBlank(this.currentFilter);
            },
            filter: function () {
                if (!this.enableFilter) {
                    return null;
                }

                var filterObj = {};
                if (this.filterType === Constants.FILTER_TYPE.DATE) {
                    if (this.startDate !== null) {
                        filterObj[Constants.FILTER_OPERATION.GTE] = this.startDate;
                    }
                    if (this.endDate !== null) {
                        filterObj[Constants.FILTER_OPERATION.LTE] = this.endDate;
                    }
                }
                else if (this.filterType === system.constants.FILTER_TYPE.TEXT) {
                    if (this.currentFilter !== "") {
                        filterObj[Constants.FILTER_OPERATION.CONT] = this.currentFilter;
                    }
                }
                else {
                    if (this.currentFilter !== "") {
                        filterObj[Constants.FILTER_OPERATION.EQ] = this.currentFilter;
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
                default: Constants.FILTER_TYPE.DROPDOWN,
                    required: false
            },
            useFilterOptions: {
                type: Boolean,
                default: true
            },
            filterOptions: {
                type: Function,
                    required: false
            },
            filterStartDate: {
                type: Date,
                    required: false,
                default: null
            },
            filterEndDate: {
                type: Date,
                    required: false,
                default: null
            }
        },
        watch: {
            currentSort: function(newSort) {
                this.updateSort(newSort);
            },
            filter: _.debounce(function (val) {
                var event = "filter:update";
                if (null == val) {
                    event = "filter:clear";
                }
                var eventData = {
                    propertyId: this.columnId,
                    fitlerValue: val
                }
                this.$emit(event, eventData);
            }, 500),
                filterStartDate: function(newStartDate) {
                    this.startDate = newStartDate;
                },
            filterEndDate: function(newEndDate) {
                this.endDate = newEndDate;
            }
        },  
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
                    this.filterOptions().then(function (data) {
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
            if (this.enableFilter &&
                (this.filterType === Constants.FILTER_TYPE.DROPDOWN ||
                    this.filterType === Constants.FILTER_TYPE.DROPDOWN_COMPLEX)) {

                this.populateFilterOptions();
            }
        },
        components: {
            "app-datepicker": DatePicker
        }
    }
</script>