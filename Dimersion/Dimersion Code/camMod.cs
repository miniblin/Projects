using UnityEngine;
using System.Collections;

public class camMod : Movement {
		
private bool sideView;
private Vector3 currentPosition;
public float xOffset, zOffset;
public AudioSource explosion;
public Ship shipScript;
public Vector3 shipPosition;


	
	private void GameOver(){
		
		rigidbody.velocity = new Vector3(0,0,0);
	}
	// Use this for initialization
	void   Start () {
		rigidbody.velocity = new Vector3(0,0,0);
		base.Start();
		
		
		zOffset = -100f;
		xOffset = -8f;
		startPosition = new Vector3(startPosition.x-xOffset,startPosition.y, startPosition.z+zOffset);
		SetSideView(true);
			
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.PlayerDead += PlayerDead;
		GameEventManager.GameOver += GameOver;
		}
	
	private void GameStart(){
		
		transform.position = startPosition;
		shipPosition = shipScript.GetStartPosition();
		SetSideView(sideView);
		
	
	}
	private void PlayerDead(){
	explosion.Play();
	}
	
	

	
	void Update () {
		base.Update();
		shipPosition= shipScript.GetPosition();
	
	
	
	}
	
public void SetSideView(bool sideView){
	this.sideView = sideView;
	if (sideView){
			transform.rotation = Quaternion.Euler(0, 0, 0);
			transform.position = new Vector3 (shipPosition.x-xOffset, shipPosition.y, shipPosition.z + zOffset);
			
			
			Camera.main.orthographic= true;		
		}
		
		else {
			transform.rotation = Quaternion.Euler(0, 90, 0);
			transform.position = new Vector3 (shipPosition.x+xOffset, shipPosition.y, shipPosition.z);
			
			
			
			Camera.main.orthographic= false;
		}
	}
	
	public float GetMaxBounds(){
		float height = 2f* Camera.main.orthographicSize;
		float width = height * Camera.main.aspect;
		return transform.position.x+ (width/2);
	}
	
	
	public float GetMinBounds(){
		float height = 2f* Camera.main.orthographicSize;
		
		float width = height * Camera.main.aspect;
		return transform.position.x- (width/2);
		
	}
	
	public void disableAuthoGraphic(){
		Camera.main.orthographic= false;
	}
	
}
