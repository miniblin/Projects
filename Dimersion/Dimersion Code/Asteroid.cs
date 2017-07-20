using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
	public ParticleSystem explosion;
	public ParticleSystem clone;
	float deadPosition;
	// Use this for initialization
	void Start () {
	deadPosition =300;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(){
		renderer.enabled = false;
		//Destroy(gameObject);
		clone = (ParticleSystem)Instantiate(explosion,transform.position,Quaternion.identity);
		//transform.position =new Vector3( 0,0, Random.Range (1000,1200));
		Destroy(clone.gameObject,clone.duration);
		transform.position =new Vector3(0,0,deadPosition+=10);
	}
	
	
}
