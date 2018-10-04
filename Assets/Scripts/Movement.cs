using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float fadeStartTime;
	public Vector3 start;
	public Vector3 end;
	private float fracJourney;
	private float fadeTime;
	public bool lerping;

	// Use this for initialization
	void Start () {
		//Vector3 start = this.transform.position;
		//end = <end position>
		lerping = false;
		fadeStartTime = Time.time;
		//float fadeTime = <some arbitrary value>
		fadeTime = (float)2.0;
	}
	
	// Update is called once per frame
	void Update () {
		if (lerping)
		{
			//float fracJourney = (Time.time - fadeStartTime) / (fadeTime);
			fracJourney = (Time.time - fadeStartTime) / (fadeTime);
			transform.position = Vector3.Lerp (start, end, fracJourney);
			if (fracJourney >= 1)
			{
				Application.LoadLevel("Grid");
			}

			//Vector3 position = pos(t);
			//Set sprite to position
			//if(player at destination || player past location)
			//{
			//	set player to location
			//	transition to puzzle scene
			//}*/
		}
	}

	/*void pos(float t)
	{
		float x = start.x + (t/6)(end.x - start.x);	//6 is 6 seconds to travel to destination
		float y = start.y + (t/6)(end.y - start.y);

		//return (x, y); 	//next refresh location --> Vector3 variable?
	}*/

}