SELECT PostCode,AVG(Value)
FROM AveragePerPostCode
WHERE Year >2014 And PostCode LIKE "BN2%"
GROUP BY Postcode