CREATE TABLE [Bank].[AccountMapping]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [UserId] INT NOT NULL, 
    [AccountId] INT NOT NULL, 
    CONSTRAINT [FK_AccountMapping_ToUser] FOREIGN KEY ([UserId]) REFERENCES [Bank].[User]([Id]),
    CONSTRAINT [FK_AccountMapping_ToAccount] FOREIGN KEY ([AccountId]) REFERENCES [Bank].[Account]([Id])
)
