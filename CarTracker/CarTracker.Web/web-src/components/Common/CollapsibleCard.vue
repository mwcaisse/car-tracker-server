<template>
    <div class="card">
        <div class="card-header">
            {{ title }}
            <span class="float-right">
                <slot name="header-icons"></slot>
                <i class="fa fa-chevron-up action-icon" v-on:click="collapse" v-if="mexpanded" aria-hidden="true"></i>
                <i class="fa fa-chevron-down action-icon" v-on:click="expand" v-if="!mexpanded" aria-hidden="true"></i>
            </span>
        </div>
        <div class="card-body" v-show="mexpanded">
            <slot>Empty Card</slot>
        </div>
    </div>
</template>
<script>
    export default {
        data: function () {
            return {
                mexpanded: this.expanded
            }
        },
        props: {
            title: {
                type: String,
                required: true
            },
            expanded: {
                type: Boolean,
                required: false,
                default: true
            }
        },    
        methods: {
            collapse: function () {
                this.mexpanded = false;
            },
            expand: function () {
                this.mexpanded = true;
            }
        }
    }
</script>