INSERT INTO AveragePerPostCode
(PostCode,Value, Year)
SELECT  Postcode, AVG(Value), year(Date)
FROM New_HouseVals
WHERE year(Date)=2015
GROUP BY Postcode