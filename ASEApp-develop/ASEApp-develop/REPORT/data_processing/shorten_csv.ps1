#trims the 3GB file down to about 300M, removing all the unnecessary columns

$source = "C:\Users\Dave\Downloads\pp-2014.csv"
$destination = "C:\Users\Dave\Documents\houseVals2014.csv"
(Import-CSV $source -Header 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16 | 
    Select "2","3","4" | 
    ConvertTo-Csv -NoTypeInformation | 
    Select-Object -Skip 1) -replace '"' | Set-Content $destination
