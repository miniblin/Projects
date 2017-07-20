
INSERT INTO Averageprice
SELECT  Postcode, AVG(Value)
FROM HouseValues
GROUP BY Postcode
ORDER BY Postcode;
