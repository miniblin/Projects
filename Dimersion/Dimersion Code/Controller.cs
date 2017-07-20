using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	
	public Ship shipScript;
	public camMod cameraScript;
	private enum Perspective{SideView,RearView};
	Perspective perspective;
	float YAxisValue;
	float XAxisValue;
	bool pause=false;
	// Use this for initialization
	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		perspective = Perspective.SideView;
		shipScript.ActivateCollider2D(true);
		shipScript.ResetRotation();
		shipScript.MoveLeft(0); //initalise ship as stationary
		
		cameraScript.SetSideView(true);
		cameraScript.GetMaxBounds();
	}
	private void GameStart(){
		perspective = Perspective.SideView;
		shipScript.ActivateCollider2D(true);
		shipScript.ResetRotation();
		shipScript.MoveLeft(0);
		
		cameraScript.SetSideView(true);
		cameraScript.GetMaxBounds();
	}
	private void GameOver(){
	gameObject.SetActive(false);
	}
	
	
	void Update () {
		XAxisValue = Input.GetAxis("Horizontal");
		YAxisValue = Input.GetAxis("Vertical");
				
		
		
		if (perspective == Perspective.RearView){
				shipScript.MoveUp(YAxisValue);
				cameraScript.MoveUp(YAxisValue);
				
				
		
			shipScript.MoveLeft(XAxisValue);
			cameraScript.MoveLeft(XAxisValue);
			
			if (Input.GetButtonDown ("persp2d")){
				
				shipScript.ResetRotation();
				shipScript.MoveLeft(0);
				perspective = Perspective.SideView;
				shipScript.ActivateCollider2D(true);
				cameraScript.SetSideView(true);
				cameraScript.GetMaxBounds();
			}
		}
		     
		
	
		else if (perspective == Perspective.SideView){
			shipScript.MoveUp(YAxisValue);
				cameraScript.MoveUp(YAxisValue);
				
			
			shipScript.MoveForward(-XAxisValue);
								
		
		
		
	
	}
}
