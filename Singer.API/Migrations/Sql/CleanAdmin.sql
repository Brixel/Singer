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
		print 'Let the seed do the automatic creation of the admin user...'
	END

select * from AdminUsers;
SET IDENTITY_INSERT AspNetUserClaims OFF;
