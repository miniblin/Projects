using UnityEngine;
using System.Collections;

public class Ship : Movement {
	
	public static float distanceTravelled;
	public static Vector3 position;
	public float rollRotation;
	public float yawRotation;
	
	public bool rolling,tipping;
	public float distance;
	public ParticleSystem explosion,trail;
	public AudioSource explodeAudio,move;
	private ParticleSystem clone;
	private bool isDead;
	//MeshCollider hitbox;
	private int lives;
	public Missile missile;
	private RaycastHit hitPoint; 
	private float sightDistance=45;
	private bool foundHit;
	public Transform crossHair; 
	public Vector3 startPosition;
	public Vector3 analogueDirection;
	public camMod camera;
	private bool perspectiveIs2D;
	public GameObject collider3D;
	public GameObject collider2D;
	public GameProgress gameProgress;
	private bool rotating = true;
	public float velocity;
	public GameStatistics stats;
	public GameObject deathScreen;

	
	void Start () {
		
		collider3D = transform.FindChild("Collider3D").gameObject;
		collider2D = transform.FindChild("Collider2D").gameObject;
		distance =0;
		analogueDirection = new Vector3(90,0,0);
		base.Start();
		lives=3;
		SetShipColour();
		QualitySettings.vSyncCount = 1;
		rollRotation=0;
		yawRotation =0;
		rolling=false;
		tipping=false;
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		isDead = true;
		//hitbox = collider as MeshCollider;
		startPosition = new Vector3 (-14.8f,2f,0f);
		GameEventManager.TriggerGameStart();
	}
	private void GameOver(){
		Debug.Log("triggered GameOver");
		stats.setLightYearsTravelled(distance);
		gameObject.SetActive(false);
		Time.timeScale=0.0f;
	}
	private void GameStart(){
		
		foreach(Transform child in transform){
			child.gameObject.SetActive(true);
		}
		SetShipColour();
		//rigidbody.isKinematic = false;	
		transform.position = new Vector3 (-14.8f,2f,0f);
		renderer.enabled = true;
		trail.renderer.enabled = true;
		distanceTravelled =0;
		explosion.Clear();
		isDead=false;
		
		
	}
	
	void FixedUpdate(){
		if (transform.position.x <camera.GetMinBounds()){
			rigidbody.velocity = new Vector3(0,rigidbody.velocity.y,rigidbody.velocity.z);
			transform.position = new Vector3(camera.GetMinBounds(),transform.position.y,transform.position.z);
			rigidbody.AddForce (new Vector3(500,0,0));
		}
		
		if (transform.position.x >camera.GetMaxBounds()){
			rigidbody.velocity = new Vector3(0,rigidbody.velocity.y,rigidbody.velocity.z);
			transform.position = new Vector3(camera.GetMaxBounds(),transform.position.y,transform.position.z);
			rigidbody.AddForce (new Vector3(500,0,0));
		}
		
		stats.setLightYearsTravelled(distance+100);
		
		float step = 2f * Time.deltaTime;
		
		//Vector3 direct = Quaternion.AngleAxis(z, Vector3.forward) * Vector3.right*100;
		Vector3 newDir = Vector3.RotateTowards(transform.forward, analogueDirection, step, 0F);
		Debug.DrawRay(transform.position, analogueDirection, Color.green);
		//Debug.DrawRay(transform.position, direct, Color.red);
		//Debug.Log("look rote: "+ newDir);
		Debug.DrawRay(transform.position, newDir*30, Color.red);
		transform.rotation = Quaternion.LookRotation(newDir);
		float tilt = analogueDirection.z * 15.0f; // might be negative, just test it
		Vector3 euler = transform.localEulerAngles;
		euler.z = Mathf.Lerp(euler.z, tilt, 2.0f * Time.deltaTime);
		
		transform.localEulerAngles = euler;
		}
	
