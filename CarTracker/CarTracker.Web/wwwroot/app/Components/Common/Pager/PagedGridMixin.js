"use strict";

define("Components/Common/Pager/PagedGridMixin", [],
	function () {

	    return {
	        data: function() {
	            return {
                    currentSort: null, // { propertyId: "", ascending: false },
                    currentPage: 1,
                    currentItemsPerPage: 15,
                    currentFilter: {},
	                totalItems: 1
	            }
	        },
	        computed: {
	            take: function() {
	                return this.currentItemsPerPage;
	            },
	            startAt: function() {
	                return (this.currentPage - 1) * this.take;
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
                    if (newPaging.itemsPerPage !== this.currentItemsPerPage ||
                        newPaging.currentPage !== this.currentPage) {

                        this.currentItemsPerPage = newPaging.itemsPerPage;
                        this.currentPage = newPaging.currentPage;

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