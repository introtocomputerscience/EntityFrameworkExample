IF (OBJECT_ID('FK_AccountMapping_ToUser', 'F') IS NOT NULL)
BEGIN
  ALTER TABLE [Bank].[AccountMapping] DROP CONSTRAINT [FK_AccountMapping_ToUser]
END
IF (OBJECT_ID('FK_AccountMapping_ToAccount', 'F') IS NOT NULL)
BEGIN
  ALTER TABLE [Bank].[AccountMapping] DROP CONSTRAINT [FK_AccountMapping_ToAccount]
END
DROP TABLE [Bank].[AccountMapping];
DROP TABLE [Bank].[User]; 
DROP TABLE [Bank].[Account];