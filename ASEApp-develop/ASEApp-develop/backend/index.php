<?php
$serverName = "locationdb.cg6ciuaq7qev.us-west-2.rds.amazonaws.com";
$username = "admin";
$password = "asegroup4";
$dbName = "locationdb";
$connection = null;
// AppKey to be sent is: 4seGroup4.
if($_SERVER["REQUEST_METHOD"] == "POST")
	SQLinsert(filter_var($_REQUEST['AppKey'], FILTER_SANITIZE_EMAIL), 
		  filter_var($_REQUEST['locationLat'], FILTER_SANITIZE_NUMBER_FLOAT),
		  filter_var($_REQUEST['locationLong'], FILTER_SANITIZE_NUMBER_FLOAT),
		  filter_var($_REQUEST['id'], FILTER_SANITIZE_STRING),
		  filter_var($_REQUEST['time'], FILTER_SANITIZE_STRING)
		 );

function SQLinsert($AppKey, $LocationLat, $LocationLong, $UserID, $Time){
<<<<<<< HEAD
	//$serverName, $username, $password, $dbName, $connection;
	$connection = new PDO("mysql:host=$GLOBALS['serverName'];dbname=$GLOBALS['dbName'], $GLOBALS['username'], $GLOBALS['password']");
=======
	$connection = new PDO("mysql:host=$serverName;dbname=$dbName, $username, $password");
>>>>>>> c5124d92b6a725265c3aaa065bf46bd9055008f3
	$connection -> setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
	
	$access = $connection -> prepare("SELECT DB_key FROM Auth WHERE AutoID == 1");
	$access -> execute();
	$row = $access->fetch();
	
	if(password_verify($AppKey, $row[0])){
		$query1 = $connection -> prepare("INSERT IGNORE INTO users (UserID) VALUES ('$UserID')");
		if(!($query1 -> execute()))
			echo "Query error. User not added";
		$query2 = $connection -> prepare("INSERT INTO Locations (LocationLat, LocationLong, UserID, Time) VALUES ('$LocationLat', '$LocationLong', '$UserID', '$Time')");
		if(!($query2 -> execute()))
			echo "Query error. Location added";
	}
	$connection->close();
	
}
						 
function buildPage(){
	#$connection = new mysqli($GLOBALS['serverName'], $GLOBALS['userName'], $GLOBALS['password'], $GLOBALS['dbname']);
	
	$connection = mysql_connect($GLOBALS['serverName'], $GLOBALS['username'], $GLOBALS['password']);
	if(!$connection){
		echo "Connection Error";
	}
	$query = "SELECT * FROM Locations";
	mysql_select_db('locationdb');
	$retval = mysql_query($query, $connection);
	
	if(!$retval){
		echo "could not get data";
	}
	echo "TABLE<br>";
	echo "--------------------------------------<br>";
	while($row = mysql_fetch_array($retval, MYSQL_ASSOC)){
		
		
		$maplink = "https://www.google.co.uk/maps/place/50%C2%B050'21.4%22N+0%C2%B007'00.6%22W/@" . "{$row['LocationLat']}" . "," . "{$row['LocationLong']}". "17z/data=!3m1!4b1!4m5!3m4!1s0x0:0x0!8m2!3d50.8392746!4d-0.1168388";
		
		echo 	"| USER ID : {$row['UserID']}<br>".
				"|  LOCATION_LAT  :     {$row['LocationLat']} <br>".
				"|  LOCATION_LONG :	{$row['LocationLong']}<br>".
				"|  TIME :         {$row['Time']} <br>" .
				"|  <a href =" . $maplink . ">SEE ON MAP</a><br>" .
				"--------------------------------------<br>";
	}
	echo "Fetched data successfully\n";
	
	mysql_close($connection);
}
?>

<!DOCTYPE html>
<html>
<body>
	<h1> PHP Generated Page </h1>
	<?php buildPage(); ?>
</body>
</html>
