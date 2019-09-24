using UnityEngine;
using System.Collections;

public class Pauser : MonoBehaviour {

	[Header("Pauser also controls Cursor States!")]

	bool isPaused = false;

	public GameObject[] toBeDisabledG;
	public MonoBehaviour[] toBeDisabledS;

	public GameObject[] toBeEnabledG;
	public MonoBehaviour[] toBeEnabledS;

	// Use this for initialization
	void Start () {
		if (!Application.isEditor) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

		//Cursor.lockState = CursorLockMode.Locked;
		//Cursor.visible = false;
	}

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {

            if (!isPaused)
            {
                isPaused = true;
                Time.timeScale = 0;

                if (!Application.isEditor)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }

                foreach (GameObject obj in toBeDisabledG)
                    obj.SetActive(false);
                foreach (MonoBehaviour mono in toBeDisabledS)
                    mono.enabled = false;

                foreach (GameObject obj in toBeEnabledG)
                    obj.SetActive(true);
                foreach (MonoBehaviour mono in toBeEnabledS)
                    mono.enabled = true;



            }
            else
            {
                isPaused = false;
                Time.timeScale = 1;

                if (!Application.isEditor)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }


                foreach (GameObject obj in toBeDisabledG)
                    obj.SetActive(true);
                foreach (MonoBehaviour mono in toBeDisabledS)
                    mono.enabled = true;

                foreach (GameObject obj in toBeEnabledG)
                    obj.SetActive(false);
                foreach (MonoBehaviour mono in toBeEnabledS)
                    mono.enabled = false;
            }
        }
    }
}
