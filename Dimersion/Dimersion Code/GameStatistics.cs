using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
//methods in this class are very simple. they do what they say on the tin.
public class GameStatistics : MonoBehaviour {
	public Leaderboard leaderboard;

	private static int currentLevel;
	private static int missilesBlownUp;
	private static int satellitesDestroyed;
	private static float lightYearsTravelled;
	private static int missilesFired;
	private static int bombsDropped;
	private static float accuracy;
	public static float score;
	public static bool perspectiveIs2D;
	public static float hitScore;
	public static float finalScore;
	public static float dangerLevel;
	
	public Text currentLevelText;
	public Text missilesBlownUpText;
	public Text satellitesDestroyedText;
	public Text lightYearsTravelledText;
	public Text missilesFiredText;
	public Text bombsDroppedText;
	public Text accuracyText;
	public Text finalScoreText;
	public GUIText  scoreText;
	
	// Use this for initialization
	void Start () {
		ResetStatistics();
		GameEventManager.GameOver += GameOver;
		GameEventManager.GameStart += GameStart;
		
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = "Score: "+(int)score;
	//	Debug.Log("missilesFired"+missilesFired);
		//Debug.Log("bombs Dropped"+bombsDropped);
	//	Debug.Log("missiles blown up"+missilesBlownUp);
	//	Debug.Log("satellites destoryed"+satellitesDestroyed);
		if ((missilesFired+bombsDropped)>0){
	//	Debug.Log("accuracy update");
			accuracy = (((missilesBlownUp+satellitesDestroyed)*100)/(missilesFired+bombsDropped));
		}
		CalculateScore();
	//	Debug.Log("Score"+score);
		//Debug.Log("accuracy:"+accuracy);
	
		currentLevelText.text = "Level Reached: "+currentLevel;
		missilesBlownUpText.text="Missiles Blown Up: "+missilesBlownUp;
		satellitesDestroyedText.text="Satellites Destroyed: "+satellitesDestroyed;
		lightYearsTravelledText.text="Light Years travelled: "+lightYearsTravelled;
		missilesFiredText.text="Missiles Fired: "+missilesFired;
		bombsDroppedText.text="Bombs dropped: "+bombsDropped;
		accuracyText.text = "Accuracy Bonus: "+accuracy+"% x 10000: "+(accuracy/100)*10000;
		
	//	Debug.Log("final Score"+finalScore);
		finalScoreText.text = "Final Score: "+(int)finalScore;
		
		
	}
	private void GameOver(){
		FinalScore();
		leaderboard.postScoreToLeaderboard(finalScore);
		foreach(Transform child in transform){
			if (child.name =="GUIText"){
				child.gameObject.SetActive(false);
			}
			else{
			child.gameObject.SetActive(true);
			}
		}
	}
	
	public void ResetStatistics(){
		currentLevel=1;
		missilesBlownUp=0;
		satellitesDestroyed=0;
		lightYearsTravelled=0;
		missilesFired=0;
		bombsDropped=0;
		accuracy =0;
		score=0;
		hitScore=0;
		dangerLevel=1;
	}
	
	public void GameStart(){
		dangerLevel=0;
	}
	
	public void incLevel(){
		currentLevel++;
		
	}
	
	public void incMissilesBlownUp(){
		missilesBlownUp++;
		if (!perspectiveIs2D){
			hitScore+=(1000);
		}
		else {
			hitScore+=(100);
		}
	}
	
	public void incsatellitesDestroyed(){
		satellitesDestroyed++;
		DecDangerLevel(0.8f);
		if (!perspectiveIs2D){
			hitScore+=(500);
			
		}
		else {
			hitScore+=(100);
		}
	}
	
	public void setLightYearsTravelled(float lyt){
		lightYearsTravelled = lyt/1000;
	}
	
	public void incMissilesFired(){
		missilesFired++;
		
	}
	
	public void incBombsDropped(){
		bombsDropped++;
	}
	
	public void CalculateScore(){
		score = hitScore + lightYearsTravelled*100;
	}
	
	public void FinalScore(){
		finalScore=score +((accuracy/100)*10000);
	}
	
	public void PerspectiveIs2D(bool perspective){
	perspectiveIs2D = perspective;
	}
	
	public void IncDangerLevel(){
	if(dangerLevel<100){
	dangerLevel+=2;
	}
	Debug.Log("Danger Level Incremented");
	}
	
	public void DecDangerLevel(float decrement){
	if (dangerLevel>1){
	dangerLevel-= decrement;
	}
	}
	
	public float GetDangerLevel(){
	Debug.Log("Danger Level: " +dangerLevel);
	return dangerLevel;
	}
}
