using UnityEngine;
using System.Collections;

public class SceneChange : MonoBehaviour {
	public string nextLevel;
	public float sceneTime;

	// Use this for initialization
	void Start () {
		StartCoroutine ("SceneTimer");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown(0)) {
			if (Application.loadedLevelName == "successphysicist" || Application.loadedLevelName == "successdad" || Application.loadedLevelName == "successchild" || Application.loadedLevelName == "successkat") {
				Controller myC = GameObject.Find ("Controller").GetComponent<Controller> ();	
				if (myC.cryptCount >= 5) {
					Application.LoadLevel ("missionaccomplished");
				} else {
					Application.LoadLevel (nextLevel);
				}
			} else {
				Application.LoadLevel (nextLevel);		
			}
		}
	}

	IEnumerator SceneTimer () {
		yield return new WaitForSeconds (sceneTime);
		if (Application.loadedLevelName == "successphysicist" || Application.loadedLevelName == "successdad" || Application.loadedLevelName == "successchild" || Application.loadedLevelName == "successkat") {
			Controller myC = GameObject.Find ("Controller").GetComponent<Controller> ();	
			if (myC.cryptCount >= 5) {
				Application.LoadLevel ("missionaccomplished");
			} else {
				Application.LoadLevel (nextLevel);
			}
		} else {
			Application.LoadLevel (nextLevel);		
		}
	}
}
