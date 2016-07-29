using UnityEngine;
using System.Collections;

public class MouseSensitivityChanger : MonoBehaviour {

    UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpc;

	// Use this for initialization
	void Start () {
        fpc = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
	}
	
	// Update is called once per frame
	void Update () {
        fpc.ChangeMouseSensitivity(PlayerPrefs.GetFloat("MouseSensitivity", 2), PlayerPrefs.GetFloat("MouseSensitivity", 2));
	}
}
