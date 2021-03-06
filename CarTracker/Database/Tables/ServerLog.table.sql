CREATE TABLE SERVER_LOG (
	ID BIGINT NOT NULL AUTO_INCREMENT,
	REQUEST_UUID VARCHAR(64) NOT NULL,
	TYPE INT NOT NULL,
	MESSAGE TEXT NULL,
	EXCEPTION_MESSAGE TEXT NULL,
	STACK_TRACE TEXT NULL,
	CREATE_DATE DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	MODIFIED_DATE DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT PK_SERVER_LOG PRIMARY KEY (ID),
	CONSTRAINT FK_SERVER_LOG_LOG_TYPE FOREIGN KEY (TYPE) REFERENCES LOG_TYPE(ID)	
);