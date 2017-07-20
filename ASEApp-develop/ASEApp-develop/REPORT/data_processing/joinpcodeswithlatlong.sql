SELECT Averageprice.Postcode ,Lattitude, Longitude , Value
From Averageprice INNER JOIN Postcodes
ON Averageprice.Postcode = Postcodes.Postcode
