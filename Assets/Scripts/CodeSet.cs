using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CodeSet : MonoBehaviour {
	Controller myC;
	int currentBlock;
	public GameObject[] paletteBlocks;
	public GameObject[] codeBlocks;
	//public GameObject[] keyGens;

	public Color[] mainKey;
	List<Color> mainKeyList = new List<Color>();
	Color[,] keys = new Color[5,9];
	GameObject[,] keyGens = new GameObject[5,9];

	Color[] colorPalette;
	GameObject highlighter;
	GameObject paletteHL;
	GameObject generateButton;
	GameObject launchButton;
	GameObject launching;
	//bool canGenerate;
	bool resetHL = true;

	public bool generating;
	public float timeBetweenGens;

	public AudioClip colorBeep;

	public bool canGenerate;

	public Color dummyColor;

	// Use this for initialization
	void Start () {
		myC = GameObject.Find ("Controller").GetComponent<Controller> ();
		colorPalette = myC.colorPalette;
		mainKey = new Color[9];

		highlighter = GameObject.Find ("Highlight");
		paletteHL = GameObject.Find ("PaletteHL");
		generateButton = GameObject.Find ("GenerateButton");
		launchButton = GameObject.Find ("ProceedButton");
		launching = GameObject.Find ("LaunchText");
		launching.SetActive (false);
		generateButton.SetActive (false);
		launchButton.SetActive (false);
		paletteHL.transform.position = paletteBlocks [0].transform.position;

		for(int i = 0; i < paletteBlocks.Length; i ++){
			paletteBlocks[i].GetComponent<Renderer>().material.color = colorPalette[i];
		}
		for(int i = 0; i < codeBlocks.Length; i ++){
			codeBlocks[i].GetComponent<Renderer>().material.color = dummyColor;
		}

		for (int i = 0; i < 5; i ++) {
			string myName = "Key" + i;
			GameObject myParent = GameObject.Find(myName);
			for(int j = 0; j < 9; j ++){
				string myNum = "" + j;
				keyGens[i,j] = myParent.transform.Find(myNum).gameObject;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!generating) {
			if (resetHL) {
				highlighter.transform.position = codeBlocks[currentBlock].transform.position;
				resetHL = false;		
			}

			if(Input.GetAxis("Mouse ScrollWheel") > .1f && !resetHL){
				currentBlock ++;
				if(currentBlock > 8){
					currentBlock = 0;
				}
				resetHL = true;
			}

			if(Input.GetAxis("Mouse ScrollWheel") < -.1f && !resetHL){
				currentBlock --;
				if(currentBlock < 0){
					currentBlock = 8;
				}
				resetHL = true;
			}

			for(int i = 0; i < paletteBlocks.Length; i ++){
				if(paletteBlocks[i].GetComponent<Region>().hovered){
					paletteHL.transform.position = paletteBlocks[i].transform.position;
				}
				
				if(paletteBlocks[i].GetComponent<Region>().clicked){
					GetComponent<AudioSource>().PlayOneShot(colorBeep);
					codeBlocks[currentBlock].GetComponent<Renderer>().material.color = paletteBlocks[i].GetComponent<Renderer>().material.color;
					mainKey[currentBlock] = paletteBlocks[i].GetComponent<Renderer>().material.color;
					currentBlock ++;
					if(currentBlock > 8){
						currentBlock = 0;
						//canGenerate = true;
						generateButton.SetActive(true);
					}
					resetHL = true;
					paletteBlocks[i].GetComponent<Region>().clicked = false;
				}
			}

			bool isFull = true;
			for(int i = 0; i < mainKey.Length; i ++){
				if(codeBlocks[i].GetComponent<Renderer>().material.color == dummyColor){
					isFull = false;
				}
			}
			canGenerate = isFull;
			
			if (generating) {
				StartCoroutine("GenerateKeys");
				generating = false;
			}
		}

		/*
		if (launchButton.GetComponent<Region> ().clicked) {
			Application.LoadLevel("complete");		
		}
		*/
	}

	public IEnumerator GenerateKeys () {
		yield return new WaitForSeconds(timeBetweenGens);
		for(int i = 0; i < 9; i ++){
			keyGens[0,i].GetComponent<Renderer>().material.color = mainKey[i];
			keys[0,i] = mainKey[i];
			myC.allKeys[0,i] = mainKey[i];
			yield return new WaitForSeconds(.1f);
		}
		yield return new WaitForSeconds(timeBetweenGens);
		for (int i = 0; i < mainKey.Length; i ++) {
			mainKeyList.Add(mainKey[i]);		
		}
		for (int i = 0; i < 9; i ++) {
			int randCol = Random.Range(0,mainKeyList.Count);
			keyGens[1,i].GetComponent<Renderer>().material.color = mainKeyList[randCol];
			keys[1,i] = mainKeyList[randCol];
			myC.allKeys[1,i] = mainKeyList[randCol];
			mainKeyList.Remove(mainKeyList[randCol]);
			yield return new WaitForSeconds(.1f);
		}
		yield return new WaitForSeconds(timeBetweenGens);
		for (int i = 0; i < mainKey.Length; i ++) {
			mainKeyList.Add(mainKey[i]);		
		}
		for (int i = 0; i < 9; i ++) {
			int randCol = Random.Range(0,mainKeyList.Count);
			keyGens[2,i].GetComponent<Renderer>().material.color = mainKeyList[randCol];
			keys[2,i] = mainKeyList[randCol];
			myC.allKeys[2,i] = mainKeyList[randCol];
			mainKeyList.Remove(mainKeyList[randCol]);
			yield return new WaitForSeconds(.1f);
		}
		yield return new WaitForSeconds(timeBetweenGens);
		for (int i = 0; i < mainKey.Length; i ++) {
			mainKeyList.Add(mainKey[i]);		
		}
		for (int i = 0; i < 9; i ++) {
			int randCol = Random.Range(0,mainKeyList.Count);
			keyGens[3,i].GetComponent<Renderer>().material.color = mainKeyList[randCol];
			keys[3,i] = mainKeyList[randCol];
			myC.allKeys[3,i] = mainKeyList[randCol];
			mainKeyList.Remove(mainKeyList[randCol]);
			yield return new WaitForSeconds(.1f);
		}
		yield return new WaitForSeconds(timeBetweenGens);
		for (int i = 0; i < mainKey.Length; i ++) {
			mainKeyList.Add(mainKey[i]);		
		}
		for (int i = 0; i < 9; i ++) {
			int randCol = Random.Range(0,mainKeyList.Count);
			keyGens[4,i].GetComponent<Renderer>().material.color = mainKeyList[randCol];
			keys[4,i] = mainKeyList[randCol];
			myC.allKeys[4,i] = mainKeyList[randCol];
			mainKeyList.Remove(mainKeyList[randCol]);
			yield return new WaitForSeconds(.1f);
		}
		yield return new WaitForSeconds(timeBetweenGens);
		launching.SetActive (true);
		yield return new WaitForSeconds (timeBetweenGens*4);
		Application.LoadLevel("complete");
		//launchButton.SetActive (true);
	}
}
