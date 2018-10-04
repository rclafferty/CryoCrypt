using UnityEngine;
using System.Collections;

public class MapCrypt : MonoBehaviour {
	Controller myC;
	public bool canSelect;
	bool hoveredOver;
	Vector3 baseScale;
	int myNum;

	// Use this for initialization
	void Start () {
		baseScale = transform.localScale;
		myC = GameObject.Find ("Controller").GetComponent<Controller> ();
		myNum = int.Parse (gameObject.name.Substring(9)) - 1;
		if (myC.cryptDone [myNum]) {
			canSelect = false;
			this.GetComponent<SpriteRenderer>().color = Color.black;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (canSelect) {
			if(hoveredOver){
				transform.localScale = new Vector3(baseScale.x + baseScale.x/5,baseScale.y + baseScale.y/5);
			} else {
				float xPP = Mathf.PingPong(Time.time,baseScale.x/5);
				float yPP = Mathf.PingPong(Time.time,baseScale.y/5);
				
				transform.localScale = baseScale + new Vector3(xPP,yPP,0.0f);
			}
		}

	}

	void OnMouseOver () {
		hoveredOver = true;
		if (Input.GetMouseButtonDown (0) && canSelect) {
			//Application.LoadLevel("Grid");
			myC.cryptName = gameObject.name;
			Movement spriteMove = GameObject.Find("player").GetComponent<Movement>();
			spriteMove.fadeStartTime = Time.time;
			spriteMove.start = GameObject.Find("player").transform.position;
			spriteMove.end = this.transform.position;
			spriteMove.lerping = true;
		}
	}

	void OnMouseExit () {
		hoveredOver = false;
	}
}
