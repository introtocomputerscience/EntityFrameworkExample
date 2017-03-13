SELECT u.FirstName, u.LastName, u.SSN, a.[Type], a.Balance
FROM [bank].[AccountMapping] am
JOIN [bank].[User] u
ON am.UserId = u.Id
JOIN [bank].Account a
ON am.AccountId = a.Id;