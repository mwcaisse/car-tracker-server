<template>  
    <app-modal :title="title" ref="modal" :onClose="clear">
        <form>
            <div class="form-group row">
                <label class="col-md-2 col-form-label">Key</label>
                <div class="col-md-9">
                    <input type="text" class="form-control" placeholder="Enter Key" v-model="key" />
                </div>
                <div class="col-md-1">
                    <i class="fa fa-gear action-icon" style="font-size: 2em;" aria-hidden="true" v-tooltip title="Generate" v-on:click="generateKey"></i>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-md-2 col-form-label">Number of Uses</label>
                <div class="col-md-10">
                    <input type="text" class="form-control" placeholder="Enter Number of Uses" v-model="usesRemaining" />
                </div>
            </div>
            <div class="form-group row">
                <label class="col-md-2 col-form-label">Active</label>
                <div class="col-md-10">
                    <input type="checkbox" class="form-check-input" v-model="active"/>
                </div>
            </div>
        </form>

        <app-registration-key-uses :uses="userRegistrationKeyUses" v-if="userRegistrationKeyId > 0"></app-registration-key-uses>

        <button slot="footer" type="button" class="btn btn-primary" v-on:click="save">Save</button>
    </app-modal> 
</template>

<script>
    import System from "services/System.js"
    import * as Util from "services/Util.js"
    import { RegistrationKeyService } from "services/ApplicationProxy.js"
    import Modal from "components/Common/Modal.vue"
    import RegistrationKeyUses from "components/Admin/RegistrationKeyUses.vue"

    export default {
        components: {
            "app-modal": Modal,
            "app-registration-key-uses": RegistrationKeyUses
        },
		data: function() {
			return {
				title: "Create Registration Key",
                userRegistrationKeyId: -1,
				key: "",
				usesRemaining: 0,
				active: false,
                userRegistrationKeyUses: []
			}
		},
		methods: {
			fetchKey: function () {
                RegistrationKeyService.get(this.userRegistrationKeyId).then(function (data) {
					this.update(data);
				}.bind(this),
				function (error) {
				    System.showAlert(error, "error");
				})
			},
			update: function (key) {
                this.userRegistrationKeyId = key.userRegistrationKeyId;
				this.key = key.key;
				this.usesRemaining = key.usesRemaining;
				this.active = key.active;
                this.userRegistrationKeyUses = key.userRegistrationKeyUses;
			},
			clear: function () {
                this.userRegistrationKeyId = -1;
				this.key = "";
				this.usesRemaining = 0;
				this.active = false;
                this.userRegistrationKeyUses = [];
			},
			createModel: function () {
				return {
                    userRegistrationKeyId: this.userRegistrationKeyId,
					key: this.key,
					usesRemaining: this.usesRemaining,
					active: this.active
				};
			},
			save: function () {
				var func;
                if (this.userRegistrationKeyId < 0) {
					func = RegistrationKeyService.create;
				}
				else {
					func = RegistrationKeyService.update;
				}

				func(this.createModel()).then(function () {
					this.$emit("registrationKey:updated");
					this.$refs.modal.close();
				}.bind(this),
				function (error) {
				    System.showAlert(error, "error");
				});
			},
			refresh: function () {
				this.fetchKey();
			},
			generateKey: function () {
				this.key = Util.generateUuid();
			}
		},
		created: function () {
			System.events.$on("registrationKey:create", function () {
				this.$refs.modal.open();
				this.title = "Create Registration Key";
			}.bind(this));

			System.events.$on("registrationKey:edit", function (keyId) {
                this.userRegistrationKeyId = keyId;
				this.fetchKey();
				this.title = "Edit Registration Key";
				this.$refs.modal.open();
			}.bind(this));
		}
	}
</script>