update AveragePerPostCode 
SET PostCode = TRIM(TRAILING '\r' from PostCode)
