using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour {
	Controller myC;
	GameObject mainCam;
	GridMaster myMaster;
	GameObject readout;
	TimeChange myTimer;

	SpriteRenderer faceSprite;

	public AudioClip success;
	public AudioClip failure;

	// Use this for initialization
	void Start () {
		mainCam = GameObject.Find ("Main Camera");
		readout = GameObject.Find ("Readout");
		myMaster = mainCam.GetComponent<GridMaster> ();
		myC = GameObject.Find ("Controller").GetComponent<Controller> ();
		myTimer = GameObject.Find ("Timer").GetComponent<TimeChange>();
		faceSprite = GameObject.Find ("CharacterFace").GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		SelectorMove ();
		CheckSelect ();
	}

	void SelectorMove(){
		Vector3 mousePos = mainCam.GetComponent<Camera>().ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 16.5f));
		int gridX = Mathf.RoundToInt (mousePos.x);
		int gridY = Mathf.RoundToInt (mousePos.y);
		if (gridX < 1) {
			gridX = 1;
		}
		if (gridX > 6) {
			gridX = 6;
		}
		if (gridY < 1) {
			gridY = 1;
		}
		if (gridY > 6) {
			gridY = 6;
		}
		transform.position = new Vector3 (gridX, gridY, -.6f);
	}

	void CheckSelect () {
		if (Input.GetMouseButtonDown (0)) {
			Vector3 mousePos = mainCam.GetComponent<Camera>().ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 16.5f));
			int gridX = Mathf.RoundToInt (mousePos.x);
			int gridY = Mathf.RoundToInt (mousePos.y);	

			//print ("" + gridX + ", " + gridY);

			if(gridX >= 1 && gridX <= 6 && gridY >= 1 && gridY <=6){
				if(myMaster.gridBlocks[gridX,gridY] == myMaster.keyBlock  && myMaster.submitRot == myMaster.solutionRot){
					readout.GetComponent<TextMesh>().text = "SUCCESS!";
					GetComponent<AudioSource>().PlayOneShot(success);
					readout.GetComponent<TextMesh>().color = Color.green;
					StartCoroutine("SuccessTimer");
				} else {
					readout.GetComponent<TextMesh>().text = "FAILURE.";
					GetComponent<AudioSource>().PlayOneShot(failure);
					if(myC.cryptCount > 0){
						if(myTimer.time > 10.0f){
							myTimer.time -= 10.0f;
							StartCoroutine("FailureBlink");
						} else {
							myTimer.time = 0.0f;
							myTimer.tm.text = "0:00:00";
							StartCoroutine("FailureTimer");
						}
					}
					readout.GetComponent<TextMesh>().color = Color.red;
				}
			}
		}
	}

	IEnumerator SuccessTimer () {
		int cryptNum = 0;
		if (myC.cryptCount > 0) {
			cryptNum = int.Parse (myC.cryptName.Substring(9)) - 1;
			myC.cryptDone[cryptNum] = true;
		}
		faceSprite.sprite = myMaster.endFaces [cryptNum];
		yield return new WaitForSeconds(3.0f);
		myC.cryptCount ++;

		if (myC.cryptCount >= 5) {
			myTimer.startTimer = false;
			myTimer.tm.text = "";
		}

		switch (cryptNum) {
		case 0:
			Application.LoadLevel("gaptwofour");
			break;
		case 1:
			Application.LoadLevel("successkat");
			break;
		case 2:
			Application.LoadLevel("successdad");
			break;
		case 3:
			Application.LoadLevel("successchild");
			break;
		case 4:
			Application.LoadLevel("successphysicist");
			break;
		default:
			Application.LoadLevel ("Map");
			break;
		}
	}

	IEnumerator FailureTimer () {
		//Have some stuff happen because of your horrible-horrible-terrible, failure.
		yield return new WaitForSeconds(3.0f);
		Application.LoadLevel ("risk");
	}

	IEnumerator FailureBlink () {
		Color initialColor = myTimer.gameObject.GetComponent<TextMesh> ().color;
		myTimer.gameObject.GetComponent<TextMesh>().color = Color.red;
		yield return new WaitForSeconds(.5f);
		myTimer.gameObject.GetComponent<TextMesh> ().color = initialColor;
	}
}
