<template>
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
        <a class="navbar-brand" v-bind:href="homeUrl">Car Tracker</a>
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent">
            <span class="navbar-toggler-icon"></span>
        </button>

        <div class="collapse navbar-collapse" id="navbarSupportedContent">
            <ul class="navbar-nav mr-auto">
                <app-nav-link v-for="link in navigationLinks"
                              :key="link.id"
                              :linkId="link.id"
                              :name="link.name"
                              :link="link.link"
                              :subLinks="link.subLinks"></app-nav-link>
            </ul>

            <ul class="navbar-nav ml-auto">
                <app-nav-link v-for="link in rightNavigationLinks"
                              :key="link.id"
                              :linkId="link.id"
                              :name="link.name"
                              :link="link.link"
                              :subLinks="link.subLinks"
                              :right="true"></app-nav-link>
            </ul>
        </div>
    </nav>
</template>

<script>
    import System from "services/System.js"
    import Links from "services/Links.js"
    import { UserService } from "services/ApplicationProxy.js"
    import NavigationLink from "components/Navigation/NavigationLink.vue"

    export default {
        data: function () {
            return {
                navigationLinks: [],
                rightNavigationLinks: [],
                currentUserName: "User",
                homeUrl: System.rootPathPrefix
            }
        },
        methods: {
            initalizeLinks: function () {
                if (System.isAuthenticated) {
                    this.fetchCurrentUser().then(function () {

                        this.navigationLinks.push({
                            id: "Home", name: "Home", link: Links.home()
                        });

                        //Create the log links
                        var logLink = { id: "Log", name: "Log", link: "#", subLinks: [] };
                        logLink.subLinks.push({
                            id: "Log/Reader", name: "Reader", link: Links.readerLog()
                        });

                        this.navigationLinks.push(logLink);

                        var adminLink = { id: "Admin", name: "Admin", link: "#", subLinks: [] };
                        adminLink.subLinks.push({
                            id: "Admin/RegistrationKeys", name: "Registration Keys",
                            link: Links.adminRegistrationKey()
                        });
                        adminLink.subLinks.push({
                            id: "Admin/RequestLogs",
                            name: "Request Logs",
                            link: Links.adminRequestLogs()
                        });
                        adminLink.subLinks.push({
                            id: "Admin/ServerLogs",
                            name: "Server Logs",
                            link: Links.adminServerLogs()
                        });

                        this.navigationLinks.push(adminLink);

                        var userNav = {
                            id: "User", name: this.currentUserName, link: "#", subLinks: []
                        };

                        userNav.subLinks.push({
                            id: "User/CustomPlaces", name: "Custom Places", link: Links.userCustomPlaces()
                        });
                        userNav.subLinks.push({
                            id: "User/AuthTokens", name: "Tokens", link: Links.userAuthenticationTokens()
                        });
                        userNav.subLinks.push({
                            id: "User/Logout", name: "Logout", link: Links.logout()
                        });

                        this.rightNavigationLinks.push(userNav);

                    }.bind(this));
                }
            },
            fetchCurrentUser: function () {
                return UserService.me().then(function (user) {
                    this.currentUserName = user.name;
                }.bind(this));
            }
        },
        created: function () {
            this.initalizeLinks();
        },
        components: {
            "app-nav-link": NavigationLink
        }
    }
</script>