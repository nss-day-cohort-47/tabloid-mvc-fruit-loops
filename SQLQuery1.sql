--Alter Table Post Add isDeleted BIT  NOT NULL DEFAULT(0);
--Insert into Category (Name, isDeleted) values ('Running',0);
Update Post set isDeleted = 0;