using UnityEngine;
using System.Collections;

public class CodeSetSequence : MonoBehaviour {
	CodeSet cs;
	Difficulty myDiff;
	public GameObject[] arrows;
	public float[] yPositions;
	bool lerping;
	float fracJourney;
	float startTime;
	public float lerpTime;
	Vector3 startPos;
	Vector3 endPos;
	int sequenceState = 0;
	
	public Color arrowColor1;
	public Color arrowColor2;

	// Use this for initialization
	void Start () {
		cs = this.GetComponent<CodeSet> ();
		myDiff = this.GetComponent<Difficulty>();
		//myDiff.enabled = false;
		GameObject.Find("diffText").GetComponent<TextMesh>().text = "";
		GameObject.Find("diffTextIntro").GetComponent<TextMesh>().text = "";
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < arrows.Length; i ++) {
			if(arrows[i].GetComponent<Region>().hovered){
				if(i != 2){
					arrows[i].GetComponent<TextMesh>().color = arrowColor2;
				} else {
					if (cs.canGenerate) {
						arrows[i].GetComponent<TextMesh>().color = arrowColor2;
					} else {
						arrows[i].GetComponent<TextMesh>().color = arrowColor1;
					}
				}
			} else {
				arrows[i].GetComponent<TextMesh>().color = arrowColor1;
			}
		}

		switch (sequenceState) {
		case 0:
			if(arrows[0].GetComponent<Region>().clicked && !lerping){
				startPos = new Vector3(transform.position.x,yPositions[0],transform.position.z);
				endPos = new Vector3(transform.position.x,yPositions[1],transform.position.z);
				startTime = Time.time;
				//print ("hey");
				lerping = true;
			}
			break;
		case 1:
			if(arrows[1].GetComponent<Region>().clicked && !lerping){
				startPos = new Vector3(transform.position.x,yPositions[1],transform.position.z);
				endPos = new Vector3(transform.position.x,yPositions[2],transform.position.z);
				startTime = Time.time;
				//print ("hey");
				lerping = true;
			}
			break;
		case 2:
			if(arrows[2].GetComponent<Region>().clicked && !lerping && cs.canGenerate){
				startPos = new Vector3(transform.position.x,yPositions[2],transform.position.z);
				endPos = new Vector3(transform.position.x,yPositions[3],transform.position.z);
				startTime = Time.time;
				//print ("hey");
				lerping = true;
			}
			break;
		}

		if (lerping) {
			fracJourney = (Time.time - startTime) / lerpTime;

			transform.position = Vector3.Lerp(startPos,endPos,fracJourney);

			if(fracJourney >= 1){
				//print("whooaa");
				transform.position = endPos;
				sequenceState ++;
				if(sequenceState == 3){
					cs.StartCoroutine("GenerateKeys");
				}
				lerping = false;
			}
		}
	}

	/*
	IEnumerator WaitTimer (float waitTime) {
		yield return new WaitForSeconds (waitTime);

	}
	*/
}
