using UnityEngine;
using System.Collections;

public class Difficulty : MonoBehaviour {

	public int diff;
	public CodeSet cs;
	//public TextMesh[] t;
	public TextMesh tm;
	public TextMesh tmIntro;
	public Controller c;

	// Use this for initialization
	void Start () {
		diff = 0;
		//0 = not assigned
		//1 = Very Easy
		//2 = Easy
		//3 = Medium
		//4 = Hard
		//5 = Very Hard
		//t = new TextMesh[6];
		//for (int i = 0; i < 6; i++)
		//	t[i] = GameObject.Find("NumSame" + (i + 1)).GetComponent<TextMesh>();
		tm = GameObject.Find ("diffText").GetComponent<TextMesh> ();
		tmIntro = GameObject.Find("diffTextIntro").GetComponent<TextMesh>();
		cs = this.GetComponent<CodeSet> ();
		c = GameObject.Find ("Controller").GetComponent<Controller> ();
	}
	
	// Update is called once per frame
	void Update () {
		//cs.mainKey[0]
		int numSame = 0;
		/*for (int i = 0; i < 9; i++)
		{
			for (int j = i + 1; j < 9; j++)
			{
				if (cs.mainKey[i] == cs.mainKey[j])
					numSame++;
			}
		}*/

		int[] colors = new int[6];

		for (int i = 0; i < 9; i++)
		{
			for (int j = 0; j < 6; j++)
			{
				if (cs.mainKey[i] == c.colorPalette[j])
				{
					colors[j]++;
					break;
				}
			}
		}

		/*if (cs.mainKey[0] == cs.mainKey[1] == cs.mainKey[2] == cs.mainKey[3] == cs.mainKey[4]
		    == cs.mainKey[5] == cs.mainKey[6] == cs.mainKey[7] == cs.mainKey[8])
			numSame = 9;
		else if (cs.mainKey[0] == cs.mainKey[1] == cs.mainKey[2] == cs.mainKey[3] == cs.mainKey[4]
		         == cs.mainKey[5] == cs.mainKey[6] == cs.mainKey[7])
			numSame = 8;
		else if (cs.mainKey[1] == cs.mainKey[2] == cs.mainKey[3] == cs.mainKey[4] == cs.mainKey[5]
		         == cs.mainKey[6] == cs.mainKey[7] == cs.mainKey[8])
			numSame = 8;*/

		//for (int i = 0; i < 6; i++)
		//	t [i].text = colors [i].ToString ();

		/*************************
		 * Very Easy (1)
		 * --> 5+ of any color 
		 * Easy (2)
		 * --> 3+ of any 1 color
		 * --> multiples touching each other
		 * --> 3+ of one color, 2+ of another, all touching
		 * Medium (3)
		 * --> If any two colors are touching, it is Medium or Easy
		 * Hard (4)
		 * --> 1+ of every color
		 * --> no color can have 3+
		 * Very Hard (5)
		 * --> Same as hard and...
		 * --> ABSOLUTELY no colors touching
		 * ************************/

		//if easy --> check for very easy
		//else if hard --> check for very hard
		//else --> medium

		//check for hard and vHard
		bool hard = false;
		bool noTouch = true;
		bool vHard = false;

		for (int i = 0; i < 6; i++)
		{
			if (colors[i] < 3 && colors[i] > 0)
				hard = true;
			else
			{
				hard = false;
				break;
			}
		}

		//if (hard)
		//{
			//vertical
			for (int i = 0; i < 6; i++)
			{
				if (cs.mainKey[i] == cs.mainKey[i + 3])
				{
					noTouch = false;
					break;
				}
			}

			//horizontal
			if (noTouch)
			{
				for (int i = 0; i < 8; i++)
				{
					if (cs.mainKey[i] == cs.mainKey[i + 1])
					{
						if(i != 2 && i != 5){
							noTouch = false;
							break;
						}
					}
					/*
					if (i == 1 || i == 4)
						i++;
						*/
				}
			}

			if (noTouch && hard)
			{
				vHard = true;
				hard = true;
			}
			else
				vHard = false;

		//}


		/*************************
		 * Very Easy (1)
		 * --> 5+ of any color 
		 * Easy (2)
		 * --> 3+ of any 1 color
		 * --> multiples touching each other
		 * --> 3+ of one color, 2+ of another, all touching
		 * Medium (3)
		 * --> If any two colors are touching, it is Medium or Easy
		 * Hard (4)
		 * --> 1+ of every color
		 * --> no color can have 3+
		 * Very Hard (5)
		 * --> Same as hard and...
		 * --> ABSOLUTELY no colors touching
		 * ************************/

		//check for easy
		bool easy = false;
		bool vEasy = false;

		int twoCount = 0;
		int threeCount = 0;

		if (!( hard || vHard))
		{
			for (int i = 0; i < 6; i++)
			{
				if (colors[i] > 2)
				{	
					threeCount++;
				
					if (colors[i] > 4){
						vEasy = true;
						break;
					}
				} else if(colors[i] > 1){
					twoCount++;
				}
				//print(twoCount);
			}
			if(threeCount > 0)
			{
				if (threeCount > 1 || twoCount > 1){
					easy = true;
				}
			}
		}

		//check for medium
		bool med = false;

		if (!(easy || vEasy || hard || vHard))
			med = true;

		if(cs.canGenerate){
			tmIntro.text = "This code will be";
			if (vEasy)
				tm.text = "Very Easy";
			else if (easy)
				tm.text = "Easy";
			else if (vHard)
				tm.text = "Very Hard";
			else if (hard)
				tm.text = "Hard";
			else
				tm.text = "Medium";
		}

	}
}
