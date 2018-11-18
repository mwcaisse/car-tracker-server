<template>
    <form>
        <span v-if="isValid"></span>
        <div class="alert alert-danger alert-dismissible" role="alert" v-if="hasPageError">
            <button type="button" class="close" aria-label="Close" v-on:click="clearPageError"><span aria-hidden="true">&times;</span></button>
            <span>{{ pageError }}</span>
        </div>

        <div class="form-group">
            <label>Name</label>
            <input type="text" class="form-control" placeholder="Name" v-model="name" />
        </div>

        <div class="alert alert-danger" v-if="hasNameError">
            <span>{{ nameError }}</span>
        </div>

        <div class="form-group">
            <label>Username</label>
            <input type="text" class="form-control" placeholder="Username" v-model="username" />
        </div>

        <div class="alert alert-danger" v-if="hasUsernameError">
            <span>{{ usernameError }}</span>
        </div>

        <div class="form-group">
            <label>Password</label>
            <input type="password" class="form-control" placeholder="Password" v-model="password" />
        </div>

        <div class="form-group">
            <label>Confirm Password</label>
            <input type="password" class="form-control" placeholder="Confirm Password" v-model="passwordConfirm" />
        </div>

        <div class="alert alert-danger" v-if="hasPasswordError">
            <span>{{ passwordError }}</span>
        </div>

        <div class="form-group">
            <label>Email</label>
            <input type="email" class="form-control" placeholder="Email" v-model="email" />
        </div>

        <div class="form-group">
            <label>Confirm Email</label>
            <input type="email" class="form-control" placeholder="Confirm Email" v-model="emailConfirm" />
        </div>

        <div class="alert alert-danger" v-if="hasEmailError">
            <span>{{ emailError }}</span>
        </div>

        <div class="form-group">
            <label>Registration Key</label>
            <input type="text" class="form-control" placeholder="Registration Key" v-model="registrationKey" />
        </div>

        <div class="alert alert-danger" v-if="hasRegistrationKeyError">
            <span>{{ registrationKeyError }}</span>
        </div>

        <div class="pull-right">
            <button type="button" class="btn btn-primary" v-on:click="registerClick">Register</button>
            <button type="button" class="btn btn-default" v-on:click="cancel">Cancel</button>
        </div>

    </form>
</template>

<script>
    import System from "services/System.js"
    import Links from "services/Links.js"
    import * as Util from "services/Util.js"
    import { UserService } from "services/ApplicationProxy.js"


    export default {        
        data: function () {
            return {
                name: "",
                username: "",
                password: "",
                passwordConfirm: "",
                email: "",
                emailConfirm: "",
                registrationKey: "",

                nameError: "",
                usernameError: "",
                passwordError: "",
                emailError: "",
                registrationKeyError: "",

                pageError: "",
                hasClickedRegister: false
            }
        },
        computed: {
            hasNameError: function () {
                return !Util.isStringNullOrBlank(this.nameError);
            },
            hasUsernameError: function () {
                return !Util.isStringNullOrBlank(this.usernameError);
            },
            hasPasswordError: function () {
                return !Util.isStringNullOrBlank(this.passwordError);
            },
            hasEmailError: function () {
                return !Util.isStringNullOrBlank(this.emailError);
            },
            hasRegistrationKeyError: function () {
                return !Util.isStringNullOrBlank(this.registrationKeyError);
            },
            hasPageError: function () {
                return !Util.isStringNullOrBlank(this.pageError);
            },
            isValid: function () {
                if (!this.hasClickedRegister) {
                    return false;
                }

                this.nameError = "";
                this.usernameError = "";
                this.passwordError = "";
                this.emailError = "";
                this.registrationKeyError = "";

                if (Util.isStringNullOrBlank(this.name)) {
                    this.nameError = "Name cannot be blank!";
                }

                if (Util.isStringNullOrBlank(this.username)) {
                    this.usernameError = "Username cannot be blank!";
                }

                if (Util.isStringNullOrBlank(this.password)) {
                    this.passwordError = "Password cannot be blank!";
                }
                else if (this.password !== this.passwordConfirm) {
                    this.passwordError = "Passwords to not match!";
                }

                if (Util.isStringNullOrBlank(this.email)) {
                    this.emailError = "Email cannot be blank!";
                }
                else if (this.email !== this.emailConfirm) {
                    this.emailError = "Emails do not match!";
                }

                if (Util.isStringNullOrBlank(this.registrationKey)) {
                    this.registrationKeyError = "Registration Key cannot be blank!";
                }

                return Util.isStringNullOrBlank(this.nameError) &&
                    Util.isStringNullOrBlank(this.usernameError) &&
                    Util.isStringNullOrBlank(this.passwordError) &&
                    Util.isStringNullOrBlank(this.emailError) &&
                    Util.isStringNullOrBlank(this.registrationKeyError);
            }
        },
        methods: {
            createModel: function () {
                return {
                    name: this.name,
                    username: this.username,
                    password: this.password,
                    email: this.email,
                    registrationKey: this.registrationKey
                };
            },
            registerClick: function () {
                this.hasClickedRegister = true;
                if (this.isValid) {
                    this.register();
                }
            },
            register: function () {
                UserService.register(this.createModel()).then(function (res) {
                    if (res) {                        
                        Links.navigateTo(Links.login("registered"));
                    }
                    else {
                        System.showAlert("Failed to Register. An unexpected error occured.", "error");
                    }
                }.bind(this),
                    function (error) {
                        var errorText = error ? error : "An unexpected error occured.";
                        System.showAlert("Failed to Register. " + errorText, "error");
                    }.bind(this));
            },
            cancel: function () {
                Links.navigateTo(Links.home());
            },
            clearPageError: function () {
                this.pageError = "";
            }
        }
    }
</script>