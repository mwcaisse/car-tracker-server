CREATE TABLE READER_LOG (
	ID BIGINT NOT NULL AUTO_INCREMENT,
	TYPE INT NOT NULL,
	MESSAGE VARCHAR(10000) NOT NULL,
	DATE DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,	
	CREATE_DATE DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	MODIFIED_DATE DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT PK_READER_LOG PRIMARY KEY (ID),
	CONSTRAINT PK_READER_LOG_LOG_TYPE FOREIGN KEY (TYPE) REFERENCES LOG_TYPE(ID)
)