using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class MonsterTruck : MonoBehaviour {
	//UI slider showing the monster's health
	public Slider healthSlider;
	//Value attached to the monster health slider
	public float health = 10000;
	//UI slider showing the monster's armour
	public Slider armourSlider;
	//Value attached to the monster armour slider
	public int armour = 0;
	//UI slider showing the monster's progress
	public Slider progressSlider;
	//Value attached to the monster progress slider
	private float progress = 0;

	//NavMeshAgent that is obtained on start from the gameobject
	private NavMeshAgent navAgent;
	//A List of components that make up the armour on the monster
	public Component[] armourPieces;

	//Amount of time between the truck chooses another movement
	public float timeBetweenMovements = 6f;
	//Keeps track of recent seconds between movements
	private float moveTimer;

	//List of positions in the world that create a movement pattern in a somewhat calm pattern
	public List<Transform> calm;
	////List of positions in the world that create a movement pattern in a somewhat mellow pattern
	public List<Transform> mellow;

	//The position at which the monster	consistently looks at to keep a bearing of forward
	public Transform pivot;
	//The speed at which the truck moves
	private float truckSpeed = 1;
	//Determines what layers the ray will and won't hit, is changed in the inspector
	public LayerMask forwardRayMask;

	//The position of the forward raycast for front weapons
	public Transform forwardRayStart;
	//Gets the data from the flamethrower child
	public Transform flamethrower;
	//Allows the flamethrower to be toggled
	private bool canToggleFlame = true;
	//Is a missile currently being deployed
	private bool missileSent = false;
	//The parent of the damaged particle effects
	public GameObject damaged;


	// Use this for initialization
	void Start () {
		GetArmour();
		navAgent = GetComponent<NavMeshAgent>();
	}

	// Update is called once per frame
	void Update () {
		
		SetSliders();
		GetArmour();
		GetProgress ();
		MoveAround();
		FlameRay ();
		MissileAttack ();
		FlameOn ();
	}

	//---------------------------------------------------------------
	//	MoveAround()
	// The Monster Trucks random Movement is decided based on mood randomisation
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	private void MoveAround(){

		//Check timer hasn't gone off
		if (Time.time - moveTimer > timeBetweenMovements){

			//Get Random Mood and choose random position within that mood
			int i = Random.Range(0,1);
			if (i == 0){
				int r = Random.Range(0,calm.Count);
				navAgent.destination = calm[r].position;
			}
			if (i == 1){
				int t = Random.Range(0,mellow.Count);
				navAgent.destination = mellow[t].position;
			}

			//Change the time between movements while keeping within the Clamp, reset timer
			timeBetweenMovements += Random.Range(-0.5f,0.5f);
			Mathf.Clamp(timeBetweenMovements, 4, 8);
			moveTimer = Time.time;
		}
		//Constantly rotate to face the pivot
		transform.LookAt(pivot);
	}

	//---------------------------------------------------------------
	//	SetSliders()
	// Set the UI sliders to the appropriate variables
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	private void SetSliders(){
		healthSlider.value = health;
		armourSlider.value = armour;
	}

	//---------------------------------------------------------------
	//	GetArmour()
	// Loop through the Components within the Monster Truck with the Armour Class, set variables
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	private void GetArmour(){
		armourPieces = GetComponentsInChildren<Armour>();
		armour = 0;
		//Set Armour to the current Armour components
		foreach (Armour armourVariable in armourPieces){
			armour += 1;
		}
	}

	//---------------------------------------------------------------
	//	GetProgress()
	// Updates the progress UI slider based on the time passed
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	private void GetProgress(){
		progress = Time.time;
		progressSlider.value = progress + truckSpeed;
	}

	//---------------------------------------------------------------
	//	TakeDamage()
	// The Monster Truck's health receives damage minus the armour value
	//
	// Param:
	//		Float damage - The value of damage being sent to the function
	// Return:
	//		Void
	//---------------------------------------------------------------
	public void TakeDamage(float damage){
		if ((damage - armour) > 0){
			health = health - (damage - armour);
		}
	}

	//---------------------------------------------------------------
	//	TakeDamage()
	// Casts a ray forward and if a player is hit toggle's the flamethrower on
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	private void FlameRay(){
		RaycastHit hit;
		if (Physics.Raycast(forwardRayStart.position, pivot.position, out hit, 100, forwardRayMask, QueryTriggerInteraction.Collide)){
			Debug.DrawLine (forwardRayStart.position, pivot.position);
			if (canToggleFlame){
				Invoke ("FlameToggleOn", 0.1f);
				canToggleFlame = false;
			}
		}
	}

	//---------------------------------------------------------------
	//	FlameToggleOn()
	// Turns on the flamethrower object and invokes it to turn off after set amount of seconds
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	private void FlameToggleOn(){
		flamethrower.gameObject.SetActive(true);
		Invoke ("FlameToggleOff", 2.5f);
	}

	//---------------------------------------------------------------
	//	FlameToggleOff()
	// Turns off the flamethrower resets the can toggle
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	private void FlameToggleOff(){
		flamethrower.gameObject.SetActive(false);
		canToggleFlame = true;
	}

	//---------------------------------------------------------------
	//	MissileAttack()
	// Triggers MissilesAway based on a timer as long as missiles aren being deployed
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	private void MissileAttack(){
		if (missileSent == false) {
			Invoke ("MissilesAway", 10);
		}
		missileSent = true;
	}

	//---------------------------------------------------------------
	//	MissilesAway()
	// Informs a random missile to fire and begin its functions
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	private void MissilesAway(){
		GameObject missile = GameObject.FindGameObjectWithTag ("Missile");
		missile.SendMessage ("Fire");
		missileSent = false;
	}

	//---------------------------------------------------------------
	//	FlameOn()
	// Sends a message to the damaged FX to begin playing
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	private void FlameOn(){
		if (health < 9999){
			damaged.SendMessage ("TurnOn");
		}
	}

}
