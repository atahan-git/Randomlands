using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeathController : MonoBehaviour {

	public string[] deathMessages;
	public MonoBehaviour[] scriptsToDisable;

	public GameObject realCam;
	public GameObject gunCam;
    public GameObject UICam;
    public GameObject deathCam;
	public Transform deathCamPos;
    public Transform WinCamPos;


    public float lerp = 0.01f;
	public float timeSlowAmount = 0.1f;
    public float lerp2 = 0.01f;



    bool areWeDead = false;
    bool areWeWon = false;

    public GameObject deathHUD;
	public Text score;
	public Text HScore;
	public Text deathMessageText;

    public GameObject WinHUD;
    public Text scoreWin;
    public Text HScoreWin;

    // Use this for initialization
    void Start () {
		Time.timeScale = 1;
		Time.fixedDeltaTime = Time.timeScale * 0.02f;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (areWeDead) {
			deathCam.transform.position = Vector3.Lerp (deathCam.transform.position, deathCamPos.position, lerp * Time.deltaTime);
			deathCam.transform.LookAt (transform.position);
		}
        if (areWeWon)
        {
            deathCam.transform.position = Vector3.Lerp(WinCamPos.transform.position, WinCamPos.position, lerp2 * Time.deltaTime);
            deathCam.transform.LookAt(transform.position);
        }

    }

	void Die (){

		realCam.transform.DetachChildren ();
		gunCam.transform.DetachChildren ();
		realCam.SetActive (false);
		gunCam.SetActive (false);
        UICam.SetActive(false);
        deathCam.SetActive (true);

		areWeDead = true;

		Time.timeScale = timeSlowAmount;
		Time.fixedDeltaTime = Time.timeScale * 0.02f;

		score.text = "Score: " + ScoreController.myScore.score.ToString();
		HScore.text = "High Score: " + PlayerPrefs.GetInt ("HScore", 0).ToString();
		deathMessageText.text = deathMessages[Random.Range(0, deathMessages.Length-1)];

		foreach (MonoBehaviour myScript in scriptsToDisable) {
			myScript.enabled = false;
		}


		Invoke ("EnableHUD", 0.4f);
	}

    public void Win()
    {
        realCam.transform.DetachChildren();
        gunCam.transform.DetachChildren();
        realCam.SetActive(false);
        gunCam.SetActive(false);
        UICam.SetActive(false);
        deathCam.SetActive(true);

        areWeWon = true;

        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

        scoreWin.text = "Score: " + ScoreController.myScore.score.ToString();
        HScoreWin.text = "High Score: " + PlayerPrefs.GetInt("HScore", 0).ToString();

        foreach (MonoBehaviour myScript in scriptsToDisable)
        {
            myScript.enabled = false;
        }


        Invoke("EnableWinHUD", 3f);

    }

    void EnableHUD (){
		deathHUD.SetActive (true);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

    void EnableWinHUD()
    {
        WinHUD.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Restart (){
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
    }
    public void Menu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }
}