	void Update () {
		base.Update();
		velocity = rigidbody.velocity.x;
		
		Vector3 targetDir = transform.right*100;
		Debug.DrawRay(transform.position, targetDir, Color.blue);
			
		
		
		
		if(!isDead){distance++;}			
		distanceTravelled = transform.localPosition.x;	
		distance = distanceTravelled;
		
		if(yawRotation == 0 && rollRotation ==0){
		//transform.rotation = Quaternion.identity;
		}
		
		position = transform.position;
		
		if (Physics.Raycast(transform.position+transform.forward, transform.forward,out hitPoint,sightDistance)){
			//Debug.Log("Somethng in front");
			foundHit = true;
		}
		else{
			foundHit=false;
		}
		//Debug.Log(hitPoint.point);
		if (foundHit){
			
			crossHair.position = hitPoint.point;
		}
		
		else{
			crossHair.position = (transform.position+transform.forward*sightDistance);
		}
		
		
		}
	
	
	void OnCollisionEnter(){
		if (!isDead){
			
			isDead= true;
			foreach(Transform child in transform){
				child.gameObject.SetActive(false);
				}
			explodeAudio.Play();
			clone = (ParticleSystem)Instantiate(explosion,transform.position,Quaternion.identity);
			GameEventManager.TriggerPlayerDead();
			renderer.enabled=false;
			trail.renderer.enabled = false;
			Destroy(clone.gameObject,clone.duration);
			
			deathScreen.SetActive(true);
			StartCoroutine(WaitToRestart(3.0f));
		}
		
	}
	
	
	private IEnumerator WaitToRestart(float seconds)
	{
		
		yield return new WaitForSeconds(seconds);
		DecrementLives();
		deathScreen.SetActive(false);
		
	}
	void HideExplosion(){
		
		explosion.renderer.enabled = false;
		
	}
	
	public override void MoveUp(float force){
		analogueDirection.y=force*90;
		yawRotation++;
		tipping = true;
		base.MoveUp(force);
		
	}
	public void MoveDown(float force){
		base.MoveDown(force);
		transform.Rotate (new Vector3(0f,0f,-.3f), Space.World);
		yawRotation--;
		tipping = true;
	}
	
	public override void MoveLeft(float force){
		analogueDirection.z=force*90;
		base.MoveLeft( force);
		rollRotation=force*90;
		rolling =true;
		if(!move.isPlaying){
			move.Play();
		}
	}
	
	public void MoveRight(float force){
		base.MoveRight(force);
		transform.Rotate (new Vector3(-2f,0f,0f), Space.World);
		rollRotation--;
		rolling =true;
		if(!move.isPlaying){
			move.Play();
		}
	}
	
	
	public int GetLives(){
	return lives;
	}
	
	public void FireMissile(bool isBomb){
	if(!isDead){
		
			if(isBomb){stats.incBombsDropped();}
			else {stats.incMissilesFired();}
			missile.FireMissile(transform.forward,perspectiveIs2D,isBomb);
		}
	}
	
	
	
	public void StopMovementAudio(){
		move.Stop();
	}
	
	public void StopRolling(){
		
		rolling = false;
	}
	
	public void StopTipping(){
		tipping = false;
	}
	
	public Vector3 GetPosition(){
		return transform.position;
	}
	
	public Vector3 GetStartPosition(){
		return startPosition;
		}
	
	public void ResetRotation(){
		//Debug.Log("fix rotation");
		transform.rotation =Quaternion.Euler(0, 90, 0);
	}
	
	
	public void ActivateCollider2D(bool colliderStatus){
		
	perspectiveIs2D = colliderStatus;
		stats.PerspectiveIs2D(perspectiveIs2D);
		collider2D.SetActive(colliderStatus);
		collider3D.SetActive(!colliderStatus);
		SetShipColour();
	}
	
	public void SetShipColour(){
		int shipLevel = gameProgress.GetProgress();
		float opacity;
		if (perspectiveIs2D){opacity=1f;}else{opacity=0.5f;}
		
		switch (shipLevel){
		case 1:
			gameObject.renderer.material.color= new Color(1.0f,1.0f,1.0f,opacity);
		break;
		case 2:
			gameObject.renderer.material.color= new Color(1.0f,0.0f,0.0f,opacity);//red
		break;
		case 3:
			gameObject.renderer.material.color= new Color(1.0f,0.92f,0.016f,opacity);//yellow
		break;
		case 4:
			gameObject.renderer.material.color= new Color(0.0f,1.0f,0.0f,opacity);//green
		break;
		case 5:
			gameObject.renderer.material.color= new Color(1.0f,0.0f,1.0f,opacity);//magenta
		break;
		default:
			gameObject.renderer.material.color= new Color(1.0f,1.0f,1.0f,opacity);
		break;
		}
		
		
	}
	
	public void DecrementLives(){
		lives--;
		if (lives>0){
			GameEventManager.TriggerGameStart();
			}
		else{
				GameEventManager.TriggerGameOver();
		}
	}
}
