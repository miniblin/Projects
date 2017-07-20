<?php
$servername = "locationdb.cg6ciuaq7qev.us-west-2.rds.amazonaws.com";
$username = "admin";
$password = "asegroup4";
$dbname = "locationdb";


//location from app

$latitude = $_REQUEST['latitude'];
$longitude = $_REQUEST['longitude'];
$PCODE="";
$PCODE = $_REQUEST['postcode'];


$sqlStatement;


//echo $functionNo;
//echo $macAddress;
//echo $displayNAme;

function getLocalHeatMap(){
    //  $latitude = 50.839523;
    //  $longitude=-0.118259;
    global $latitude;
    global $longitude;
    global $servername;
    global $username;
    global $password;
    global $dbname;
    global $PCODE;
    $latitude = number_format((float)$latitude, 3, '.', '');
    $longitude = number_format((float)$longitude, 3, '.', '');

    $range = 0.009;
    $minLat = ""+($latitude-$range);
    $maxLat = ""+($latitude+$range);
    $minLong =""+( $longitude-$range);
    $maxLong =""+( $longitude+$range);


    $longitude =""+$longitude;
    $latitude =""+$latitude;
    $conn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
    // set the PDO error mode to exception
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

    // $sqlStatement ='SELECT Postcode,Latitude, Longitude FROM Postcodes WHERE Latitude LIKE "'.$latitude .'%" AND Longitude LIKE "'.$longitude.'%"';
    if ($PCODE==NULL){
        $sqlStatement ='SELECT Postcode,Latitude, Longitude FROM Postcodes WHERE Latitude BETWEEN '.$minLat .'AND '.$maxLat.'  AND Longitude  BETWEEN '.$minLong.' AND '.$maxLong;
        $result = $conn->query($sqlStatement);
        $output=$result->fetchAll(PDO::FETCH_ASSOC);
        $json=json_encode($output);
        //print $json;

        $json = json_decode($json, true);
        $postcode =$json[0]['Postcode'];
   }
   else {
       $postcode = $PCODE;
   }
    $postcode = substr($postcode,0, 2);
    //echo $sqlStatement;


    //echo $postcode;

    $sqlStatement ='SELECT AveragePerPostCode.PostCode,AVG(Value), Latitude, Longitude FROM AveragePerPostCode INNER JOIN Postcodes ON Postcodes.Postcode = AveragePerPostCode.postCode WHERE Year >2014 And AveragePerPostCode.PostCode LIKE "'.$postcode .'%" GROUP BY AveragePerPostCode.PostCode ';
    $result = $conn->query($sqlStatement);
    $output=$result->fetchAll(PDO::FETCH_ASSOC);
    $json=json_encode($output);

    echo $json;
    // input long lat
    //get postcode
    //return nearby values
    /*
     * $sqlStatement ='SELECT AveragePerPostCode.PostCode,AVG(Value), Latitude, Longitude FROM AveragePerPostCode INNER JOIN Postcodes ON Postcodes.Postcode = AveragePerPostCode.postCode WHERE Year >2014 And AveragePerPostCode.PostCode LIKE "BN2%" GROUP BY AveragePerPostCode.PostCode ';
      $result = $conn->query($sqlStatement);
       $output=$result->fetchAll(PDO::FETCH_ASSOC);
       $json=json_encode($output);
   print $json;
     *
     *
     */
}


try {
    getLocalHeatmap();
    /*
        $conn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
        // set the PDO error mode to exception
        $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

        $sqlStatement ='SELECT AveragePerPostCode.PostCode,AVG(Value), Latitude, Longitude FROM AveragePerPostCode INNER JOIN Postcodes ON Postcodes.Postcode = AveragePerPostCode.postCode WHERE Year >2014 And AveragePerPostCode.PostCode LIKE "BN2%" GROUP BY AveragePerPostCode.PostCode ';
        $result = $conn->query($sqlStatement);
        $output=$result->fetchAll(PDO::FETCH_ASSOC);
        $json=json_encode($output);
        print $json;
       // print(json_encode($result));
    */

}


catch(PDOException $e)
{
    echo $sqlStatement . "<br>" . $e->getMessage();
}

$conn = null;
?>
