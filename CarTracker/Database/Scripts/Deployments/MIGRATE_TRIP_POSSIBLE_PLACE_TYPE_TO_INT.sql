ALTER TABLE TRIP_POSSIBLE_PLACE
  CHANGE COLUMN PLACE_TYPE PLACE_TYPE_OLD VARCHAR(50);
  
ALTER TABLE TRIP_POSSIBLE_PLACE
  ADD COLUMN PLACE_TYPE INT AFTER PLACE_ID;
  
UPDATE TRIP_POSSIBLE_PLACE TPP
LEFT JOIN PLACE_TYPE PT ON TPP.PLACE_TYPE_OLD = PT.NAME
SET TPP.PLACE_TYPE = PT.ID;

ALTER TABLE TRIP_POSSIBLE_PLACE
  DROP COLUMN PLACE_TYPE_OLD;

ALTER TABLE TRIP_POSSIBLE_PLACE  
  MODIFY COLUMN PLACE_TYPE INT NOT NULL;
  
ALTER TABLE TRIP_POSSIBLE_PLACE
  ADD CONSTRAINT FK_TRIP_POSSIBLE_PLACE_PLACE_TYPE FOREIGN KEY (PLACE_TYPE) REFERENCES PLACE_TYPE(ID);



ALTER TABLE TRIP
  ADD CONSTRAINT FK_TRIP_TRIP_STATUS FOREIGN KEY (STATUS) REFERENCES TRIP_STATUS(ID);