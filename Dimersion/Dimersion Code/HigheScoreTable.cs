using UnityEngine;
using System.Collections;

public class HigheScoreTable : MonoBehaviour {
	public Leaderboard leaderboard;
	private string highScores;
	// Use this for initialization
	void Start () {
		transform.position= new Vector3(400,0,0);
		//leaderboard.CreateUser();
		leaderboard.getScoresFromLeaderboard();
		// leaderboard.getPage();
	
	}
	
	// Update is called once per frame
	void Update () {
		
	
	}
	
	
	

}
