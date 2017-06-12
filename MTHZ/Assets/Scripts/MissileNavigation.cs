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
	public GameObject[] enemies;
	//Explosion particle effect played at raycast "collision"
	public GameObject explosion;
	//The selected player/enemy chosen from the list and targetted
	public GameObject enemy;
	//The Layer mask allowing the missiles to only collide with enemies/players
	public LayerMask missileRayMask;

	private bool boomed = false;
	private float missileDamage = 45;

	// Use this for initialization
	void Start () {
		enemy = enemies [Random.Range (1, 4)];
	}
	
	// Update is called once per frame
	void Update () {
		Chase ();
		transform.LookAt(enemy.transform);
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
				transform.position = (Vector3.MoveTowards (transform.position,enemy.transform.position, 0.3f ));
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
		RaycastHit hitBack;
		RaycastHit hitRight;

		if (Physics.Raycast(transform.position, enemy.transform.position, out hit, 100, missileRayMask, QueryTriggerInteraction.Collide)){
			Debug.DrawLine (transform.position, enemy.transform.position);
			if (hit.distance < 2){
				Instantiate (explosion, transform.position, Quaternion.identity);
				hit.collider.SendMessage ("TakeDamage", missileDamage);
				boomed = true;
				TurnOff();
			}
		}

		if (Physics.Raycast(transform.position, transform.forward, out hitBack, 100, missileRayMask, QueryTriggerInteraction.Collide) && boomed == false){
			Debug.DrawLine (transform.position, transform.forward);
			if (hitBack.distance < 2){
				Instantiate (explosion, transform.position, Quaternion.identity);
				boomed = true;
				TurnOff();
			}
		}

		if (Physics.Raycast(transform.position, transform.right, out hitRight, 100, missileRayMask, QueryTriggerInteraction.Collide) && boomed == false){
			Debug.DrawLine (transform.position, transform.forward);
			if (hitBack.distance < 2){
				Instantiate (explosion, transform.position, Quaternion.identity);
				TurnOff();
			}
		}
	}

	public void ShotDown(){
		if (boomed == false){
			Instantiate (explosion, transform.position, Quaternion.identity);
			TurnOff();
		}
	}
}
