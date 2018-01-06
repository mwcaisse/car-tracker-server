ALTER TABLE TRIP 
  ADD COLUMN STATUS_OLD VARCHAR(50);
  
UPDATE TRIP 
  SET STATUS_OLD = STATUS;
  
ALTER TABLE TRIP
  DROP FOREIGN KEY TRIP_ibfk_2;
  
DROP TABLE TRIP_STATUS;  

CREATE TABLE TRIP_STATUS (
	ID INT NOT NULL,
	NAME VARCHAR(50) NOT NULL,
	DESCRIPTION VARCHAR(500) NOT NULL,
	CREATE_DATE DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	MODIFIED_DATE DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	CONSTRAINT PK_TRIP_STATUS PRIMARY KEY (ID),
	CONSTRAINT PK_TRIP_NAME UNIQUE (NAME)
);

INSERT INTO TRIP_STATUS (ID, NAME, DESCRIPTION) VALUES 
	(1, 'NEW', 'The trip is new'),
	(2, 'STARTED', 'The trip has been started'),
	(3, 'FINISHED', 'The trip has been finished'),
	(4, 'PROCESSED', 'The trip has been processed'),
	(5, 'FAILED', 'The trip failed to be processed');


ALTER TABLE TRIP DROP COLUMN STATUS;

ALTER TABLE TRIP
  ADD COLUMN STATUS INT 
  AFTER DESTINATION_PLACE;


UPDATE TRIP T
LEFT JOIN TRIP_STATUS TS ON T.STATUS_OLD = TS.NAME
SET 
  T.STATUS = TS.ID;
  
  
ALTER TABLE TRIP
  DROP COLUMN STATUS_OLD;
  
ALTER TABLE TRIP
  MODIFY COLUMN STATUS INT NOT NULL;
  
ALTER TABLE TRIP
  ADD CONSTRAINT FK_TRIP_TRIP_STATUS FOREIGN KEY (STATUS) REFERENCES TRIP_STATUS(ID);
  
