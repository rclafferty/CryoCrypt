using UnityEngine;
using System.Collections;

public class LabMaster : MonoBehaviour {
	GameObject mainCam;
	public Transform myCrypt;
	public float spawnRate;
	bool spawnAgain = true;

	bool advance;
	TextMesh text;
	public float textTime;
	int storyState = 0;

	public Vector3 startPos;
	public Vector3 endPos;
	float fracJourney;
	float panStart;
	public float panTime;


	// Use this for initialization
	void Start () {
		mainCam = GameObject.Find ("Main Camera");
		text = GameObject.Find ("Text").GetComponent<TextMesh> ();
		StartCoroutine ("Advance");
		panStart = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		fracJourney = (Time.time - panStart) / panTime;
		mainCam.transform.position = Vector3.Lerp (startPos, endPos, fracJourney);

		if (spawnAgain) {
			StartCoroutine("SpawnTimer");
			spawnAgain = false;
		}

		switch (storyState) {
		case 0:
			text.text = "The engineers at CryoCrypt\nperfected spacetime travel.";
			break;
		case 1:
			text.text = "Sentinels like me pack you up\nin a secure crypt and guide you\nthrough the tesseract\nto new worlds.";
			break;
		case 2:
			text.text = "But this trip is special.\n\nPappy is going and so am I.";
			break;
		case 3:
			text.text = "Goodbye Earth.";
			break;
		case 4:
			text.text = "";
			break;
		case 5:
			Application.LoadLevel("CodeSet");
			break;
		}
	}

	IEnumerator SpawnTimer () {
		Instantiate (myCrypt, this.transform.position, Quaternion.identity);
		yield return new WaitForSeconds (spawnRate);
		spawnAgain = true;
	}

	IEnumerator Advance () {
		yield return new WaitForSeconds(textTime);
		storyState ++;
		StartCoroutine ("Advance");
	}
}
