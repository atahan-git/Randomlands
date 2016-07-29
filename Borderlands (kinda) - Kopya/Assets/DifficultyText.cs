using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DifficultyText : MonoBehaviour {

	Text myText;

	// Use this for initialization
	void Start () {
		myText = GetComponent<Text> ();

		switch (PlayerPrefs.GetInt ("Diff")) {
		case 0:
			myText.text = "Difficulty: Easy";
			break;
		case 1:
			myText.text = "Difficulty: Normal/Kind";
			break;
		case 2:
			myText.text = "Difficulty: Normal";
			break;
		case 3:
			myText.text = "Difficulty: Normal/Harsh";
			break;
		case 4:
			myText.text = "Difficulty: Hardcore";
			break;
		default:
			myText.text = "Difficulty not set";
			break;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
