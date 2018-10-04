using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	public Color[] colorPalette;
	public Color[,] allKeys = new Color[5,9];
	public int cryptCount;
	public bool[] cryptDone;
	public string cryptName;

	void Awake () {
		DontDestroyOnLoad (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		cryptDone = new bool[5];
		cryptName = "ship";
		//theKey = new Color[9];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
