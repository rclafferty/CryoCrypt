using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {
	public AudioClip music1;
	public AudioClip music2;
	bool changedMusic;

	void Awake () {
		DontDestroyOnLoad (this.gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevelName == "CryoSleep" && !changedMusic) {
			GetComponent<AudioSource>().clip = music2;
			GetComponent<AudioSource>().Play();
			changedMusic = true;
		}

		if (Application.loadedLevelName != "CryoSleep" && changedMusic) {
			GetComponent<AudioSource>().clip = music1;
			changedMusic = false;
			GetComponent<AudioSource>().Play();
		}
	}
}
