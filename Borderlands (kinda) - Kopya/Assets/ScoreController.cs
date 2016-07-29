using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

	public int score = 0;

	public Text ScoreText;
	public Text HighScoreText;


	static public ScoreController myScore;
	// Use this for initialization
	void Start () {

		HighScoreText.text = "High Score: " + PlayerPrefs.GetInt ("HScore", 0).ToString();
		if (!myScore)
			myScore = this;
	
		UpdateScore ();
	}
	

	void UpdateScore () {

		ScoreText.text = "Score: " + score;
	
	}

	void Die (){

		if (score > PlayerPrefs.GetInt ("HScore", 0)) {
			PlayerPrefs.SetInt("HScore", score);
		}

	}

	public void AddScore (int toAdd){
		if(toAdd > 0)
			ScoreText.text = "Score: " + score + " +" + toAdd;
		else
			ScoreText.text = "Score: " + score + " " + toAdd;
		score += toAdd;
		Invoke ("UpdateScore", 0.5f);

	}
}
