using UnityEngine;
using System.Collections;

public class Animate : MonoBehaviour {
	public Sprite[] myAnimation;
	public float timeBFrames;
	bool restartAnim = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (restartAnim) {
			StartCoroutine("AnimateMe");
			restartAnim = false;		
		}
	}

	IEnumerator AnimateMe () {
		for (int i = 0; i < myAnimation.Length; i++){
			this.GetComponent<SpriteRenderer>().sprite = myAnimation[i];
			yield return new WaitForSeconds(timeBFrames);
		}
		restartAnim = true;
	}
}
