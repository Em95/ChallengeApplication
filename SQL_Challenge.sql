

-- SQL Challenge 1:
Select ISNULL(cast(ClientID As varchar(MAX)), 'n/a') As client , SUM(ISNULL(Total,0)) As total
From users
	Full Outer Join orders On orders.UserID = users.UserID
Group By ClientID

-- SQL Challenge 2:
Select Top 2 ISNULL(cast(ClientID As varchar(MAX)), 'n/a') As client , SUM(ISNULL(Total,0)) As total
From users
	Full Outer Join orders On orders.UserID = users.UserID
Group By ClientID Order By total desc

