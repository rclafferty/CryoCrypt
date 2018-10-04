using UnityEngine;
using System.Collections;

public class Play : MonoBehaviour {
	Color baseColor;

	// Use this for initialization
	void Start () {
		baseColor = this.GetComponent<TextMesh> ().color;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseOver () {
		this.GetComponent<TextMesh> ().color = Color.white;
		if (Input.GetMouseButtonDown (0)) {
			Application.LoadLevel("stasis");		
		}
	}

	void OnMouseExit () {
		this.GetComponent<TextMesh> ().color = baseColor;
	}
}
