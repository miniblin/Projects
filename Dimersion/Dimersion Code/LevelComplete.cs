using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//this script affects the level complete message displayed to the user
public class LevelComplete : MonoBehaviour {

	public Text LevelCompleted;
	public Text levelUnlocked;
	public Text shipUnlocked;
	
	private int level;
	private int progress;
	
	public LevelOne levelScript;
	public GameProgress progressScript;
	// Use this for initialization
	void Start () {
		
		LevelCompleted.text = "";
		levelUnlocked.text="";
		shipUnlocked.text="";
		}
		
	
	//display level complete message, if new level unlocked let user know.
	//if game completed, congratulate the user.
	void Update () {
		//check level
		//check user progress
		//
		level=levelScript.GetLevel();
		progress = progressScript.GetProgress();
		if (level<LevelOne.maxLevel){
		LevelCompleted.text = "Level "+level+" Completed";
		if (level>=progress){
		levelUnlocked.text="Level "+(level+1)+" Unlocked";
		shipUnlocked.text="New Ship Colour Unlocked";
		}
		}
		else{
			LevelCompleted.text = "Wow, Youve Finished the Game";
			levelUnlocked.text="Now see if you can get to the top of the leaderBoard!";
			shipUnlocked.text="";
		}
		
		
		

	// Use this for initialization
	
}
}
