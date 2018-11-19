<template>
    <div class="row">
        <div class="col-lg-7">
            <p>Welcome to Car Tracker. Here you can create your account to access the system!</p>
            <p>
                Placeholder for future content here. For now we are just going to put some text here, so that the form
                on the right looks better.
            </p>
        </div>
        <div class="col-lg-5">
            <app-registration></app-registration>
        </div>
    </div>
</template>

<script>
    import Registration from "components/User/Registration.vue"

    export default {
        data: function () {
            return {
            }
        },
        components: {
            "app-registration": Registration
        }
    }
</script>