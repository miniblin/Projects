<?php

$servername = "locationdb.cg6ciuaq7qev.us-west-2.rds.amazonaws.com";
$username = "admin";
$password = "asegroup4";
$dbname = "locationdb";
$connection = null;

function recordLog($datetime,$errorType, $UserID){

$connection = new mysqli($GLOBALS['servername'], $GLOBALS['username'], $GLOBALS['password'], $GLOBALS['dbname']);
	if($connection->connect_error){
		echo "Connection Error";
	}	

	$query2 = "INSERT INTO Log (datetime, errorType, deviceID) VALUES('$datetime','$errorType','$userID')";
	
	if($connection->query($query2) == TRUE){
		echo "LOG RECORDED";
	}
	
	$connection->close();
}

if($_SERVER["REQUEST_METHOD"] == "POST"){
	SQLinsert($_REQUEST['datetime'], $_REQUEST['errorType'], $_REQUEST['UserID']);
}else{
	#echo "no post request made";
}

?>
