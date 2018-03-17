"use strict";

define("Views/Navigation/Navigation", 
    ["Service/system",
     "Service/navigation", 
	 "Components/Navigation/NavigationLink/NavigationLink",
	 "Service/applicationProxy",
	 "AMD/text!Views/Navigation/Navigation.html"], 
		
		function(system, navigation, navLink, proxy, template) {
			
			var isAuthenticated = $("#isAuthenticated").val() === "true";
	
			return function (elementId) {
				return new Vue({			
					el: elementId,
					template: template,
					data: {
						navigationLinks: [],
						rightNavigationLinks: [],
                        currentUserName: "User",
                        homeUrl: system.baseUrl
					},
					methods: {
						initalizeLinks: function () {
							if (isAuthenticated) {
								this.fetchCurrentUser().then(function () {				
								
									this.navigationLinks.push({
										id: "Home", name: "Home", link: navigation.homeLink()
									});
									
									//Create the log links
									var logLink = { id: "Log", name: "Log", link: "#", subLinks: [] };
									logLink.subLinks.push({
										id: "Log/Reader", name: "Reader", link: navigation.readerLogLink()
									});
									
									this.navigationLinks.push(logLink);
									
									var adminLink = {id: "Admin", name: "Admin", link: "#", subLinks: [] };
									adminLink.subLinks.push({
										id:"Admin/RegistrationKeys", name: "Registration Keys", 
										link: navigation.adminRegistrationKeyLink()
                                    });
								    adminLink.subLinks.push({
								        id: "Admin/RequestLogs",
								        name: "Request Logs",
								        link: navigation.adminRequestLogsLink()
								    });
									
									this.navigationLinks.push(adminLink);
									
									var userNav = {
										id: "User", name: this.currentUserName, link: "#", subLinks: []
									};

                                    userNav.subLinks.push({
                                        id: "User/CustomPlaces", name: "Custom Places", link: navigation.userCustomPlacesLink()
                                    });
									userNav.subLinks.push({
										id: "User/AuthTokens", name: "Tokens", link: navigation.userAuthenticationTokensLink()
									});
									userNav.subLinks.push({
										id: "User/Logout", name: "Logout", link: navigation.logoutLink()
									});
									
									this.rightNavigationLinks.push(userNav);
									
								}.bind(this));
							}
						},
                        fetchCurrentUser: function () {
                            return proxy.user.me().then(function (user) {
                                this.currentUserName = user.name;
                            }.bind(this));
                        }
					},
					created: function () {				
						this.initalizeLinks();
					}
				});
			}
		}
);