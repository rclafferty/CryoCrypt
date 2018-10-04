using UnityEngine;
using System.Collections;

public class TextTimeline : MonoBehaviour {
	TextMesh myText;
	public float textTime;
	bool moveOn;

	// Use this for initialization
	void Start () {
		myText = GameObject.Find ("Talking").GetComponent<TextMesh> ();
		StartCoroutine ("TextTimer");
		myText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		if (moveOn) {
			Application.LoadLevel("Map");		
		}
	}

	IEnumerator TextTimer () {
		yield return new WaitForSeconds (1.0f);
		myText.GetComponent<TextMesh> ().text = "Some stuff is going on!";
		yield return new WaitForSeconds(textTime);
		myText.GetComponent<TextMesh> ().text = "Solve the puzzles and stuff!";
		yield return new WaitForSeconds (textTime);
		//Do code B
		moveOn = true;
		//yield return 0;
	}
}
