SELECT t.Id TripId
,t.[Name] TripName
,t.TripManagerId
,s.Id StopId
,s.[Order] StopOrder 
,s.[Name] StopName
,s.Description StopDescription
,s.Latitude
,s.Longitude
,s.Arrival
,s.Departure
,wh.*
,c.*
,r.*
,ct.*
FROM Trip t
LEFT JOIN Stop s ON t.Id = s.TripId
LEFT JOIN WorldHeritage wh on s.WorldHeritageId = wh.Id
LEFT JOIN Country c ON s.CountryId = c.Id
LEFT JOIN Region r ON r.Id = c.RegionId
LEFT JOIN Continent ct ON r.ContinentId = ct.Id;