CREATE TABLE USER_REGISTRATION_KEY_USE (
	ID BIGINT NOT NULL AUTO_INCREMENT,
	KEY_ID BIGINT NOT NULL,
	USER_ID BIGINT NOT NULL,
	CREATE_DATE DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	MODIFIED_DATE DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT URKU_PK PRIMARY KEY (ID),
	CONSTRAINT URKU_FK_KEY FOREIGN KEY (KEY_ID) REFERENCES USER_REGISTRATION_KEY(ID),
	CONSTRAINT URKU_FK_USER FOREIGN KEY (USER_ID) REFERENCES USER(ID),
	CONSTRAINT URKU_U_USER_KEY UNIQUE (KEY_ID, USER_ID)
);