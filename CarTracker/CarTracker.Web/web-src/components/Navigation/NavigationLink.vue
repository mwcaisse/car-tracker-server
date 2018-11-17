<template>   
    <li class="nav-item" v-if="!hasSubLinks">
        <a class="nav-link" :class="{ active: active }" :href="link">{{ name }}</a>
    </li>
    <li class="nav-item dropdown" v-else>
        <a class="nav-link dropdown-toggle" :class="{ active: active }" href="#" data-toggle="dropdown">{{ name }}</a>

        <div class="dropdown-menu" :class="{'dropdown-menu-right': right}">
            <a class="dropdown-item" v-for="subLink in subLinks" :href="subLink.link">{{ subLink.name }}</a>
        </div>
    </li>
</template>

<script>
    import System from "services/System.js";
    import * as Constants from "services/Constants.js";


    export default {        
        data: function() {
            return {
                active: false
            };
        },
        props: {
            linkId: {
                type: String,
                required: true
            },
            name: {
                type: String,
                required: true
            },
            link: {
                type: String,
                required: false,
				default: function () {
                    return "#";
                }
            },
            subLinks: {
                type: Array,
                required: false,
				default: function () {
                    return [];
                }
            },
            right: {
                type: Boolean,
				default: false
            }
        },
        computed: {
            hasSubLinks: function () {
                return this.subLinks.length > 0;
            }
        },  
        methods: {
            addSubLink: function (link) {
                this.subLinks.push(link);
            }
        },
        created: function () {
            System.events.$on(Constants.EVENT_NAVIGATION_ACTIVE_CHANGED, function (data) {
                this.active = (this.linkId === data.id);
            }.bind(this));
        }    
    }
</script>