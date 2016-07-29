using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevelAtStart : MonoBehaviour {

	int levelid = 1;

	// Use this for initialization
	void Start () {
        Invoke("LoadNow", 6.5f);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LoadNow()
    {
        SceneManager.LoadSceneAsync(levelid);
    }
}
