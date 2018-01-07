"use strict";

define("Components/Admin/RegistrationKeyModal/RegistrationKeyModal", 
		["moment", "Service/system", "Service/util", "Service/applicationProxy", "Service/navigation", 	
		 "AMD/text!Components/Admin/RegistrationKeyModal/RegistrationKeyModal.html",   
         "Components/Common/Modal/Modal",
         "Components/Admin/RegistrationKeyUses/RegistrationKeyUses"],
	function (moment, system, util, proxy, navigation, template) {	
	
	
	return Vue.component("app-registration-key-modal", {
		data: function() {
			return {
				title: "Create Registration Key",			
                userRegistrationKeyId: -1,
				key: "",
				usesRemaining: 0,
				active: false,
				keyUses: []
			}
		},			
		template: template,
		methods: {
			fetchKey: function () {							
                proxy.registrationKey.get(this.userRegistrationKeyId).then(function (data) {					
					this.update(data);
				}.bind(this),
				function (error) {
					alert("error fetching auth tokens!");
				})
			},
			update: function (key) {
                this.userRegistrationKeyId = key.userRegistrationKeyId;
				this.key = key.key;
				this.usesRemaining = key.usesRemaining;
				this.active = key.active;
				this.keyUses = key.keyUses;
			},
			clear: function () {
                this.userRegistrationKeyId = -1;
				this.key = "";
				this.usesRemaining = 0;
				this.active = false;
				this.keyUses = [];
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
					func = proxy.registrationKey.create;
				}
				else {
					func = proxy.registrationKey.update;
				}
				
				func(this.createModel()).then(function (key) {
					this.$emit("registrationKey:updated");	
					this.$refs.modal.close();
				}.bind(this),
				function (error) {
					alert(error);
				});
			},
			refresh: function () {
				this.fetchKey();
			},
			generateKey: function () {
				this.key = util.generatkeUuid();
			}
		},
		created: function () {
			system.bus.$on("registrationKey:create", function () {
				this.$refs.modal.open();
				this.title = "Create Registration Key";
			}.bind(this));
			
			system.bus.$on("registrationKey:edit", function (keyId) {
                this.userRegistrationKeyId = keyId;
				this.fetchKey();
				this.title = "Edit Registration Key";
				this.$refs.modal.open();
			}.bind(this));
		}
	});
	
});