using UnityEngine;
using System.Collections;

public class BlockFade : MonoBehaviour {
	GameObject mainCam;
	GridMaster myMaster;

	Color myColor;
	
	bool fadeOut;
	bool fadeIn;
	int fadeState;
	Color col1;
	Color col2;
	float alpha;
	public float fadeTime;
	float fadeStartTime;
	float fracJourney;
	float resetTimer;

	// Use this for initialization
	void Start () {
		mainCam = GameObject.Find ("Main Camera");
		myMaster = mainCam.GetComponent<GridMaster> ();
		int randCol1 = Random.Range (0, 7);
		int randCol2 = Random.Range (0, 7);
		col1 = myMaster.colorPalette [randCol1];
		col2 = myMaster.colorPalette [randCol2];
		//this.GetComponent<TextMesh>().text = 
	}
	
	// Update is called once per frame
	void Update () {
		/*
		switch (fadeState) {
		case 0:
			col1 = col2;
			col2 = myMaster.colorPalette[Random.Range(0,7)];
			fadeStartTime = Time.time;
			chooseNewColor = false;
			fadeState = 1;
			break;
		case 1:
			fracJourney = (Time.time - fadeStartTime) / (fadeTime/2);
			
			this.renderer.material.color = Color.Lerp(col1,Color.black,fracJourney);
			
			if(fracJourney >= 1){
				this.renderer.material.color = Color.black;
				fadeStartTime = Time.time;
				fadeState = 2;
			}
			break;
		case 2:
			fracJourney = (Time.time - fadeStartTime) / (fadeTime/2);
			
			this.renderer.material.color = Color.Lerp(Color.black,col2,fracJourney);
			
			if(fracJourney >= 1){
				this.renderer.material.color = col2;
				fadeStartTime = Time.time;
				fadeState = 3;
			}
			break;
		case 3:
			resetTimer += Time.fixedDeltaTime;

			if(resetTimer >= myMaster.waitTime){
				fadeState = 0;
				resetTimer = 0;
			}
			break;
		} 
		*/
	}
}
