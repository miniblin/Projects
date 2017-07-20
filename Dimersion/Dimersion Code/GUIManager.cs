using UnityEngine;
using System.Collections;
using System;

public class GUIManager : MonoBehaviour {
	public GUIText lives,dangerLevel;
	public LevelOne levelScript;
	
	// Use this for initialization
	void Start () {
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		
		//score.enabled=false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Submit")){
			//GameEventManager.TriggerGameStart();
			//Debug.Log("start Pressed");
		}
		else{
	//	Debug.Log("start button up");
		}
		
		
		
		
		lives.text = "Lives: "+levelScript.GetLives();
		dangerLevel.text="Danger Level: "+ levelScript.GetDangerLevel()+"%";
	}
	
	private void GameStart(){
		
		//enabled = false;
	
	}
	
	private void GameOver(){
		//gameOverText.enabled = true;
	//	instructionsText.enabled = true;
		enabled = true;
		
	}
}
