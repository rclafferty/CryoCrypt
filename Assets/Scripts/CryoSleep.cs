using UnityEngine;
using System.Collections;

public class CryoSleep : MonoBehaviour {
	public float cryoTime;

	// Use this for initialization
	void Start () {
		StartCoroutine ("Timer");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Timer () {
		yield return new WaitForSeconds(cryoTime);
		Application.LoadLevel ("Map");
	}
}
