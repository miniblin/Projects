using UnityEngine;
using System.Collections;

public static class GameEventManager{
	public delegate void GameEvent();
	
	public static event GameEvent GameStart, GameOver, PlayerDead;
	public static void Nullify(){
		GameStart = null;
		GameOver = null;
		PlayerDead=null;
		
	}
	public static void TriggerGameStart(){
		
		Debug.Log("gamestart triggered");
		if (GameStart != null){
		Debug.Log("gamestart not null");
			GameStart();
		}
		else{
			Debug.Log("gamestart is null");
		}
	
	}
	
	public static void TriggerGameOver(){
		
		if (GameOver != null){
			GameOver();
		}	
	}
	public static void TriggerPlayerDead(){
		if (PlayerDead != null){
			PlayerDead();
		}	
	}
}
