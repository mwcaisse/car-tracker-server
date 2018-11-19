<template>
    <div>
        <div class="row">
            <div class="col-md-12">
                <h2>Registration Keys</h2>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12 py-3">
                <app-registration-key-grid></app-registration-key-grid>
            </div>
        </div>
    </div>
</template>

<script>
    import Links from "services/Links.js"
    import RegistrationKeyGrid from "components/Admin/RegistrationKeyGrid.vue"

    export default {
        data: function () {
            return {     
            }
        },
        components: {
            "app-registration-key-grid": RegistrationKeyGrid
        },
        created: function () {
            Links.setActiveNavigation("Admin");
        }
    }
</script>