using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	protected float movementSpeed;
	protected Vector3 startPosition;
	
	// Use this for initialization
	protected virtual void Start () {
		movementSpeed =75;
		startPosition = new Vector3 (-14.8f,2f,0f);
		GameEventManager.GameStart += GameStart;
		
	}
	
	private void GameStart(){
		rigidbody.velocity = new Vector3(0,0,0);
		rigidbody.AddForce (new Vector3(500,0,0));
	}
	
	// Update is called once per frame
	
	
	protected void Update(){
		if ((transform.position.y <-20 && rigidbody.velocity.y<0 ||transform.position.y >20 && rigidbody.velocity.y>0 ) ){
			rigidbody.velocity = new Vector3(rigidbody.velocity.x,0f,rigidbody.velocity.z);
		}
		
		if (transform.position.z >15 && rigidbody.velocity.z>0 ){
			rigidbody.velocity = new Vector3(rigidbody.velocity.x,rigidbody.velocity.y,0f);
			transform.position = new Vector3(transform.position.x,transform.position.y,15);
		}
		
		if (transform.position.z <-15 && rigidbody.velocity.z<0 ){
			rigidbody.velocity = new Vector3(rigidbody.velocity.x,rigidbody.velocity.y,0f);
			transform.position = new Vector3(transform.position.x,transform.position.y,-15);
		}
		
		
		
	//developer defined functions
	}
	
	
	public virtual void MoveUp(float force){
		force=force/2;
		rigidbody.AddForce (new Vector3(0,movementSpeed*force,0));
		
		
		
	}
	public void MoveDown(float force){
		force=force/2;
		rigidbody.AddForce (new Vector3(0,-movementSpeed,0));
		
		
	}
	
	public virtual void MoveLeft(float force){
		force=force/2;
		rigidbody.AddForce (new Vector3(0,0,movementSpeed*force));
		
		
	}
	
	public void MoveRight(float force){
		force=force/2;
		rigidbody.AddForce (new Vector3(0,0,-movementSpeed));
		
		
	}
	
	public void MoveForward(float force){
		force=force/2;
		rigidbody.AddForce (new Vector3(movementSpeed*force,0,0));
		
	}
	
	
	public void StopDepthMovement(){
		if (rigidbody.velocity.z <15 && rigidbody.velocity.z >-15 ){
			rigidbody.velocity = new Vector3(rigidbody.velocity.x,rigidbody.velocity.y,0f);
		}
		if (rigidbody.velocity.z > 0){
			rigidbody.AddForce (new Vector3(0,0,-2f*movementSpeed));
		}
		if (rigidbody.velocity.z <0){
			rigidbody.AddForce (new Vector3(0,0,+2f*movementSpeed));
		}
	}
	
	public void StopVerticalMovement(){
		if (rigidbody.velocity.y <20 && rigidbody.velocity.y >-20 ){
			rigidbody.velocity = new Vector3(rigidbody.velocity.x,0f,rigidbody.velocity.z);
		}
		
		if (rigidbody.velocity.y > 0){
			rigidbody.AddForce (new Vector3(0,-2f*movementSpeed,0));
		}
		if (rigidbody.velocity.y <0){
			rigidbody.AddForce (new Vector3(0,+2f*movementSpeed,0));
		}
		
	}
	
	public void StopHorizontalMovement(){
		if (rigidbody.velocity.x <20 && rigidbody.velocity.x >-20 ){
			rigidbody.velocity = new Vector3(10,rigidbody.velocity.y,rigidbody.velocity.z);
		}
		
		if (rigidbody.velocity.x > 10){
			rigidbody.AddForce (new Vector3(-2f*movementSpeed,0,0));
		}
		if (rigidbody.velocity.x <10){
			rigidbody.AddForce (new Vector3(+2f*movementSpeed,0,0));
		}
		
	}
}
