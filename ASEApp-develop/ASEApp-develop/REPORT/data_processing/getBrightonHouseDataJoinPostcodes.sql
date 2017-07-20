SELECT AveragePerPostCode.PostCode,AVG(Value), Latitude, Longitude
FROM AveragePerPostCode
INNER JOIN Postcodes
ON Postcodes.Postcode = AveragePerPostCode.postCode
WHERE Year >2014 And AveragePerPostCode.PostCode LIKE "BN%"
GROUP BY AveragePerPostCode.PostCode 
