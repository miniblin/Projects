using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
	bool pause=false;
	bool QuitConfirm=false;
	float pauseMenuX;
	float pauseMenuY;
	float pauseMenuWidth;
	float pauseMenuHeight;
	public GameObject light2D;
	public GameObject light3D;
	
	// Use this for initialization
	void Start () {
		pauseMenuWidth=700;
		pauseMenuHeight=30;
		pauseMenuX=Screen.width/2-(pauseMenuWidth/2);
		pauseMenuY=50;
		
	}
	public void LoadScene (int scene){
		UnPause();
		GameEventManager.Nullify();
		Application.LoadLevel(scene);
		Time.timeScale=1.0f;
	}
	
	public void UnPause(){
	
		pause=false;
	}
	
	public void confirmQuit(){
		QuitConfirm = true;
	}
	
	public void unconfirmQuit(){
		QuitConfirm = false;
		pause=false;
	}
	
	
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Submit")){
			Debug.Log("Game should be triggered");
			//GameEventManager.TriggerGameStart();
			pause=!pause;
		}
		if (pause){
			Time.timeScale=0.0f;
			light2D.SetActive(false);
			light3D.SetActive(false);
			
		}
		else{
			light2D.SetActive(true);
			light3D.SetActive(true);
			Time.timeScale=1.0f;
		}
		
	}
	void OnGUI(){
		
		if(pause){
			GUI.Box(new Rect(pauseMenuX, pauseMenuY, pauseMenuWidth, pauseMenuHeight), "Game Paused");	
			//GUI.Button(new Rect(920,100,80,20),"Resume");
			
			foreach (Transform child in this.transform){
							
			if (child.name =="Canvas Pause"||child.name =="PauseEvent"){
					child.gameObject.SetActive(true);
				}
			}
		}
		else{
			foreach (Transform child in this.transform){
				child.gameObject.SetActive(false);
			}
		}
		
		if (QuitConfirm){
			foreach (Transform child in this.transform){
				if (child.name =="Canvas Pause"||child.name =="PauseEvent"){
					child.gameObject.SetActive(false);
				}
				if (child.name =="Canvas Confirm"||child.name =="ConfirmEvent"){
					child.gameObject.SetActive(true);
				}
			}
		}
		
		
		
		
		
		
		
		
	}
}
