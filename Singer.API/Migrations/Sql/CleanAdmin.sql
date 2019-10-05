DELETE FROM ApiResources;
DELETE FROM Clients;

DELETE FROM AdminUsers;

declare @userId uniqueidentifier;
declare @isAdminUserCount int;
declare @topId int;
select @userId = Id FROM AspNetUsers where UserName = 'admin'

select @isAdminUserCount = count(*) from AdminUsers where UserId = @userId;
select @topId = MAX(Id) FROM AspNetUserClaims; 


SET IDENTITY_INSERT AspNetUserClaims ON;

if @isAdminUserCount > 0
	BEGIN
		print 'User is already in admin table. Stopping...'
	END
else
	BEGIN
		INSERT INTO AdminUsers (Id, UserId) VALUES (newid(), @userId);
      UPDATE AspNetUsers SET Email = 'admin@test.com', firstName = 'Ad', LastName = 'Min', UserName = 'admin@test.com' WHERE Id = @userid;
		INSERT INTO AspNetUserClaims (Id, UserId, ClaimType, ClaimValue) VALUES (@topId+1, @userId, 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role', 'Administrator');
	END

select * from AdminUsers;
SET IDENTITY_INSERT AspNetUserClaims OFF;
