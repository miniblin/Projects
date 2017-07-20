using UnityEngine;
using System.Collections;

public class Satellite : MonoBehaviour {
	public ParticleSystem explosion;
	
	//public AudioSource explodeAudio;
	public ParticleSystem clone;
	private bool alive;
	public GameStatistics stats;
	// Use this for initialization
	void Start () {
		GameObject Statistics = GameObject.Find("GameOverMenuAndHUD");  
		stats = Statistics.GetComponent<GameStatistics >();   
		alive=true;
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(){
		GameObject.Find("Explosion").GetComponent<AudioSource>().Play();
		stats.incsatellitesDestroyed();
		renderer.enabled = false;
		clone = (ParticleSystem)Instantiate(explosion,transform.position,Quaternion.identity);
		Destroy(clone.gameObject,clone.duration);
		alive=false;
		gameObject.active = false;
		
	}
	
	public bool GetAlive(){
		return alive;
	}
	
	
}
