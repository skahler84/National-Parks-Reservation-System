DELETE FROM reservation
DELETE FROM [site]
DELETE FROM campground
DELETE FROM park

INSERT INTO park VALUES ('Grand Canyon', 'Arizona', '01-01-1900', 1234, 1234567, 'The biggest canyon.');
DECLARE @parkId int = (SELECT @@IDENTITY);

INSERT INTO campground VALUES (@parkId, 'GC Campground', 3, 11, 75.00)
DECLARE @campgroundId int = (SELECT @@IDENTITY);

INSERT INTO [site] VALUES (@campgroundId, 11, 12, 0, 0, 0);
DECLARE @siteId int = (SELECT @@IDENTITY);

INSERT INTO reservation VALUES (@siteId, 'Reservation Test Name', '06/01/2020', '06/15/2020', '02/25/2020');
DECLARE @reservationId int = (SELECT @@IDENTITY);

SELECT @parkId AS parkId, @campgroundId AS campgroundId, @siteId AS siteId, @reservationId AS reservationId;