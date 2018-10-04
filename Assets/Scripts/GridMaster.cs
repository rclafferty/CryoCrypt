using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridMaster : MonoBehaviour {
	public Color[] colorPalette;
	public float waitTime;
	public float fadeTime;
	public GameObject[,] gridBlocks = new GameObject[8,8];
	GameObject theGrid;
	GameObject theKeyG;
	GameObject[] xRow;
	GameObject readout;
	Controller myC;
	TimeChange myTimer;
	
	bool fadeOut;
	bool fadeIn;
	int fadeState;
	Color[,] col1 = new Color[8,8];
	Color[,] col2 = new Color[8,8];
	float alpha;
	float fadeStartTime;
	float fracJourney;
	float resetTimer;

	public float rotTime;
	float rotJourney;
	float rotStartTime;
	float startRot;
	float endRot;
	bool rotating;
	public int submitRot;
	public int solutionRot;

	public Color[] theKey;
	public GameObject keyBlock;
	public GameObject[] keyBlocks;

	public Sprite[] faces;
	public Sprite[] endFaces;
	SpriteRenderer faceSprite;

	int cryptNum;

	public AudioClip rotateSound;

	public float scaler;

	// Use this for initialization
	void Start () {
		//Get all blocks
		for (int i = 0; i < 8; i ++) {
			for(int j = 0; j < 8; j ++){
				string nameToFind = "codeBlock" + i + j;
				gridBlocks[i,j] = GameObject.Find(nameToFind);
				col2[i,j] = Color.black;
			}
		}
		theGrid = GameObject.Find ("TheGrid");
		theKeyG = GameObject.Find ("TheKey");
		readout = GameObject.Find ("Readout");
		readout.GetComponent<TextMesh> ().text = "";
		myC = GameObject.Find ("Controller").GetComponent<Controller> ();
		myTimer = GameObject.Find ("Timer").GetComponent<TimeChange>();

		if (myC.cryptCount > 0) {
			cryptNum = int.Parse (myC.cryptName.Substring(9)) - 1;
		}

		faceSprite = GameObject.Find ("CharacterFace").GetComponent<SpriteRenderer>();
		faceSprite.sprite = faces [cryptNum];

		for (int i = 0; i < 9; i++) {
			//print(myC.allKeys[2,i]);
			theKey[i] = myC.allKeys [myC.cryptCount,i];
		}
		for (int i = 0; i < 9; i++) {
			keyBlocks[i].GetComponent<Renderer>().material.color = theKey[i];		
		}
		for (int i = 0; i < 6; i++) {
			colorPalette[i] = myC.colorPalette[i];		
		}
		/*
		for (int i = 0; i < allCodeBlocks.Length; i ++) {
			allCodeBlocks[i].name = "block_" + i;		
		}
		*/
		//Make color arrays that are the same length as the block array.
	}
	
	// Update is called once per frame
	void Update () {
		CheckRot ();

		switch (fadeState) {
		case 0:
			//Right before fading out.
			for (int x = 0; x < 8; x++) {
				for(int y = 0; y < 8; y++){
					col1[x,y] = col2[x,y];
				}
			}
			//Set random colors for all blocks
			for (int x = 0; x < 8; x++) {
				for(int y = 0; y < 8; y++){
					col2[x,y] = colorPalette[Random.Range(0,6)];
				}
			};
			//Set the key
			SetKeyColors();

			fadeStartTime = Time.time;
			fadeState = 1;
			break;
		case 1:
			//Fade out
			fracJourney = (Time.time - fadeStartTime) / (fadeTime/2);


			for(int i = 0; i < 8; i++){
				for(int j = 0; j < 8; j++){
					gridBlocks[i,j].GetComponent<Renderer>().material.color = Color.Lerp(col1[i,j],Color.black,fracJourney);
					//gridBlocks[i,j].renderer.material.color = Color.black;
				}
			}
			
			if(fracJourney >= 1){
				fadeStartTime = Time.time;
				readout.GetComponent<TextMesh>().text = "";
				fadeState = 2;
			}
			break;
		case 2:
			//Fade in
			fracJourney = (Time.time - fadeStartTime) / (fadeTime/2);

			for(int x = 0; x < 8; x++){
				for(int y = 0; y < 8; y++){
					gridBlocks[x,y].GetComponent<Renderer>().material.color = Color.Lerp(Color.black,col2[x,y],fracJourney);
					//gridBlocks[x,y].renderer.material.color = Color.black;
				}
			}
			
			if(fracJourney >= 1){
				fadeStartTime = Time.time;
				fadeState = 3;
			}
			break;
		case 3:
			//Wait until the rest timer goes.
			resetTimer += Time.fixedDeltaTime;
			
			if(resetTimer >= waitTime){
				fadeState = 0;
				resetTimer = 0;
			}
			break;
		} 

		waitTime = 3 + (myTimer.time / myTimer.baseTime)*scaler;
	}

	void SetKeyColors () {
		int randX = Random.Range(1,6);
		int randY = Random.Range(1,6);
		int randRot = Random.Range(0,4);
		solutionRot = randRot;
		//int randRot = 0;
		col2[randX,randY] = theKey[4];
		keyBlock = gridBlocks[randX,randY];
		switch(randRot){
		case 0:
			col2[randX - 1,randY + 1] = theKey[0];
			col2[randX,randY + 1] = theKey[1];
			col2[randX + 1,randY + 1] = theKey[2];
			col2[randX - 1,randY] = theKey[3];
			col2[randX + 1,randY] = theKey[5];
			col2[randX - 1,randY - 1] = theKey[6];
			col2[randX, randY - 1] = theKey[7];
			col2[randX + 1,randY - 1] = theKey[8];
			break;
		case 1:
			col2[randX - 1,randY + 1] = theKey[2];
			col2[randX,randY + 1] = theKey[5];
			col2[randX + 1,randY + 1] = theKey[8];
			col2[randX - 1,randY] = theKey[1];
			col2[randX + 1,randY] = theKey[7];
			col2[randX - 1,randY - 1] = theKey[0];
			col2[randX, randY - 1] = theKey[3];
			col2[randX + 1,randY - 1] = theKey[6];
			break;
		case 2:
			col2[randX - 1,randY + 1] = theKey[8];
			col2[randX,randY + 1] = theKey[7];
			col2[randX + 1,randY + 1] = theKey[6];
			col2[randX - 1,randY] = theKey[5];
			col2[randX + 1,randY] = theKey[3];
			col2[randX - 1,randY - 1] = theKey[2];
			col2[randX, randY - 1] = theKey[1];
			col2[randX + 1,randY - 1] = theKey[0];
			break;
		case 3:
			col2[randX - 1,randY + 1] = theKey[6];
			col2[randX,randY + 1] = theKey[3];
			col2[randX + 1,randY + 1] = theKey[0];
			col2[randX - 1,randY] = theKey[7];
			col2[randX + 1,randY] = theKey[1];
			col2[randX - 1,randY - 1] = theKey[8];
			col2[randX, randY - 1] = theKey[5];
			col2[randX + 1,randY - 1] = theKey[2];
			break;
		}
	}

	void CheckRot () {
		if (Input.GetMouseButtonDown (1) && !rotating) {
			GetComponent<AudioSource>().PlayOneShot(rotateSound);
			rotating = true;
			rotStartTime = Time.time;
			startRot = theKeyG.transform.rotation.eulerAngles.z;
			endRot = startRot - 90;
		}

		if (rotating) {
			rotJourney = (Time.time - rotStartTime) / rotTime;

			float myZ = Mathf.Lerp(startRot,endRot,rotJourney);
			theKeyG.transform.rotation = Quaternion.Euler(0,0,myZ);

			if(rotJourney >= 1){
				rotating = false;
				theKeyG.transform.rotation = Quaternion.Euler(0,0,endRot);
			}
		}

		float calcRot = endRot;
		if (calcRot < 0) {
			calcRot += 360.0f;		
		}
		submitRot = Mathf.RoundToInt((calcRot / 90.0f)) % 4;

	}
}
