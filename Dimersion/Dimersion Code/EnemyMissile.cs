using UnityEngine;
using System.Collections;

public class EnemyMissile : MonoBehaviour {
	public ParticleSystem explosion;
	public ParticleSystem clone;
	public GameStatistics stats;
	float deadPosition;
	public float accuracy;
	//put stats in prefab somehow?
	
	// Use this for initialization
	void Start () {
		GameObject Statistics = GameObject.Find("GameOverMenuAndHUD");  
		stats = Statistics.GetComponent<GameStatistics >();   
		
		
		deadPosition=300;
		transform.rotation = Quaternion.Euler(0, -90, 0);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetDir = Ship.position - transform.position;
		Debug.DrawRay(transform.position, targetDir, Color.blue);
		
		float step = 0.8f * Time.deltaTime;
		
		
		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0F);
		Debug.DrawRay(transform.position, newDir, Color.red);
		transform.rotation = Quaternion.LookRotation(newDir);
		Debug.Log("accuracy"+accuracy);
		if(Time.timeScale!=0){
		transform.position = Vector3.MoveTowards(transform.position, Ship.position, accuracy);
		}
	}
	
	void OnCollisionEnter(){
		GameObject.Find("Explosion").GetComponent<AudioSource>().Play();
		stats.incMissilesBlownUp();
		renderer.enabled = false;
		clone = (ParticleSystem)Instantiate(explosion,transform.position,Quaternion.identity);
		Destroy(clone.gameObject,clone.duration);
		gameObject.active = false;
	}
	
	public void SetAccuracy(float accuracy){
	
		this.accuracy=accuracy;
	}
}
