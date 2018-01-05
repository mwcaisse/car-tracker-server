CREATE TABLE CAR (
	ID BIGINT NOT NULL AUTO_INCREMENT,
	VIN VARCHAR(17) UNIQUE NOT NULL,
	NAME VARCHAR(250) NULL,
	OWNER_ID BIGINT NULL,
	MILEAGE DOUBLE NULL DEFAULT 0,
	MILEAGE_LAST_USER_SET DATETIME NULL,
	CREATE_DATE DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	MODIFIED_DATE DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	PRIMARY KEY (ID),
	FOREIGN KEY (OWNER_ID) REFERENCES USER(ID)
);