using UnityEngine;
using System.Collections;
using System;



public class GameProgress : MonoBehaviour {
	public int levelUnlocked;
	
	void Start () {
	
		RetrieveProgressFromFile();
		Debug.Log("read from file level No: "+levelUnlocked);
	}
	
	
	void Update () {
		
	}
	
	public void SaveUserProgress(int level){
		if(level>levelUnlocked){
			levelUnlocked=level;
		System.IO.File.WriteAllText("./userProgress.txt", level.ToString());
		
		}
	}
	
	public void RetrieveProgressFromFile(){	
		try{
		string level = System.IO.File.ReadAllText("./userProgress.txt");
		Debug.Log("string from file: "+level);
				
		int levelInt;
		Int32.TryParse(level,out levelInt);
		if (levelInt<1){
			levelUnlocked=1;
		}
		else{
			levelUnlocked=levelInt;
			}
	}
	catch (Exception e){}
	
		}
	
	public int GetProgress(){
		return levelUnlocked;
	}

}
