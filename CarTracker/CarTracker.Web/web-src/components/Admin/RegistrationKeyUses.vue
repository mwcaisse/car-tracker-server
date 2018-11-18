<template>
    <app-collapsible-card title="Key Usages">
        <table class="table table-striped table-bordered" v-if="uses.length > 0">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Username</th>
                    <th>Used On</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="keyUse in uses">
                    <td>{{ keyUse.user.name }}</td>
                    <td>{{ keyUse.user.username }}</td>
                    <td>{{ keyUse.createDate | formatDateTime }}</td>
                </tr>
            </tbody>
        </table>
        <div class="card-body text-center" v-else>
            <p class="card-text">This Registration Key has not been used yet!</p>
        </div>
    </app-collapsible-card>
</template>

<script>
    import CollapsibleCard from "components/Common/CollapsibleCard.vue"

    export default {
        components: {
            "app-collapsible-card": CollapsibleCard
        },
        data: function () {
            return { }
        },
        props: {
            uses: {
                type: Array,
                required: true,
                default: function () {
                    return [];
                }
            }
        }  
    }
</script>