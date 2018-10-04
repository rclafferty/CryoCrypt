using UnityEngine;
using System.Collections;

public class TimeChange : MonoBehaviour {

	public TextMesh tm;
	//public Time t;
	public float time;
	public bool startTimer;
	public float baseTime;

	// Use this for initialization
	void Start ()
	{
		baseTime = time;
		tm = this.GetComponent<TextMesh> ();
		//time = (float)10.00;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Application.loadedLevelName == "CodeSet") {
			tm.text = "";
		} else	if (Application.loadedLevelName == "Grid") {
			transform.position = new Vector3 (-11.3f, 7.2f, -.3f);
			transform.localScale = new Vector3 (.08f, .08f, .08f);
			tm.color = new Color (.447f, .596f, .655f, 1.0f);
		} else {
			if (Application.loadedLevelName == "gpathree") {
				startTimer = true;	
			}
			transform.position = new Vector3 (-1.0f, -3.0f, -.25f);
			transform.localScale = new Vector3 (.07f, .07f, .07f);
			tm.color = new Color (.447f, .596f, .655f, 1.0f);
		}
		if (Application.loadedLevelName == "risk") {
			tm.text = "";		
		}
		
		if (startTimer) {
			if (time > 0) {
				float dt = Time.deltaTime;
				time -= dt;
				if (time < 0) {
					time = 0;
				}
				int mins = (int)(time / 60);
				float sec = time % 60;
				tm.text = "" + mins.ToString () + ":" + string.Format ("{0:00.00}", sec);
				if (time < 5) {
					tm.color += new Color (0, 1f * Time.fixedDeltaTime, 1f * Time.fixedDeltaTime);
				}
				if ((time < 5.1 && time > 4.9) || (time < 4.1 && time > 3.9) || (time < 3.1 && time > 2.9) || (time < 2.1 && time > 1.9) || (time < 1.1 && time > 0.9)) {
					tm.color = new Color (1, 0, 0);
				}

			} else {
				startTimer = false;
				tm.text = "0:00:00";
				tm.color = Color.red;
				StartCoroutine("FailureTimer");
				//tm.color = Color.red;
			}
		}
	}

	IEnumerator FailureTimer () {
		//Have some stuff happen because of your horrible-horrible-terrible, failure.
		yield return new WaitForSeconds(2.0f);
		Application.LoadLevel ("risk");
	}
}
