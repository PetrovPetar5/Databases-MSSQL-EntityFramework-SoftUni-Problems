ALTER TABLE Users
DROP CONSTRAINT[PK__Users__3214EC0770499AA2]

ALTER TABLE Users
ADD CONSTRAINT PK_Users 
PRIMARY KEY(Id)

ALTER TABLE Users
ADD CONSTRAINT CK_UsernameLength
CHECK(LEN(Username)  >=3)