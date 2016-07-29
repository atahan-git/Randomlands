using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

    public AudioSource[] musicSources;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        AudioListener.volume = PlayerPrefs.GetFloat("SoundVolume", 1);

        foreach(AudioSource aud in musicSources)
        {
            aud.volume = PlayerPrefs.GetFloat("MusicVolume", 1);
        }
    }
}
