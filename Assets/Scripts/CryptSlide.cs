using UnityEngine;
using System.Collections;

public class CryptSlide : MonoBehaviour {
	public float slideSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3 (-slideSpeed * Time.fixedDeltaTime, 0.0f, 0.0f));
	}
}
