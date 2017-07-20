using UnityEngine;
using System.Collections;
using System;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
 

public class Leaderboard : MonoBehaviour {

	string userMacAddress;
	string displayName;
	WWW hs_post;
	bool downloaded=false;
	
	//gets users mac address on startup 
	void Start () {
		userMacAddress=GetMacAddress(); //primary key for database
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//code searches through network cards and retrieves user's mac address
	private string GetMacAddress()
	{
		string macAddresses = string.Empty;
		
		foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
		{
			if (nic.OperationalStatus == OperationalStatus.Up)
			{
				macAddresses += nic.GetPhysicalAddress().ToString();
				break;
			}
		}
		
		return macAddresses;
	}
	
	//calls php page to create user, sends mac address and windows environment name
	public void CreateUser(){
		Debug.Log(Environment.UserName);
		string post_url ="davethings.com/updateLeaderboard.php"+"?functionNo=1" +"&userMacAddress="+userMacAddress+"&displayName="+ Environment.UserName;
		hs_post = new WWW(post_url);
	}
	
	//checks if user exists, if false,creates user, then calls php page to add high score
	public void postScoreToLeaderboard(float score){
			
		CreateUser();
	
			
		string post_url ="davethings.com/updateLeaderboard.php"+"?functionNo=2" +"&userMacAddress="+userMacAddress+"&score="+score;
	
		// Post the URL to the site and create a download object to get the result.
		
		
		
		hs_post = new WWW(post_url);
	}
	//waits for download of page information
	public IEnumerator finishDownload(){
		string post_url ="davethings.com/updateLeaderboard.php";
		
		hs_post = new WWW(post_url);
		
		yield return hs_post;
		
		downloaded=true;
		
		
	}
	//grabs high score data from web page using regex, display it in table in game, displays loading message while loading.
	void OnGUI(){
		float startX = (Screen.width/2)-(900/2);
		float startY =100;
		String leaderboardText="";
		try{
			leaderboardText=hs_post.text;
			}
			catch (Exception E){}
		if(downloaded){
		if(leaderboardText!=""){
		
		
		
			String pattern =@"<td.*?>(.*?)<\/td>";// match all items between td tags. using look behind and look ahead
			int row=1;
			int column =0;
		
		
			GUI.skin.box.fontSize = 25;
			GUI.Box(new Rect(startX+50,startY+ 35, 300, 40), "Name");
			GUI.Box(new Rect(startX+350, startY+35, 300, 40), "Score");
			GUI.Box(new Rect(startX+650,startY+ 35, 300, 40), "Date");
			//GUI.Box(new Rect(0, 70, 200, 30), "1");
		foreach (Match m in Regex.Matches(hs_post.text,pattern)){
		
						
				GUI.Box(new Rect(startX+50+300*column, startY+35+ 40*row, 300, 40), ""+m.Groups[1].Value);
		column++;
		if(column%3==0){row++;column=0;
					GUI.Box(new Rect(startX+5,startY+ 40*row, 30, 40), ""+(row-1));}
		}
		}
		
		else{
			GUI.skin.box.fontSize = 30;
			GUI.Box(new Rect(startX,startY, 900, 60), "Downloading... Please Ensure you are connected to the internet");
		}
		}
	}
	
	public void getScoresFromLeaderboard(){
		StartCoroutine(finishDownload());
				
	}
	
	public string getPage(){
	   return hs_post.text;
	}
	
}
	
