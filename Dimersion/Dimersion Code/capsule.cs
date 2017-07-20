using UnityEngine;
using System.Collections;

public class capsule : MonoBehaviour {
	float timeToLive =4f; // object will self destruct after 4 seconds
	
	void Start () {
				
	}
	
	
	void Update () {
	Destroy(gameObject, timeToLive);
	}
	
	void OnCollisionEnter(){
	Destroy (gameObject);
		
	}
	//switch between 2d and 3d colliders when perspective changes
	public void ActivateCollider2D(bool colliderStatus){
		this.GetComponent<BoxCollider>().enabled =colliderStatus;
		this.GetComponent<CapsuleCollider>().enabled =!colliderStatus;
	}
	
	
}
