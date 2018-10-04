using UnityEngine;
using System.Collections;

public class TutorialDelete : MonoBehaviour {
	public float totalTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator DeathTime () {
		yield return new WaitForSeconds(totalTime);
		Destroy (this.gameObject);
	}
}
