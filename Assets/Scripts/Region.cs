using UnityEngine;
using System.Collections;

public class Region : MonoBehaviour {
	public bool hovered;
	public bool clicked;

	void OnMouseOver () {
		hovered = true;
		if (Input.GetMouseButtonDown (0)) {
			clicked = true;		
		}
	}

	void OnMouseExit () {
		hovered = false;
	}
}
