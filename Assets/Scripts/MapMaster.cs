using UnityEngine;
using System.Collections;

public class MapMaster : MonoBehaviour {
	GameObject player;
	Controller myC;
	public GameObject[] allCrypts;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("player");
		myC = GameObject.Find ("Controller").GetComponent<Controller> ();
		GameObject currentCrypt = GameObject.Find(myC.cryptName);
		player.transform.position = new Vector3(currentCrypt.transform.position.x,currentCrypt.transform.position.y,player.transform.position.z);

		for (int i = 0; i < allCrypts.Length; i++) {
			if(myC.cryptDone[i]){
				allCrypts[i].GetComponent<MapCrypt>().canSelect = false;
			}
		}

		if (myC.cryptCount == 0) {
			allCrypts [0].GetComponent<MapCrypt> ().canSelect = true;
			allCrypts [0].GetComponent<SpriteRenderer> ().color = Color.white;
		} else {
			
			for(int i = 1; i < allCrypts.Length; i++){
				if(!myC.cryptDone[i]){
					allCrypts[i].GetComponent<MapCrypt>().canSelect = true;
					allCrypts[i].GetComponent<SpriteRenderer>().color = Color.white;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
