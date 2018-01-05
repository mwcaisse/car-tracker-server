CREATE TABLE READER_LOG (
	ID BIGINT NOT NULL AUTO_INCREMENT,
	TYPE VARCHAR(50) NOT NULL,
	MESSAGE VARCHAR(10000) NOT NULL,
	DATE DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	
	CREATE_DATE DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	MODIFIED_DATE DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	PRIMARY KEY (ID),
	FOREIGN KEY (TYPE) REFERENCES LOG_TYPE(ID)
)