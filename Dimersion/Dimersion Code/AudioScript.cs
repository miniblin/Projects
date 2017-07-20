using UnityEngine;
using System.Collections;
//keeps audio instances alive
public class AudioScript : MonoBehaviour {

	// Use this for initialization
	public static AudioScript AudioInstance;
	// Use this for initialization
	void Awake(){
		if(AudioInstance){
			DestroyImmediate(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
			AudioInstance = this;
		}
		
	}
}
