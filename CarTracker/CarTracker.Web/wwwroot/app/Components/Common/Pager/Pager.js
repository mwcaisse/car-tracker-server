"use strict";

define("Components/Common/Pager/Pager", 
		["moment", "Service/system", "Service/util", "Service/applicationProxy", "Service/navigation", 
         "AMD/text!Components/Common/Pager/Pager.html"],
	function (moment, system, util, proxy, navigation, template) {

	    return Vue.component("app-pager",
	    {
            data: function () {
                return {
                    mItemsPerPage: 15,
                    mCurrentPage: 1
                }
	        },
	        computed: {
	            totalPages: function() {
                    return Math.ceil(this.totalItems / this.mItemsPerPage);
	            },
	            itemStart: function() {
                    return (this.mCurrentPage - 1) * this.mItemsPerPage + 1;
	            },
	            itemEnd: function() {
                    return Math.min(this.totalItems, this.itemStart + this.mItemsPerPage - 1);
	            },
	            showFirstPageButton: function() {
                    return this.mCurrentPage > 1;
	            },
	            showLastPageButton: function() {
                    return this.mCurrentPage < this.totalPages;
	            },
	            pageOptions: function() {
	                var pages = [];
	                for (var i = 1; i <= this.totalPages; i++) {
	                    pages.push(i);
	                }
	                return pages;
	            },
	            currentPageUserInput: {
	                get: function() {
                        return this.mCurrentPage;
	                },
	                set: _.debounce(function(newValue) {
	                        if (!$.isNumeric(newValue)) {
	                            newValue = 1;
	                        }
	                        if (newValue > this.totalPages) {
	                            newValue = this.totalPages;
	                        }
	                        this.mCurrentPage = newValue;
	                    },
	                    500)
	            }
	        },
	        watch: {
	            itemsPerPage: function(newVal) {
                    this.mItemsPerPage = newVal;
	            },
                currentPage: function (newVal) {
                    this.mCurrentPage = newVal;
                },
                mItemsPerPage: function() {
                    this.updatePaging();
                },
                mCurrentPage: function() {
                    this.updatePaging();
                }
	        },
	        props: {
	            itemsPerPageOptions: {
	                type: Array,
	                default: function() { return [5, 10, 15, 25] },
	                required: false
	            },
	            totalItems: {
	                type: Number,
	                required: true
	            },
	            itemsPerPage: {
	                type: Number,
                    required: false,
                    default: 15
                },
                currentPage: {
                    type: Number,
                    required: false,
                    default: 1
                }
		    },		
		    template: template,
		    methods: {
			    updatePaging: function () {
				    var eventData = {
					    itemsPerPage: this.mItemsPerPage,
                        currentPage: this.mCurrentPage
				    };
				    this.$emit("paging:update", eventData);				
			    },
			    previousPage: function () {
                    this.setPage(this.mCurrentPage - 1);
			    },
			    nextPage: function () {
                    this.setPage(this.mCurrentPage + 1);
			    },
			    firstPage: function () {
				    this.setPage(1);
			    },
			    lastPage: function () {
				    this.setPage(this.totalPages);
			    },
			    setPage: function (newPage) {
				    if (newPage < 1 || newPage > this.totalPages) {
					    return;
				    }
                    this.mCurrentPage = newPage;			
			    }
            }

	    });
    }
);