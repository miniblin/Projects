using UnityEngine;
using System.Collections;

public class LoadOnCLick : MonoBehaviour {
	//public GameObject loadingImage;
	// Use this for initialization
	
		public void LoadScene (int scene){
		
		if (scene ==1 && LevelSelect.GetLevel() ==1){ //load tutorial
			Application.LoadLevel(3);
			Time.timeScale=1.0f;
			}
		else if(scene==4){     //loads single player after tutorial
			GameEventManager.Nullify();
			Application.LoadLevel(1);
			Time.timeScale=1.0f;
		}
			else{
//	loadingImage.SetActive(true);
		GameEventManager.Nullify();
		Application.LoadLevel(scene);
		Time.timeScale=1.0f;
		}
		
	}
}
