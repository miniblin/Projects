window. onload = function ()
{
var canvas = document.getElementById("myCanvas"); // sets which div in the html the canvas will go in
var context = canvas.getContext("2d"); // creates 2D canvas and calls it "contex"
	context.font = "50px Chewy";
var collision=false;
var scale =800;
var carWidth=317;
var carHeight=200;
var foxWidth=181;
var foxHeight=150;
var screenWidth=583;
var screenHeight = 700;
var leftDirectionCars=[];
var rightDirectionCars=[];
var roads=[];
var horizon;
var coins=[];
var speed=16;
var snowStorm = new SnowStorm(50);
var totalScore=0;
var coinsOnLevel=0;
var totalCoins=0;
var timeRemaining=0;
var timeOut=false;
var eagleCreated=false;
var extremeMode=false;
var bonusChickens=[];

////////////////////////////////audio/////////////////////////
var chickenAudio = new Audio('audio/chicken.mp3');
var eatAudio= new Audio('audio/foxEat.mp3');
var eagleAudio= new Audio('audio/eagle.mp3');
var coinAudio= new Audio('audio/coin.mp3');
var carHornAudio= new Audio('audio/carHorn.mp3');
var foxHurtAudio= new Audio('audio/foxHurt.mp3');
var backingAudio=new Audio('audio/backing.mp3');
var tickingAudio = new Audio('audio/ticking.mp3');
var levelCompleteAudio=new Audio('audio/levelComplete.mp3');
var gameOverAudio=new Audio('audio/gameOver.mp3');
if(backingAudio.readyState > 0){
playBackingTrack();
}
else{
	setTimeout(playBackingTrack, 2000);
}

function playBackingTrack(){
	backingAudio.currentTime=0;
backingAudio.loop=true;

backingAudio.play();
}
///////////////////////////////////////////////////////////////

var chicken;
var gameStates ={ MENU: 0, INSTRUCTIONS:1, lEVELCOMPLETE:2, GAMEPLAY:3,END:4,CREDITS:5,DEAD:6,LEADERBOARD:7}
var gameState= gameStates.MENU;
var previousState = gameStates.MENU;

var displayLevelComplete=false;
var gamePaused=false;
//this tores the variable of a car when it needs to be drawn in front of the fox.(all cars 
// are drawn behind the fox by default)
var carInFront=[];

//menu 
var xcoordCar = -380; 
var ycoordCar = 330; 
xcoordBlueCar=710;
ycoordBlueCar =250
var widthCar = 317; 
var heightCar = 200; 
var xcoordFox=50;
var ycoordFox=450;

var blueCarsrc="blueCar.png";
var redCarsrc="car.png";
var roadsrc="road.png"
var foxLeftsrc="foxLeftSpriteSheet.png";
var foxRightsrc="foxRightSpriteSheet.png";


//////////////////////////////input variables//////////////////////////
var up,down,left,right;
up=false;
down=false;
left=false;
right=false;

var startGame = false;
window.addEventListener('keyup', function (key){

if (key.which == 37){left = false;}

if (key.which == 39){right = false;}

if (key.which == 38){up = false;}

 if (key.which == 40){down = false;}
});

var lives =10;
var currentLevel=0;


window.addEventListener('keydown', function (key){
if (key.which == 37){left = true;}

if (key.which == 39){right = true;}

if (key.which == 38){up = true;}
if (key.which == 86){
	callOnce=true;
	video.currentTime=0;
	gameOver();}
if (key.which == 40){down = true;}
if (key.which == 69){extremeMode = !extremeMode;}
if (key.which == 13&&gameState==gameStates.LEVELCOMPLETE)
	{
	if (totalCoins==45&&currentLevel==5){
		loadNewLevel();
	gameState=gameStates.GAMEPLAY;
	previousState = gameStates.GAMEPLAY;
	}	
	else if(currentLevel<5){
	loadNewLevel();
	gameState=gameStates.GAMEPLAY;
	previousState = gameStates.GAMEPLAY;
	}
	else{
		gameOver();
	}
	}
//two if statements for readability	
if (key.which == 13&&gameState==gameStates.MENU){
	bonusChickens=[];
	totalScore=0;
	totalCoins=0;
	coinsOnLevel=0;
	lives=10;
	currentLevel=0;
	
	timeRemaining=0;
	loadNewLevel();
	gameState=gameStates.GAMEPLAY;
	previousState = gameStates.GAMEPLAY;
	}
if (key.which == 72){
	if(gameState==gameStates.LEADERBOARD){
		gameState=previousState;
	}
	else{
	gameState=gameStates.LEADERBOARD;
	}
}
if (key.which == 13&&gameState==gameStates.CREDITS){
	 callOnce=true;
	 video.pause();
	 video.currentTime=0;
	gameState= gameStates.MENU;
	previousState=gameStates.MENU;
}
if (key.which == 13&&gameState==gameStates.DEAD){
	gameState= gameStates.MENU;
	previousState=gameStates.MENU;
}
if (key.which == 81){
	totalScore=0;
	totalcoins=0;
	lives=10;
	currentLevel=0;
	foxxy.alive = true;
	gameState= gameStates.MENU;
	previousState = gameStates.MENU;
	}
	
if (key.which == 83){snowStorm.activate();}
if (key.which == 70){
	
	foxxy.activateFoxyTime();
	
	
}


if (key.which == 73){
	if(gameState == gameStates.INSTRUCTIONS){
		gameState=previousState;
		
	}
	else{
		gameState= gameStates.INSTRUCTIONS;
	}
}

});

//////////////////////////////////////////////////////////////////////
var centerX=300;
var centerY= 300;
var radius = 200;

function leaderBoard(){
	try{
	var highScore= localStorage.getItem ("highScore") || '';
	
	context.fillStyle = "#A2E2FD";
			context.fillRect(0,0,screenWidth,screenHeight);
	
			
			context.fillStyle = "#FFFFFF";
			context.font = "60px Chewy";
			context.fillText("Your Highest Score",70,250);
			context.fillText(""+highScore,220,350);
			context.font = "30px Chewy";
			context.fillStyle = "#000000";
			
			context.fillText("Press H to return",190,600);
	}
	
	catch (err){
		context.fillStyle = "#A2E2FD";
			context.fillRect(0,0,screenWidth,screenHeight);
	
			
			context.fillStyle = "#FFFFFF";
			context.font = "40px Chewy";
			context.fillText("Sorry",240,200);
			context.fillText("This browser does not",120,250);
			context.fillText("support local storage" ,120,290);
			context.font = "30px Chewy";
			context.fillStyle = "#000000";
			
			context.fillText("Press H to return",190,600);
		
	}
			
			
			
			
	
}

function addScoreToLeaderboard(score){
	try{
		console.log("adding HighScore"+ localStorage.highScore);
	if(!(localStorage.highScore)){
		localStorage.highScore = ""+score;
	}	
	
	else if (score>(localStorage.highScore)){
		
		localStorage.highScore = ""+score;
	
	}
	}
	catch(err){
		
	}
	
	
	
	
}

function dead(){
	context.fillStyle = "#A2E2FD";
			context.fillRect(0,0,screenWidth,screenHeight);
			context.fillStyle = "#000000";
			context.font = "43px Chewy";
			context.fillText("Hey! You killed Foxxy",100,150);
			context.fillText("You B*****D",170,200);
			context.fillText("But still, you got to Level "+currentLevel,60,250);
			
			
			context.fillStyle = "#FFFFFF";
			context.font = "60px Chewy";
			
			context.fillText("Your Final Score was",50,350);
			context.fillText(""+(totalScore),230,420);
		
			
			context.fillStyle = "#000000";
			context.font = "40px Chewy";
			context.fillText("Press ENTER to return to the menu ",20,550);
	
}
function levelComplete(){
		tickingAudio.pause();
		console.log("totalCoins"+totalCoins);
		context.fillStyle = "#A2E2FD";
		context.fillRect(0,0,screenWidth,screenHeight);
		context.fillStyle = "#FFFFFF";
		context.font = "60px Chewy";
		switch(currentLevel){
			case 1:
			case 2:
			case 3:
			case 4:
			
			context.fillText("Level "+currentLevel+ " Complete",95,150);
			context.fillStyle = "#000000";
			context.font = "43px Chewy";
			context.fillText("Coins Collected: " +coinsOnLevel+"/"+coins.length,120,250);
			
			context.fillText("Current Score: "+ (totalScore),120,300);
			context.fillText("Time Remaining Bonus: "+ (timeRemaining),65,350);
			context.fillStyle = "#FFFFFF";
			context.font = "60px Chewy";
			context.fillText("Total Score: "+ (totalScore+timeRemaining),90,450);
			context.fillStyle = "#000000";
			context.font = "43px Chewy";
			context.fillText("Press ENTER to continue to level "+ (currentLevel+1),5,550);
			
			break;
			case 5:
			context.fillText("Level "+currentLevel+ " Complete",90,150);
			context.fillStyle = "#000000";
			context.font = "43px Chewy";
			context.fillText("Coins Collected: " +coinsOnLevel+"/"+coins.length,120,250);
			
			context.fillText("Current Score: "+ (totalScore),150,300);
			context.fillText("Time Remaining Bonus: "+ (timeRemaining),70,350);
			context.fillStyle = "#FFFFFF";
			context.font = "60px Chewy";
			context.fillText("Total Score: "+ (totalScore+timeRemaining),90,450);
			context.fillStyle = "#000000";
			context.font = "43px Chewy";
			context.fillText("You just completed The Game!",40,550);
			context.fillText("Press Enter To Continue",97,650);
			
			break;
			
			case 6:
			
			
			context.fillStyle = "#000000";
			context.font = "43px Chewy";
			
			context.fillText("Okay,now you're Done",20,150);
			context.fillText("Current Score: "+ (totalScore),135,300);
			context.fillText("Time Remaining Bonus: "+ (timeRemaining),70,350);
			context.fillStyle = "#FFFFFF";
			context.font = "60px Chewy";
			context.fillText("Total Score: "+ (totalScore+timeRemaining),90,450);
			context.fillStyle = "#000000";
			context.font = "43px Chewy";
			context.fillText("Well Done, youre the best!",65,550);
			context.fillText("Press Enter To Continue",97,650);
			}
		
		
}

function secretEnding(){
			context.fillStyle = "#000000";
			context.font = "43px Chewy";
			context.fillText("Total Coins Collected: " +totalCoins+"/"+"45",120,250);
			
			context.fillText("BONUS LEVEL UNLOCKED",150,300);
			
			context.fillText("Press Enter To Begin",97,650);
	
}



function checkTime(){
	
	
	if (timeRemaining<1){
		timeOut=true;
		timeRemaining=0;
		if (!eagleCreated){
		eagleAudio.currentTime=0;
		eagleAudio.play();
		eagle= new Eagle(screenWidth,foxxy.positionY-(foxHeight*(foxxy.positionY/scale)/2));
		eagleCreated=true;
		}
		else{
			eagle.positionX-=15;
			eagle.draw();
			if(eagle.positionX<foxxy.positionX+(foxWidth*(foxxy.positionY/scale)/1.5))
			{
				
				
				lives=0;
				
			}
		}
	}
	
	}
var video;
 video = document.getElementById("video");
function gameOver(){
	gameState=gameStates.END;
	 video.play();
}







   

	
//}
	
	//if time<1*
	//create an eagle
	//stop fox movement;
	//stop time
	//run eagle from left to right//when eagle = fox position, kill fox;
	



function Eagle (positionX, positionY){
	this.positionX=positionX;
	this.positionY=positionY;
	this.sizeX=180;
	this.sizeY=190;
	this.image = new Image();
	
	this.image.src = "eagle.png";
	
		
}
Eagle.prototype.draw=function(){
	
	context.drawImage(this.image,this.positionX,this.positionY,(this.sizeX*(this.positionY/scale)) , (this.sizeY*(this.positionY/scale)));
	
}
function loadNewLevel(){
	
	eagleCreated=false;
	timeOut=false;
	leftDirectionCars=[];
	rightDirectionCars=[];
	roads=[];
	totalScore=totalScore+timeRemaining;
	
	coinsOnLevel=0;
	coins=[];
	currentLevel++
	timeRemaining= currentLevel*350;
	foxxy=new Fox();
	
	generateRoads(currentLevel);
	
	
}



function moveRedCar(){
	xcoordCar+=10;
	if (xcoordCar>screenWidth){
		xcoordCar=-380;
	}
}

function moveBlueCar(){
	xcoordBlueCar-=15;
	if (xcoordBlueCar<-widthCar){
		xcoordBlueCar=710;
	}
}


var fox= new Image();
fox.src = "fox.png";
var level= new Image();
level.src = "levelbackground.png";
function draw() {

  setTimeout(function() {
	requestAnimationFrame(draw);
	context.clearRect(0, 0, canvas.width, canvas.height); // Clears canvas
	//gameOver();
	
	
	runLevel();
	
	if(gameState!=gameStates.END&&gameState!=gameStates.CREDITS){	
	context.fillStyle = "#FFFFFF";
	context.fillRect(0,700,500,500);
	context.fillRect(screenWidth,0,500,screenHeight);
	
	
	controlButtons();
	}
	 
   
	context.stroke;	
	
    }, 1000 / 30);
}

var callOnce=true;
function runLevel(){
	
	switch (gameState){
		case gameStates.DEAD:
		dead();
		
		break;
		
		case gameStates.END:
			 if(!(video.paused || video.ended)){
			context.drawImage(video,0,0,900,700);
			if(callOnce){
				callOnce=false;
			setTimeout(function(){
			gameState=gameStates.CREDITS; 
},	4500);

			}
	}break;
		case gameStates.LEADERBOARD:
			leaderBoard();
			break;
		case gameStates.CREDITS:
			credits();
			break;
		case gameStates.MENU:
			mainMenu();
		
		break;
		case gameStates.INSTRUCTIONS:
			  displayInstructions();
		break;
		
		case gameStates.LEVELCOMPLETE:
			levelComplete();
		break;
		
		case gameStates.GAMEPLAY:
			context.drawImage(level,0, 0, 583, 700);
			drawRoads();
			horizon.draw();
			if(!foxxy.alive){
				foxxy.render();
			}
			drawCoins();
			for (i=0;i<bonusChickens.length;i++){
				bonusChickens[i].checkGet();
				bonusChickens[i].draw();
			}
			chicken.checkGet();
			chicken.draw();
			displayLives();
			checkCoinCollision();
			updateCars();
			
			if(foxxy.alive){
				if(!timeOut){
				foxxy.move();
				}
				horizon.collision();
				foxxy.render();
			}
			checkTime();
			//draw any cars that need to appear in front of foxxy
			for(i=0;i<carInFront.length;i++){
			carInFront[i].drawCar();
			carInFront[i].inFrontDrawList=false;
			}
			//clear the list
			carInFront=[];
			foxxy.runFoxyTime();
			
		break;
		
		
		
		
	}
	
		
	
	if(snowStorm.active){
		snowStorm.update();
		snowScreen();
	}
	
	else{
		
	}
	
}

function snowScreen(){
	var snow= new Image();
	snow.src="night.png";
	
	context.drawImage(snow,0, 0, 900, 700);
}
draw();


var jumpUp =true;
//fox
	
var wait=0;
function moveFox(){
	
	if (ycoordFox>500){
		
		wait++;
	}
	if(wait>25&wait<30){
	fox.src = "foxcrouch.png";
	}
	if(wait>30){
	jumpUp=true
	wait=0;
	}
	if (ycoordFox<430){
	jumpUp = false;}
	if (jumpUp&wait<1){
		fox.src = "fox.png";
		ycoordFox-=8;
		//xcoordFox+=2;
	}
	else if(wait<1){ycoordFox+=7;
	
		//xcoordFox--;
	}
}	


function mainMenu(){
	var menu= new Image();
	menu.src = "menu.png";
	context.drawImage(menu,0, 0, 583, 700);
	
	//TITLE
	context.fillStyle = "#000000";
	context.font = "60px Chewy";
	context.fillText("CROSSY FOX ",10,100);
	context.fillStyle = "#ffffff";
	context.font = "25px Chewy";
	context.fillText("Press Enter to Start Game ",300,600);	
		
	
	var blueCar= new Image();
	blueCar.src = blueCarsrc;
	context.drawImage(blueCar,xcoordBlueCar, ycoordBlueCar, widthCar, heightCar);
		
	//car
	var car= new Image();
	car.src = redCarsrc;
	context.drawImage(car,xcoordCar, ycoordCar, widthCar, heightCar);
	moveBlueCar();
	
	
	context.drawImage(fox,xcoordFox,ycoordFox ,181 , 150);
	moveFox();
	moveRedCar();
	

	
	
}


function updateCars(){
	
	for (i=0;i<rightDirectionCars.length;i++){
	rightDirectionCars[i].collision();
	if(!rightDirectionCars[i].inFrontOfFox){
	rightDirectionCars[i].drawCar();
	}
	rightDirectionCars[i].moveCar();
	if(rightDirectionCars[i].inFrontOfFox&&!leftDirectionCars[i].inFrontDrawList){
		rightDirectionCars[i].inFrontDrawList=true;
		carInFront.push(rightDirectionCars[i]);
	}
	}
	for (i=0;i<leftDirectionCars.length;i++){
	//ehck collsions	
	leftDirectionCars[i].collision();
	if(!leftDirectionCars[i].inFrontOfFox){
	leftDirectionCars[i].drawCar();
	}
	leftDirectionCars[i].moveCar();
		if(leftDirectionCars[i].inFrontOfFox&&!leftDirectionCars[i].inFrontDrawList){
			
leftDirectionCars[i].inFrontDrawList=true;
		carInFront.push(leftDirectionCars[i]);
	}
	
	}
}

function drawRoads(){
	for (i=0;i<roads.length;i++){
	
	roads[i].drawRoad();
	}
	
}





function playerTimeout(){
	//bird flies down to player posirtion and kills it
	//play a sound for dead
}

function gameManager(){
	//call al necessary funtctions, have states
	//set difficulty
	//set level
}

function setDifficulty(difficulty){
	//numkber of cars based on diff
	//shorter time out
	//less health?
	//
}

function playOpeningVideo(){
	//need your help starfox
	//starfox videos after every level complete
	//
}



function generateRoads(numRoads){
scale=300+(numRoads*100);
horizon = new Background();
switch (numRoads){
	case 6:
	for(i=0;i<70;i++){
	bonusChickens.push(new Chicken(Math.random()*(screenWidth-100),(Math.random()*(screenHeight-100))+100));
	}
	break;
	case 5:
		roads.push(new Road(500,2,3));
		roads.push(new Road(350,3,3));
		roads.push(new Road(250,3,3));
		chicken= new Chicken(Math.random()*(screenWidth-100),160);
		roads.push(new Road(180,5,3));
		roads.push(new Road(130,7,3));
		break;
	case 4:
		roads.push(new Road(500,2,3));
		roads.push(new Road(340,3,3));
		roads.push(new Road(230,3,3));
		chicken= new Chicken(Math.random()*(screenWidth-100),140);
		roads.push(new Road(150,5,3));
		break;
	case 3:
		roads.push(new Road(470,2,3));
		chicken= new Chicken(Math.random()*(screenWidth-100),250);
		roads.push(new Road(300,3,3));
		roads.push(new Road(190,5,3));
		break;
	case 2:
		roads.push(new Road(400,2,3));
		chicken= new Chicken(Math.random()*(screenWidth-100),350);
		roads.push(new Road(250,3,3));
		break;
	case 1:
	chicken= new Chicken(Math.random()*(screenWidth-100),275);
		roads.push(new Road(325,2,3));
		break;
		
		
}

	

/*
road= new Road(100,5); 
road4= new Road(150,5)
road2= new Road(225,3);
road3= new Road(300,2);
road5= new Road(450,2);
*/	
}

function SnowFlake(){
	this.positionX= Math.random()*(screenWidth+700);
	this.positionY=Math.random()*screenHeight;
	this.size=5;
	
}

SnowStorm.prototype.activate=function(){
	this.active=!this.active;
	if (this.active){
		blueCarsrc="blueCarSnow.png";
		redCarsrc="carsnow.png";
		roadsrc="roadice.png";
		
	}
	else{
		blueCarsrc="blueCar.png";
		redCarsrc="car.png";
		roadsrc="road.png";
		
	}
	for (i=0;i<rightDirectionCars.length;i++){
		rightDirectionCars[i].image.src=blueCarsrc;
		leftDirectionCars[i].image.src=redCarsrc;
	}
	for (i=0;i<roads.length;i++){
		roads[i].image.src= roadsrc;
	}
	
}
function SnowStorm(numberofFlakes){
	this.active=false;
	this.snow=[]
	for (i=0;i<numberofFlakes;i++){
		this.snow.push(new SnowFlake());
	}
}
var begin =true;
SnowStorm.prototype.update=function(){
	
	
	for (i=0;i<this.snow.length;i++){
		this.snow[i].positionX--;
		this.snow[i].positionY+=2;
		if(this.snow[i].positionY>screenHeight){
			this.snow[i].positionY=0;
			this.snow[i].positionX= Math.random()*(screenWidth+700);
		}
		context.fillStyle = "white"; //sets the colour to green
		context.fillRect(this.snow[i].positionX,this.snow[i].positionY,this.snow[i].size,this.snow[i].size);
	}
	
}


function Fox(){
	this.image = new Image();
	this.image.src= foxRightsrc;
	this.startX=50;
	this.startY=screenHeight-foxHeight;
	this.positionX=this.startX;
	this.positionY=this.startY;
	this.alive=true;
	this.facingRight=true;
	this.tickCount=0;
	this.frameIndex=0;
	this.ticksPerFrame = 0;
	this.numberofFrames =10
	this.foxyTime =100;
	this.foxyTimeEnabled=false;
	
	
	
}



Fox.prototype.activateFoxyTime=function(){
	
	this.foxyTimeEnabled=!this.foxyTimeEnabled;
	if (this.foxyTimeEnabled&& this.foxyTime>0){
		tickingAudio.currentTime=0;
		tickingAudio.play();
		for (i=0;i<rightDirectionCars.length;i++){
			rightDirectionCars[i].speed=rightDirectionCars[i].speed/3;
			leftDirectionCars[i].speed=leftDirectionCars[i].speed/3;
		}
	}
	else{
		tickingAudio.pause();
		for (i=0;i<rightDirectionCars.length;i++){
		foxyTimeEnabled=false;
		rightDirectionCars[i].speed=1+(speed*rightDirectionCars[i].scale);
		leftDirectionCars[i].speed=1+(speed*leftDirectionCars[i].scale);
		}
	}
}

Fox.prototype.runFoxyTime=function(){
	
	if(this.foxyTime<1){
		this.activateFoxyTime();
	}
	else if (this.foxyTimeEnabled){
		this.foxyTime--;
	}
	
		
	
}



Fox.prototype.render=function(){
	if(this.facingRight){
		this.image.src= foxRightsrc;
		
	}
	else{
		this.image.src= foxLeftsrc;
		
	}
	context.drawImage(this.image,
	this.frameIndex*foxWidth,
	0,
	foxWidth,
	foxHeight,
	this.positionX,
	this.positionY,
	(foxWidth*(this.positionY/scale)) , 
	(foxHeight*(this.positionY/scale)));
	
}
Fox.prototype.update = function(){
	
	if (this.tickCount>this.ticksPerFrame){
		this.frameIndex++;
		this.tickCount=0;
		if (this.frameIndex>= this.numberofFrames-1){
			this.frameIndex=0;
		}
		
		
		
	}
	
}


Fox.prototype.drawFox=function(){
	
	context.drawImage(this.image,this.positionX,this.positionY,(foxWidth*(this.positionY/scale)) , (foxHeight*(this.positionY/scale)));
	
}
Fox.prototype.move =function() {
	var speed=1+(5*(this.positionY/scale));
	

	if (left){
	if(this.positionX<(-(foxWidth*this.positionY/scale))){
		this.positionX=screenWidth;
	}
	
		
		this.facingRight=false;
		this.positionX-=speed;
		this.tickCount++;
		foxxy.update();
	}
	if(right){
		if(this.positionX>screenWidth){
			this.positionX=(-(foxWidth*(this.positionY/scale)));
		}
		
		this.facingRight=true;
		this.positionX+=speed;
		this.tickCount++;
		foxxy.update();
	}
	
	if(up){
		this.tickCount++;
		this.positionY-=speed;
		if(this.facingRight){
						this.positionX+=speed/1.5;
		}
		else{
						this.positionX-=speed/1.5;
		}
		foxxy.update();
	}
	if(down&&(this.positionY<(screenHeight-(foxHeight*this.positionY/scale)))){
		if(!extremeMode){
		this.tickCount++;
		this.positionY+=speed;
		if(this.facingRight){
			this.positionX+=speed/1.5;
		}
		else{
			this.positionX-=speed/1.5;
		}
		}
		
		foxxy.update();
	}
}

function Background(){
	
	this.image = new Image();
	this.scale = (200/scale);
	this.sizeX= 6000*this.scale;
	this.sizeY= 540*this.scale;
	this.image.src = "horizon.png";
	
}

Background.prototype.collision=function() {
	if(this.sizeY>(foxxy.positionY+(foxHeight*(foxxy.positionY/scale)))){
				console.log("levelcomplete");
				levelCompleteAudio.currentTime=0;
				levelCompleteAudio.play();
				totalCoins+=coinsOnLevel;
				addScoreToLeaderboard((totalScore+timeRemaining));
				gameState=gameStates.LEVELCOMPLETE;
				previousState = gameStates.LEVELCOMPLETE;
				
				
	}
	
}

Background.prototype.draw=function() {
	context.drawImage(this.image,0, 0, this.sizeX, this.sizeY);
	
}


function Road(positionY,numCars, numCoins){
	this.positionY=positionY;
	this.image = new Image();
	this.scale = this.positionY/scale;
	this.sizeX= 6000*this.scale;
	this.sizeY= 197*this.scale;
	this.image.src = roadsrc;
	
	
	var gap=0;
	for (i=0;i<numCars;i++){
		gap+= Math.random()*screenWidth*this.scale;
		leftDirectionCars.push(new Car(true, (carWidth*i*this.scale)+gap,this.positionY-(10*this.scale),20,20,1+(speed*this.scale)));
		
		rightDirectionCars.push(new Car(false,(screenWidth)-(i*this.scale*carWidth)-gap,this.positionY-(100*this.scale),20,20,1+(speed*this.scale)));
	
	}
	
	for(i=0;i<numCoins;i++){
			
		var x= Math.random()*(screenWidth-50);
		var y=this.positionY+ (Math.random()*this.sizeY*0.8);
		coins.push(new Coin(x,y))
		
	}
	

}

Road.prototype.drawRoad=function() {
	context.drawImage(this.image,0, this.positionY, this.sizeX, this.sizeY);
	
}

function Chicken (positionX, positionY){
	this.positionX=positionX;
	this.positionY=positionY;
	this.sizeX=75;
	this.sizeY=75;
	this.image = new Image();
	var chick = Math.random();
	if(chick<0.5){
	this.image.src = "chicken2.png";
	}
	else{
		this.image.src = "chicken3.png";
	}
	this.collected=false;
}
Chicken.prototype.draw=function(){
	
	context.drawImage(this.image,this.positionX,this.positionY,(this.sizeX*(this.positionY/scale)) , (this.sizeY*(this.positionY/scale)));
	
}
Chicken.prototype.checkGet=function(){
	//console.log("coin get!");
	if(!this.collected){
	if(this.positionX+this.sizeX*(this.positionY/scale)>foxxy.positionX && 
	this.positionX<(foxxy.positionX+foxWidth*(this.positionY/scale)*0.8)&&
	this.positionY+this.sizeY*(this.positionY/scale)>(foxxy.positionY+(foxHeight*(foxxy.positionY/scale)*0.8))&&
	this.positionY<foxxy.positionY+(foxHeight*(foxxy.positionY/scale))*.9){
		console.log("coin get!");
		this.collected = true;
		
		chickenAudio.currentTime=0;
		chickenAudio.play();
		eatAudio.currentTime=0;
		eatAudio.play();
		if(lives<20){
		
		lives+=2;
		}
		totalScore+=30;
	}
	}
	if(this.collected){
		this.image.src="chickenDead.png"
	}
//check collision
//update crossyfox coins collected
//go up till off the screen	

}

function Coin (positionX, positionY){
	this.positionX=positionX;
	this.positionY=positionY;
	this.sizeX=50;
	this.sizeY=50;
	this.image = new Image();
	
	this.image.src = "coin.png";
	this.collected=false;
}

Coin.prototype.draw=function(){
	if((this.sizeX*(this.positionY/scale))>0){
	context.drawImage(this.image,this.positionX,this.positionY,(this.sizeX*(this.positionY/scale)) , (this.sizeY*(this.positionY/scale)));
	}
}

Coin.prototype.checkGet=function(){
	//console.log("coin get!");
	if(this.positionX+this.sizeX*(this.positionY/scale)>foxxy.positionX && 
	this.positionX<(foxxy.positionX+foxWidth*(this.positionY/scale)*0.8)&&
	this.positionY+this.sizeY*(this.positionY/scale)>(foxxy.positionY+(foxHeight*(foxxy.positionY/scale)*0.8))&&
	this.positionY<foxxy.positionY+(foxHeight*(foxxy.positionY/scale))*.9){
		console.log("coin get!");
		this.collected = true;
		coinAudio.currentTime=0;
		coinAudio.play();
		coinsOnLevel++;
		totalScore+=50;
	}
	
	if(this.collected&&this.positionY>(-50)){
		this.positionY-=60;
	}
//check collision
//update crossyfox coins collected
//go up till off the screen	

}

function checkCoinCollision(){
	for(i=0;i<coins.length;i++){
		coins[i].checkGet();	
				
	}
	
	
}


function drawCoins(){
		for(i=0;i<coins.length;i++){
		coins[i].draw();	
				
	}
	
}

function Car (rightToLeft, startX, startY, minX, maxX, speed){
	this.speed=speed;
	this.slow=false;
	this.startX=startX;
	this.startY=startY;
	this.rightToLeft=rightToLeft
	this.positionX=startX;
	this.positionY=startY;
	this.scale = this.positionY/scale;
	this.sizeX=317*this.scale;
	this.sizeY=200*this.scale;
	this.image = new Image();
	this.inFrontOfFox=false;
	this.inFrontDrawList=false;
		
	if (rightToLeft== true){
		this.image.src = redCarsrc;
	}
	else{
		this.image.src = blueCarsrc;
		
	}
		
}





	
	
	

Car.prototype.collision=function(){
if(this.positionX+this.sizeX>foxxy.positionX+(foxWidth*(this.positionY/scale)*0.2) && 
	this.positionX<(foxxy.positionX+foxWidth*(this.positionY/scale)*0.7)&&
	this.positionY+this.sizeY>(foxxy.positionY+(foxHeight*(this.positionY/scale)*0.9))&&
	this.positionY+this.sizeY*.8<foxxy.positionY+(foxHeight*(this.positionY/scale))
	

	){
		this.inFrontOfFox=false;
		var snow= new Image();
		snow.src="bloodscreen.png";
	
		context.drawImage(snow,0, 0, 583, 700);
		console.log("collision");
		collision=true;
		foxxy.positionY= foxxy.positionY+10;
		carHornAudio.currentTime=0;
		carHornAudio.play();
		foxHurtAudio.currentTime=0;
		foxHurtAudio.play();
		lives--;
	}
	
	else if(this.positionX+(this.sizeX)>foxxy.positionX+(foxWidth*(this.positionY/scale)*0.5) && 
	this.positionX<(foxxy.positionX+foxWidth*(this.positionY/scale)*0.5)&&
	this.positionY+(this.sizeY*0.9)>(foxxy.positionY+(foxHeight*(this.positionY/scale)*0.9))&&
	this.positionY<foxxy.positionY+(foxHeight*(foxxy.positionY/scale))
	
	
	
	
	
	)
	{
	
		this.inFrontOfFox=true;
		//check collision, top half. redraw car
	}
	else{this.inFrontOfFox=false;}
}

Car.prototype.drawCar=function() {
	context.drawImage(this.image,this.positionX, this.positionY, this.sizeX, this.sizeY);
	
}

Car.prototype.moveCar=function(){
	if (this.rightToLeft==true){
		this.positionX-=this.speed;
		if (this.positionX<-this.sizeX){
			this.positionX=screenWidth;
		}
	}
	
	else{
		this.positionX+=this.speed;
		if (this.positionX>screenWidth){
			this.positionX=-(carWidth*this.scale);
		}
	}
		
}
	

function controlButtons(){
	context.fillStyle = "#000000";
	context.font = "40px Chewy";
	
	context.fillText("Controls",screenWidth+90,150);
	context.font = "25px Chewy";
	context.fillText("Movement = arrow Keys",screenWidth+40,200);
	context.fillText("Snow (On/off) = S",screenWidth+80,250);
	context.fillText("(De)Activate Foxxy Time = F",screenWidth+20,300);
	context.fillText("Quit to main Menu = Q",screenWidth+60,350);
	context.fillText("High Score = H",screenWidth+90,400);
	context.fillText("Instuctions(on/off) = I",screenWidth+60,450);
	context.fillText("Watch Ending = V",screenWidth+80,500);
	context.fillText("Extreme Mode = E",screenWidth+75,550);
	if(extremeMode){
				
				context.fillStyle = "#FFFFFF";
				context.font = "30px Chewy";
				
				context.fillText("Extreme Mode",5,690);
		}
}
	
var coundowntoDeath=40;
function displayLives(){
	
	context.fillStyle = "#000000";
	context.font = "25px Chewy";
	context.fillText("Health",0,23);
	context.fillText("Foxy Time",0,50);
	context.fillText("Total Score: "+totalScore,400,23);
	context.fillText("Coins: "+coinsOnLevel+"/"+coins.length,400,50);
	context.fillText("Time Remaining: "+timeRemaining,340,690);
	if(foxxy.alive){
	timeRemaining--;
	if(extremeMode&&timeRemaining>0){
				timeRemaining--;
				}
	}
	for ( i=1;i<=foxxy.foxyTime;i++){
		var timePos =100;
		 context.fillStyle = "#FBA133"; // Fill style to pink
		context.fillRect(timePos+1*i,35,1,17); // Draws pink health squares
		
	}
	
	
	
	 
	 if(lives<1){
		 gameOverAudio.currentTime=0;
		 gameOverAudio.play();
		 addScoreToLeaderboard(totalScore);
		  foxLeftsrc="foxleftdead.png";
		 foxRightsrc="foxrightdead.png";
		 foxxy.alive=false;
		if(coundowntoDeath<1){
			gameState=gameStates.DEAD;
			coundowntoDeath=40;
		}
		coundowntoDeath--;

		 
		 
	 }
	  else if(lives<3){
		  foxLeftsrc="foxleftdying.png";
		 foxRightsrc="foxrightdying.png";
		 
	 }
	  else if(lives<7){
		 foxLeftsrc="foxlefthurt.png";
		 foxRightsrc="foxrighthurt.png";
		 
		 
	 }
	 else{
		 
		 foxLeftsrc="foxLeftSpriteSheet.png";
		 foxRightsrc="foxRightSpriteSheet.png";
	 }
	 
	 
	for ( i=1;i<=lives;i++){
		var healthPos =65;
		 context.fillStyle = "#F888FF"; // Fill style to pink
		context.fillRect(healthPos+10*i,7,10,17); // Draws pink health squares
		
	}
}


function displayInstructions(){
			
				context.fillStyle = "#54C0A0";
				context.fillRect(0,0,screenWidth,screenHeight);
				context.fillStyle = "#000000";
				context.font = "42px Chewy";
				context.fillText("Get Foxxy to the Forest",75,100);
				context.fillText("without getting run over!",60,150);
				context.fillText("Grab the COINS for points and",30,250);
				context.fillText("eat the CHICKENS for health!",40,300);
				context.fillText("FOXXY TIME can slow things down",10,400);
				context.fillText("But dont use it all at once!",80,450);
				
				context.fillText("whatever you do...",120,550);
				context.fillText("DON'T TAKE TOO LONG!",90,600);
			
}

function credits(){
	context.fillStyle = "#000000";
				context.fillRect(0,0,900,700);
				context.fillStyle = "#FFFFFF";
				context.font = "42px Chewy";
				
				context.fillText("THANKS FOR PLAYING",270,250);
				
				context.fillText("PRESS ENTER TO RETURN TO THE MENU",145,600);
	
}
}
