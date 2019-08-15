USE IDP;

--Users
SELECT * FROM AspNetUsers; --1 record
SELECT * FROM AspNetUserClaims; --4 records
SELECT * FROM AspNetUserLogins;  --EMPTY
SELECT * FROM AspNetUserRoles;  --EMPTY
SELECT * FROM AspNetUserTokens;  --EMPTY
--Roles
SELECT * FROM AspNetRoleClaims; --EMPTY
SELECT * FROM AspNetRoles;  --EMPTY

--Clients
SELECT * FROM Clients; --2 records
SELECT * FROM ClientClaims; -- EMPTY
SELECT * FROM ClientCorsOrigins; -- 1 record
SELECT * FROM ClientGrantTypes; -- 2 record