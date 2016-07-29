using UnityEngine;
using System.Collections;

public class Checker_Debug : MonoBehaviour, IValue {

	public float curValue {
		get{
			return leValue;
		}
		set{
			curValue = value;
		}
	}

	public float leValue;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//curValue = leValue;
	
	}
}
