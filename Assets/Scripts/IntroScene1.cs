using UnityEngine;
using System.Collections;

public class IntroScene1 : MonoBehaviour {
	GameObject myEarth;
	public TextMesh text;
	bool lerping;
	float fracJourney;
	float startTime;
	public float lerpTime;

	public float advanceTime;
	int state;

	public float rotSpeed;
	bool rotating;
	GameObject myBurst;
	GameObject burstOb;
	GameObject fadeBlack;

	public float growTime;
	public float scale1;
	public float scale2;
	float growJourney;
	float growStart;
	bool growing;

	bool setOnce;

	///public AudioClip[] vos;
	//public bool[] vosSet;

	// Use this for initialization
	void Start () {
		//vosSet = new bool[vos.Length];
		lerping = true;
		startTime = Time.time;
		myEarth = GameObject.Find ("Earth");
		text = GameObject.Find ("Text").GetComponent<TextMesh> ();
		burstOb = GameObject.Find ("BurstOb");
		myBurst = burstOb.transform.Find ("Burst").gameObject;
		fadeBlack = GameObject.Find ("BlackBG");
		burstOb.transform.localScale = new Vector3 (scale1, scale1, scale1);
		StartCoroutine ("Advance");
	}
	
	// Update is called once per frame
	void Update () {
		if (lerping) {
			fracJourney = (Time.time - startTime) / lerpTime;

			float alpha = Mathf.Lerp(1.0f,0.0f,fracJourney);
			myEarth.GetComponent<SpriteRenderer>().color = new Color(1.0f,1.0f,1.0f,alpha);

			if(fracJourney >= 1){
				alpha = 0.0f;
				lerping = false;
			}
		}

		switch (state) {
		case 0:
			text.text = "...We had to leave earth.";
			break;
		case 1:
			text.text = "Resources were scarce.\nPeople were scared.";
			break;
		case 2:
			text.text = "Then came a solution...";
			break;
		case 3:
			text.text = "";
			if(!setOnce){
				rotating = true;
				growing = true;
				growStart = Time.time;
				setOnce = true;
			}
			break;
		case 4:
			Application.LoadLevel("secure");
			break;
		}

		if (growing) {
			print("yay");
			growJourney = (Time.time - growStart) / growTime;

			float fScale = Mathf.Lerp(scale1,scale2,growJourney);
			float alpha = Mathf.Lerp(0.0f,1.0f,growJourney);

			burstOb.transform.localScale = new Vector3(fScale,fScale,fScale);
			fadeBlack.GetComponent<SpriteRenderer>().color = new Color(0.0f,0.0f,0.0f,alpha);

			if(growJourney >= 1){
				burstOb.transform.localScale = new Vector3(scale2,scale2,scale2);
				fadeBlack.GetComponent<SpriteRenderer>().color = new Color(0.0f,0.0f,0.0f,1.0f);
				growing = false;
			}
		}

		if (rotating) {
			float zRot = myBurst.transform.rotation.eulerAngles.z + rotSpeed*Time.fixedDeltaTime;

			myBurst.transform.rotation = Quaternion.Euler(0,0,zRot);
		}
	}

	IEnumerator Advance () {
		yield return new WaitForSeconds(advanceTime);
		state ++;
		StartCoroutine ("Advance");
	}
}
