using UnityEngine;
using System.Collections;

public class AI_FollowScript : MonoBehaviour {

	GameObject player;       
	NavMeshAgent nav; 
	float distance;
	public float visionDist = 400;
	public float stoppingDistance = 0;
	[Range (0.5f, 20)]
	public float maxSpeed = 10;
	[Range (1f, 20)]
	public float acceleration = 5;

	//float speedMultiplier = 1f;

	// Use this for initialization
	void Start () {



		player = GameObject.FindGameObjectWithTag ("Player");
		nav = GetComponent<NavMeshAgent> ();

		float speedMultiplier = GameSpeedChanger.monsterSpeedMult;
		nav.speed = maxSpeed * speedMultiplier;
		nav.acceleration = acceleration * speedMultiplier;

		nav.stoppingDistance = stoppingDistance;
	
	}
	
	// Update is called once per frame
	void Update () {

		if(player == null)
			player = GameObject.FindGameObjectWithTag("Player");
		if (player == null)
			return; //player does not exist

		distance = Vector3.Distance(transform.position, player.transform.position);

		if(distance > visionDist){

			nav.enabled = false;
		}else{
			nav.enabled = true;
			nav.SetDestination (player.transform.position);
		}
	
	}
}
