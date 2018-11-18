<template>
    <div class="row">
        <div class="col-xl">
            <span style="margin-left: 15px;">Showing items {{ itemStart }} to {{ itemEnd }} out of {{ totalItems }}</span>
        </div>
        <div class="col-xl">
            <ul class="pagination justify-content-center">
                <li class="page-item" v-for="item in itemsPerPageOptions" :class="{ active: mItemsPerPage == item }">
                    <a class="page-link" href="#" v-on:click.prevent="mItemsPerPage = item">{{ item }}</a>
                </li>
            </ul>
        </div>
        <div class="col-xl">
            <ul class="pagination justify-content-end" style="margin-right: 15px;">
                <li class="page-item" :class="{ disabled: !showFirstPageButton }">
                    <a class="page-link" href="#" v-on:click.prevent="firstPage">First</a>
                </li>
                <li class="page-item" :class="{ disabled: !showFirstPageButton }">
                    <a class="page-link" href="#" v-on:click.prevent="previousPage">Previous</a>
                </li>
                <li>
                    <input class="form-control" style="width: 75px;" type="text" v-model.number="currentPageUserInput" />
                </li>
                <li class="page-item" :class="{ disabled: !showLastPageButton }">
                    <a class="page-link" href="#" v-on:click.prevent="nextPage">Next</a>
                </li>
                <li class="page-item" :class="{ disabled: !showLastPageButton }">
                    <a class="page-link" href="#" v-on:click.prevent="lastPage">Last</a>
                </li>
            </ul>
        </div>
    </div>
</template>

<script>
    export default {
        data: function () {
            return {
                mItemsPerPage: 15,
                mCurrentPage: 1
            }
        },
        computed: {
            totalPages: function () {
                return Math.ceil(this.totalItems / this.mItemsPerPage);
            },
            itemStart: function () {
                return (this.mCurrentPage - 1) * this.mItemsPerPage + 1;
            },
            itemEnd: function () {
                return Math.min(this.totalItems, this.itemStart + this.mItemsPerPage - 1);
            },
            showFirstPageButton: function () {
                return this.mCurrentPage > 1;
            },
            showLastPageButton: function () {
                return this.mCurrentPage < this.totalPages;
            },
            pageOptions: function () {
                var pages = [];
                for (var i = 1; i <= this.totalPages; i++) {
                    pages.push(i);
                }
                return pages;
            },
            currentPageUserInput: {
                get: function () {
                    return this.mCurrentPage;
                },
                set: _.debounce(function (newValue) {
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
            itemsPerPage: function (newVal) {
                this.mItemsPerPage = newVal;
            },
            currentPage: function (newVal) {
                this.mCurrentPage = newVal;
            },
            mItemsPerPage: function () {
                this.updatePaging();
            },
            mCurrentPage: function () {
                this.updatePaging();
            }
        },
        props: {
            itemsPerPageOptions: {
                type: Array,
                default: function () { return [5, 10, 15, 25] },
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

    }
</script>