--Add Users
SET IDENTITY_INSERT [Bank].[User] ON;
insert into [Bank].[User](Id, FirstName, LastName, SSN) values(1,'John','Smith','123456789');
insert into [Bank].[User](Id, FirstName, LastName, SSN) values(2,'David','Marks','458966532');
insert into [Bank].[User](Id, FirstName, LastName, SSN) values(3,'Kelly','Carlton','623558945');
insert into [Bank].[User](Id, FirstName, LastName, SSN) values(4,'Craig','Atwater','986665588');
insert into [Bank].[User](Id, FirstName, LastName, SSN) values(5,'Dale','Gayle','147852265');
SET IDENTITY_INSERT [Bank].[User] OFF;
GO
--Add Accounts
SET IDENTITY_INSERT [Bank].[Account] ON;
insert into [Bank].[Account](Id, Type,Balance) values(1,'Checking', 1500.02);
insert into [Bank].[Account](Id, Type,Balance) values(2,'Checking', 865.23);
insert into [Bank].[Account](Id, Type,Balance) values(3,'Savings', 35621.00);
insert into [Bank].[Account](Id, Type,Balance) values(4,'Checking', 566.37);
insert into [Bank].[Account](Id, Type,Balance) values(5,'Checking', 10563.08);
insert into [Bank].[Account](Id, Type,Balance) values(6,'Checking', 11536.60);
SET IDENTITY_INSERT [Bank].[Account] OFF;
GO
--Map Users to Accounts
SET IDENTITY_INSERT [Bank].[AccountMapping] ON;
insert into [Bank].[AccountMapping](Id, UserId, AccountId) values(1,1,1);
insert into [Bank].[AccountMapping](Id, UserId, AccountId) values(2,2,2);
insert into [Bank].[AccountMapping](Id, UserId, AccountId) values(3,2,3);
insert into [Bank].[AccountMapping](Id, UserId, AccountId) values(4,3,4);
insert into [Bank].[AccountMapping](Id, UserId, AccountId) values(5,4,5);
insert into [Bank].[AccountMapping](Id, UserId, AccountId) values(6,5,6);
SET IDENTITY_INSERT [Bank].[AccountMapping] OFF;
GO
