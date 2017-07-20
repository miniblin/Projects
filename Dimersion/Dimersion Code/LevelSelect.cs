using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {
	private static int level;
	public Text levelText;
	public GameProgress gameProgress;
	
	// initialise selected level as 1
	void Start () {
		
		level=1;
		levelText.text = ""+level;
	}
	
	
	void Update () {
		levelText.text = ""+level;
		Debug.Log (level);
	}
	
	//check level has been unlocked before incrmenting
	public void IncrementLevel(){
		if (checkUnlocked(level+1)){
			level++;
			
						
			}
	}
	
	public static int GetLevel(){
	return level;
	}
	
	public void DecrementLevel(){
		if (level>1){
			
			level--;
			
		}
	}
	
	public bool checkUnlocked(int level){
	return (gameProgress.GetProgress()>= level);
		
	}
}
