<template>
    <div>
        <app-page-alert></app-page-alert>
    </div>
</template>

<script>    
    import PageAlert from "components/Common/PageAlert.vue"

    export default {
        data: function () {
            return {
            }
        },
        components: {
            "app-page-alert": PageAlert
        }      
    }
</script>