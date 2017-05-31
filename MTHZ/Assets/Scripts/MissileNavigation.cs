using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileNavigation : MonoBehaviour {

	//Whether the ball should move towards its up position
	public bool goUp = true;
	//Gets checked in order to begin missile locking
	public bool startChase = false;
	//The position of the missiles initial jump
	public Transform missileUp;
	//List of potential targets aka players
	public Transform[] enemies;
	//Explosion particle effect played at raycast "collision"
	public GameObject explosion;
	//The selected player/enemy chosen from the list and targetted
	public Transform enemy;
	//The Layer mask allowing the missiles to only collide with enemies/players
	public LayerMask missileRayMask;

	// Use this for initialization
	void Start () {
		enemy = enemies [Random.Range (1, 4)];
	}
	
	// Update is called once per frame
	void Update () {
		Chase ();
		transform.LookAt(enemy);
		BOOM ();

	}

	//---------------------------------------------------------------
	//	Chase()
	// Once Fire is triggered move up until StopUp has been triggered in which target the player and move towards
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	public void Chase(){
		//Invoke ("TurnOff", 30);
		if (startChase){
			if (goUp == false){
				transform.position = (Vector3.MoveTowards (transform.position,enemy.position, 0.3f ));
			}
			else{
				transform.position = (Vector3.MoveTowards(transform.position, missileUp.position, 0.3f));
			}
		}
	}

	//---------------------------------------------------------------
	//	Fire()
	// Start the missile tracking up, then invoke StopUp to set the missile at the target
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	public void Fire(){
		startChase = true;
		Invoke ("StopUp", 3);
	}

	//---------------------------------------------------------------
	//	StopUp()
	// Stops the missiles up movement, making it begin to move towards the target
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	private void StopUp(){
		goUp = false;
	}

	//---------------------------------------------------------------
	//	TurnOff()
	// Set the object to false
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	private void TurnOff(){
		gameObject.SetActive(false);
	}

	//---------------------------------------------------------------
	//	BOOM()
	// If the ray hits a player within 2 units explode and destroy missile
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	private void BOOM(){

		RaycastHit hit;
		if (Physics.Raycast(transform.position, enemy.position, out hit, 100, missileRayMask, QueryTriggerInteraction.Collide)){
			Debug.DrawLine (transform.position, enemy.position);
			if (hit.distance < 2){
				GameObject explo = Instantiate (explosion, transform.position, Quaternion.identity) as GameObject;
				TurnOff();


			}
		}
	}
}
