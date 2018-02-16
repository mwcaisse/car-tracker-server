"use strict";

define("Components/Common/Pager/PagedGridMixin", [],
	function () {

	    return {
	        data: function() {
	            return {
	                currentSort: null, // { propertyId: "", ascending: false },
	                currentPaging: {
	                    itemsPerPage: 15,
	                    currentPage: 1
                    },
                    currentFilter: {},
	                totalItems: 1
	            }
	        },
	        computed: {
	            take: function() {
	                return this.currentPaging.itemsPerPage;
	            },
	            startAt: function() {
	                return (this.currentPaging.currentPage - 1) * this.take;
	            }
	        },
	        methods: {
	            refresh: function() {
	                // place holder method, called by paging event handlers defined below
	            }, 		
	            sortUpdated: function(newSort) {
	                this.currentSort = newSort;
	                this.refresh();
	            },
	            pagingUpdated: function(newPaging) {
	                //only update the paging if it is different than the one we currently have
	                if (JSON.stringify(newPaging) !== JSON.stringify(this.currentPaging)) {
	                    this.currentPaging = newPaging;
	                    this.refresh();
	                }
	            },
	            sortCleared: function() {
	                this.currentSort = null;
	                this.refresh();
	            },
                filterUpdated: function(eventData) {
                    this.currentFilter[eventData.propertyId] = eventData.fitlerValue;
                    this.refresh();
                },
                filterCleared: function(eventData) {
                    delete this.currentFilter[eventData.propertyId];
                    this.refresh();
                }
	        }		
	    };
	
});