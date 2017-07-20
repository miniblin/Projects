using UnityEngine;
using System.Collections;
//unused class, dynamically changes lighting to match the audio frequency/amplitude. flashing lights
//turned out to be distracting
public class Light2D : MonoBehaviour {

	
	public float duration = 1.0F;
	public Light lt;
	
	float amplitude;
	float[] spectrum = new float[1024];
	float prev;
	int count;	
	
	//get light component
	void Start() {
		lt = GetComponent<Light>();
		
		prev =0.5f;
		count=0;
		GameEventManager.GameStart += GameStart;
		GameEventManager.PlayerDead += PlayerDead;
		prev =lt.intensity;
	}
	
	
	
	private void GameStart(){
	lt.intensity = prev;
	}
	
	private void PlayerDead(){
		lt.intensity = 0f;
	}
	//update light intensity with  normalised (between zero and 1) audio frequency
	/*
	void Update() {
	
		//float phi = Time.time / duration * 2 * Mathf.PI;
		
	
		
		audio.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
		
		int i = 1;
		amplitude =0;
		while (i < spectrum.Length-1) {
		
			amplitude += spectrum[i];
			//lt.intensity = amplitude;
			//Debug.Log(spectrum[i]);
			//Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
			//Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
			//Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
			//Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.yellow);
			i++;
	}
	
	float phi =100000*(amplitude/1024);
		phi = Mathf.Round(phi);
		//amplitude =  .1f+Mathf.Sqrt(Mathf.Cos(phi)* Mathf.Cos(phi));
		//amplitude=Mathf.Cos(phi) * 0.5F + 0.5F;
		//Debug.Log(phi);
		//phi = .2f+Mathf.Round(phi)/100;
		
		//phi = amplitude;
		//phi = Mathf.Round(phi)/10;
		//Debug.Log(phi);
		
		lt.intensity=phi/10-.3f;
		//Debug.Log(phi);
		
		//Debug.Log(prev/count);
	//	Debug.Log(lt.intensity);
	count++;
	}
	*/
}




