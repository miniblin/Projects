<?php
$servername = "davethings.com.mysql";
$username = "davethings_com";
$password = "c4xLAvX3";
$dbname = "davethings_com";


//variables from Dimersion
$macAddress = $_REQUEST['userMacAddress'];
$displayName = $_REQUEST['displayName'];
$score = $_REQUEST['score'];
$sqlStatement;
$functionNo = $_REQUEST['functionNo'];

//echo $functionNo;
//echo $macAddress;
//echo $displayNAme;

echo "<br>";
function addUser() {
    
    global $displayName;
    global $macAddress;
    if ($displayName == ""){
        $displayName = "Anon: Non windows user?";
    }
    $sql = "INSERT  INTO Users (Username, DisplayName)
    VALUES ('$macAddress', '$displayName')";
    return $sql;
    //  code to be executed;
}



function addScore(){
    global $macAddress;
    global $score;
    $scoreTime = date( 'Y-m-d H:i:s');
    $sql = "INSERT  INTO Scores (ScoreTime, Username,Score)
    VALUES ('$scoreTime', '$macAddress',$score)";
    return $sql;


}



function getScores(){
    global $macAddress;

    $sql = "SELECT * FROM Scores inner join Users WHERE Users.Username = Scores.Username ORDER BY Score DESC LIMIT 10 ";
    //WHERE Username='$macAddress'";

    return $sql;



}

switch ($functionNo) {
    case 1:
        echo "Adding User <br>";
        $sqlStatement = addUser();
        break;
    case 2:
        echo "Adding Score <br>";
        $sqlStatement = addScore();
        break;
    case 3:
        $sqlStatement=getScores();
        // code to be executed if n=label3;
        break;
    default:
        $sqlStatement=getScores();
        $functionNo=3;
        //do nothing
}


try {
    
     $conn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
            // set the PDO error mode to exception
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
   
    if ($functionNo==3) {
     // echo $sqlStatement;
    echo" <table style='width:100%'>";
     echo "<tr style='text-align:left'>";
       echo"<th> DisplayName </th>";
        echo"<th> Score </th>";
       echo"<th> Date </th>";
       echo "</tr>";
        foreach ($conn->query($sqlStatement) as $row) {
           //echo "<br>";
            echo "<tr>";
            echo"<td>";
            echo $row['DisplayName'];
             echo"</td>";
            echo"<td>";
            echo $row['Score'];
            echo"</td>";
            echo"<td>";
            echo $row['ScoreTime'];
            echo"</td>";
            echo "</tr>";
        }
    }
    
    else {
            echo "funct num not 3";
            echo $sqlStatement;
            echo "<br>";
            // Prepare statement
            $stmt = $conn->prepare($sqlStatement);

            // execute the query
            $result = $stmt->execute();

            // echo a message to say the UPDATE succeeded
            echo $stmt->rowCount() . " records UPDATED successfully";
        }
    

    }
catch(PDOException $e)
{
    echo $sqlStatement . "<br>" . $e->getMessage();
}

$conn = null;
?>