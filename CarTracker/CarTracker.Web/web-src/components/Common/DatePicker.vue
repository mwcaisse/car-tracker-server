<template>
    <div class="input-group">
        <input type="text" class="form-control" v-datepicker="date" />
        <div class="input-group-append">
            <span class="input-group-text">
                <i class="fa fa-calendar"></i>
            </span>
        </div>
    </div>
</template>

<script>
    export default {
        data: function () {
            return {
            }
        },
        computed: {
            date: function () {
                return this.value;
            }
        },
        props: {
            value: {
                type: Date,
                required: false
            }
        },    
        methods: {
            updateValue: function (value) {
                this.$emit("input", value);
            }
        },
        directives: {
            datepicker: {
                bind: function (el, binding, vnode) {
                    $(el).datepicker().on("changeDate", function (e) {
                        vnode.context.updateValue(e.date);
                    });
                    $(el).datepicker("update", binding.value);
                },
                update: function (el, binding) {
                    $(el).datepicker("update", binding.value);
                }
            }
        }
    }
</script>