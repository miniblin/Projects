using UnityEngine;
using System.Collections;

public class QuitApp : MonoBehaviour {
	bool quitConfirm=false;
	public void ExitApp(){
		
		Application.Quit();
	}
	
	public void confirmQuit(){
		quitConfirm = true;
	}
	
	public void unconfirmQuit(){
		quitConfirm = false;
	}
	void Update(){
		if (quitConfirm){
			foreach (Transform child in this.transform){
				if (child.name =="Canvas"||child.name =="EventSystem"){
					child.gameObject.SetActive(false);
				}
				if (child.name =="Canvas Confirm"||child.name =="ConfirmEvent"){
					child.gameObject.SetActive(true);
				}
			}
		}
	}
}
