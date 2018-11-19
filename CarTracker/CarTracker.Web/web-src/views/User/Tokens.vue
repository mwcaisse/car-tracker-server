<template>
    <div>
        <div class="row">
            <h2>User Authentication Tokens</h2>
        </div>

        <div class="row py-3">
            <div class="col-md-12">
                <app-auth-token-grid></app-auth-token-grid>
            </div>
        </div>
    </div>
</template>

<script>
    import Links from "services/Links.js"
    import AuthTokenGrid from "components/User/AuthTokenGrid.vue"

    export default {
        components: {
            "app-auth-token-grid": AuthTokenGrid   
        },
        data: function () {
            return {
            }
        },
        created: function () {
            Links.setActiveNavigation("User");
        }
    }
</script>