using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelOne : MonoBehaviour {
	public static int maxLevel=5;
	public GameStatistics gameStats;
	public GameProgress progress;
	public bool destroyQueueEmpty;
	public bool levelQueueEmpty;
	private int level;
	public GameObject floor,sensor,ceiling, missile;
	public Ship shipScript;
	private Queue<GameObject> levelQueue, destroyQueue;
	public Texture2D levelTexture;
	public Texture2D levelTexture2;
	public Texture2D levelTexture3;
	public Texture2D []levels;
	private float drawDistance,killDistance;
	private int levelX, levelY;
	private float missileAccuracy;
	private int dangerLevel;
	private int score;
	bool begin =true;
	public camMod camera;
	private float xPosition, yPosition, zPosition, unitSizeX, unitSizeY; 
	public GameObject deathScreen;
	// Use this for initialization
	void Start () {
		destroyQueueEmpty=false;
		levelQueueEmpty=false;
		Debug.Log("loading Level");
		level=LevelSelect.GetLevel();
		LoadLevel(level);
			score =0;
			
			
			levelX=0;
			levelY =0;
			levelQueue = new Queue<GameObject>((levelTexture.width * levelTexture.height));
			destroyQueue = new Queue<GameObject>(500);
			xPosition = 100;
			yPosition = -20;
			zPosition = -10;
			drawDistance = 180;
			killDistance =50;
			Vector3 scale = new Vector3(2f,50f,10f);
			floor.transform.localScale = scale;
			ceiling.transform.localScale = scale;
			unitSizeY=floor.transform.localScale.y;
			unitSizeX=floor.transform.localScale.x;
			GameEventManager.GameStart += GameStart;
			GameEventManager.PlayerDead += PlayerDead;
			GameEventManager.GameOver += GameOver;
		
	
	}
	private void GameOver(){
		
		gameObject.SetActive(false);
	}
	
	void Update () {
		dangerLevel=(int)gameStats.GetDangerLevel();
		missileAccuracy = 0.005f*((float)dangerLevel);
		if(begin){
			GameEventManager.TriggerGameStart();
			begin=false;
			}
				
		score= (int)shipScript.GetPosition().x +14;
		DrawLevel();
		DestroyLevel();
		
		if (destroyQueueEmpty&& levelQueueEmpty){
			Debug.Log("levelComplete");
			LevelComplete();
		}
	}
	
	private void GameStart(){
		SetLevelColour( level);
		Debug.Log("Level gamestart running");
		dangerLevel =1;
		missileAccuracy=0.1f;
		foreach (GameObject element in destroyQueue){
			Destroy(element.gameObject);
			
		}destroyQueue.Clear();
		foreach (GameObject element in levelQueue){
			Destroy(element.gameObject);
			
		}levelQueue.Clear();
		
		levelX=0;
		xPosition =100;
	
		ReadTextures();
		
	}
		
		
	
	
	private void PlayerDead(){
				
		}
	
	void ReadTextures(){
	for (int x =0; x<levelTexture.width;x++){
		for (int y =0 ; y<levelTexture.height; y++){
	 
				QueueLevel(levelTexture.GetPixel(x,y),xPosition,yPosition,zPosition);
				QueueLevel(levelTexture2.GetPixel(x,y),xPosition,yPosition,zPosition+10);
				QueueLevel(levelTexture3.GetPixel(x,y),xPosition,yPosition,zPosition+20);
		
			yPosition+=1;
		}
		xPosition+=unitSizeX*1f;
		yPosition=-20;
	}	
						
}
	
	void DestroyLevel(){
	try{
			if (destroyQueue.Peek().transform.localPosition.x <camera.GetMinBounds()){
			GameObject o =destroyQueue.Dequeue();
			SetDangerLevel(o);
			
			Destroy(o.gameObject);
			}
		}
		catch (System.Exception e){
			
		}
		try{
		if (destroyQueue.Count==0){
				destroyQueueEmpty=true;
			}
			else{
				
				destroyQueueEmpty=false;
			}
			}
			catch(System.Exception notBuiltYet){}
		
		
		
	}	
	
	
	void DrawLevel(){
	try{
		if (levelQueue.Peek().transform.localPosition.x < Ship.distanceTravelled + drawDistance){
			GameObject o = levelQueue.Dequeue();
			o.gameObject.SetActive(true);
			if (o.GetComponent<EnemyMissile>()){
				Debug.Log("applynig accuracy to missile"+missileAccuracy);
				o.GetComponent<EnemyMissile>().SetAccuracy(missileAccuracy);
				}
			destroyQueue.Enqueue(o);
			}
		if (levelQueue.Count==0){
			levelQueueEmpty=true;
		}
		else{
			levelQueueEmpty=false;
		}
		
	}
	catch (System.Exception e){}
	}
		
		
void QueueLevel(Color pixelColour,float x,float y, float zDepth){
					GameObject o;
			
			if(pixelColour==Color.black ){
					o = (GameObject)Instantiate(
					floor, new Vector3(x,y,zDepth), Quaternion.identity);
					o.gameObject.SetActive(false);
					levelQueue.Enqueue( o);
					}
			else if(pixelColour==Color.green ){
				o = (GameObject)Instantiate(
				ceiling, new Vector3(x,y,zDepth), Quaternion.identity);
				o.gameObject.SetActive(false);
				levelQueue.Enqueue( o);
			}
			
			else if (pixelColour==Color.red){
					zDepth = Random.Range (zDepth-5, zDepth +5) ;
					o = (GameObject)Instantiate(
					sensor, new Vector3(x,y,zDepth), Quaternion.identity);
					
					o.gameObject.SetActive(false);
					levelQueue.Enqueue( o);
				}	
				
			else if (pixelColour==Color.magenta){
					zDepth = Random.Range (zDepth-5, zDepth +5) ;
					//Debug.Log("creatng missile");
					o = (GameObject)Instantiate(
					missile, new Vector3(x,y,zDepth), Quaternion.identity);
					o.gameObject.SetActive(false);
					levelQueue.Enqueue( o);
			}	
			
			//	}
		
		

}
	
void SetDangerLevel(GameObject o){
		Debug.Log("SEtting d Level");
		if (o.GetComponent<Satellite>()){
			Debug.Log("Sattelite found");
			if (o.GetComponent<Satellite>().GetAlive()){
				
				Debug.Log("missile acc incremented");
				gameStats.IncDangerLevel();
				
				//this works.
				}
			
		}
}

bool doOnce =true;
void LevelComplete(){
if(doOnce){
doOnce=false;
		foreach(Transform child in transform){
			
			child.gameObject.SetActive(true);
		}
		
		StartCoroutine(WaitToRestart(3.0f));
	}
		
}

	





private IEnumerator WaitToRestart(float seconds)
{
	
	yield return new WaitForSeconds(seconds);
		foreach(Transform child in transform){
			
			child.gameObject.SetActive(false);
		}
	level++;
		if(level>maxLevel){
		GameEventManager.TriggerGameOver();
		}
		else{
	LoadLevel(level);
	
	GameEventManager.TriggerGameStart();
	
	progress.SaveUserProgress( level);
	}
	doOnce=true;
	
}

void SetLevelColour(int level){
		switch (level){
		case 1:
			ceiling.renderer.material.color= new Color(0.0f,0.0f,1.0f,1f);//blue
			floor.renderer.material.color= new Color(0.0f,0.0f,1.0f,1f);//blue
			
			break;
		case 2:
			ceiling.renderer.material.color= new Color(.0f,1.0f,0.0f);//green
			floor.renderer.material.color= new Color(.0f,1.0f,0.0f,1f);//green
			break;
		case 3:
			ceiling.renderer.material.color= new Color(0.0f,0.0f,1.0f,1f);//blue
			floor.renderer.material.color= new Color(0.0f,0.0f,1.0f,1f);//blue
			break;
		case 4:
			ceiling.renderer.material.color= new Color(0.0f,1.0f,1.0f,1f);//cyan
			floor.renderer.material.color= new Color(0.0f,1.0f,1.0f,1f);//cyan
			break;
		case 5:
			ceiling.renderer.material.color= new Color(1.0f,0.0f,1.0f,1f);//magenta
			floor.renderer.material.color= new Color(1.0f,0.0f,1.0f,1f);//magenta
			break;
		default:
			ceiling.renderer.material.color= new Color(1.0f,0.0f,0.0f,1f);//red
			floor.renderer.material.color= new Color(1.0f,0.0f,0.0f,1f);//red
			break;
		}
		
}



void LoadLevel(int level){
		Debug.Log("called load level");
		int textPosition = (level-1)*3;
		Debug.Log("level"+textPosition);
		levelTexture=levels[textPosition];
		levelTexture2=levels[textPosition+1];
		levelTexture3=levels[textPosition+2];
	
}


public int GetLevel(){
	return level;
}



public int GetLives(){
	return shipScript.GetLives();
}

public int GetScore(){
	return score;
}

public int GetDangerLevel(){
	return dangerLevel;
	
}

public float GetMissileAccuracy(){
	return missileAccuracy;
}
}
