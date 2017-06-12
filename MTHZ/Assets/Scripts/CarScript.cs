using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using UnityEngine.UI;

public class CarScript : MonoBehaviour {
	//The controller assigned within the inspector
	public XboxController controller;

	//The movement speed of the player
	public float moveSpeed = 50f;

	//A rigidbody that gets the cars rigidbody on start and stores it for later use in movement
	public Rigidbody rb;
	//A slot to store the X movement 
	public Vector3 moveX;
	//A slot to store the Y movement
	public Vector3 moveY;
	//The combined values of X and Y resulting in a final movement direction
	public Vector3 combinedMove;

	//Indicates whether the car can move in any of the said directions based on the raycast feedback
	private bool canMoveLeft = true;
	private bool canMoveRight = true;
	private bool canMoveForward = true;
	private bool canMoveBack = true;
	//Determines the length of the raycast
	private float rayLength = 3f;
	//Determines what layers the ray will and won't hit, is changed in the inspector
	public LayerMask rayMask;

	public Slider healthSlider;
	public float health = 100;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		Movement ();
		RayChecks ();
		UpdateHUD();
		if (health < 1){
			gameObject.SetActive (false);
		}

	}


	//---------------------------------------------------------------
	//	Movement()
	// Called every Update in order to position the car/player
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	private void Movement(){
		
		//Set the current MoveX to 0,0,0
		moveX = Vector3.zero;

		//Runs through the controller input in each direction and confirms that the player is able to move in that direction, then adds the value to the accumulating (MoveX & MoveY)
		if (canMoveRight && XCI.GetAxis (XboxAxis.LeftStickX, controller) > 0) {
			moveX = transform.right * XCI.GetAxis (XboxAxis.LeftStickX, controller);
		}

		if (canMoveLeft && XCI.GetAxis (XboxAxis.LeftStickX, controller) < 0) {
			moveX = transform.right * XCI.GetAxis (XboxAxis.LeftStickX, controller);
		}
			
		moveY = Vector3.zero;
		if (canMoveForward && XCI.GetAxis (XboxAxis.LeftStickY, controller) > 0){
			moveY = transform.forward * XCI.GetAxis (XboxAxis.LeftStickY, controller);
		}

		if (canMoveBack && XCI.GetAxis (XboxAxis.LeftStickY, controller) < 0){
			moveY = transform.forward * XCI.GetAxis (XboxAxis.LeftStickY, controller);
		}

		//Combines the Vector3 Values in order to get a final direction
		combinedMove = moveX + moveY;
		//Moves the rigidbody the script is attached to in the CombinedMove direction, then times it by the players speed
		rb.MovePosition (transform.position + combinedMove * moveSpeed * Time.deltaTime);
	}

	//---------------------------------------------------------------
	//	RayChecks()
	// Constantly called in Update to keep track of the raycasts keeping the players collisions in check
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	private void RayChecks(){

		//Raycasts relative to the Player's left and sets the appropriate values allowing or disabling movement
		RaycastHit hitLeft;
		if (Physics.Raycast(transform.position, transform.right * -1, out hitLeft, rayLength,rayMask, QueryTriggerInteraction.Collide )){
			Debug.DrawRay (transform.position, hitLeft.point);
			canMoveLeft = false;
			canMoveRight = true;
			canMoveForward = true;
			canMoveBack = true;
		}
		else{
			canMoveLeft = true;
		}
			
		//Raycasts relative to the Player's right and sets the appropriate values allowing or disabling movement
		RaycastHit hitRight;
		if (Physics.Raycast(transform.position, transform.right, out hitRight, rayLength,rayMask, QueryTriggerInteraction.Collide)){
			canMoveLeft = true;
			canMoveRight = false;
			canMoveForward = true;
			canMoveBack = true;
		}
		else{
			canMoveRight = true;
		}

		//Raycasts relative to the Player's forward and sets the appropriate values allowing or disabling movement
		RaycastHit hitForward;
		if (Physics.Raycast(transform.position, transform.forward, out hitForward, rayLength,rayMask, QueryTriggerInteraction.Collide)){
			canMoveLeft = true;
			canMoveRight = true;
			canMoveForward = false;
			canMoveBack = true;
		}
		else{
			canMoveForward = true;
		}

		//Raycasts relative to the Player's b and sets the appropriate values allowing or disabling movement
		RaycastHit hitBack;
		if (Physics.Raycast(transform.position, transform.forward * -1, out hitBack, rayLength,rayMask, QueryTriggerInteraction.Collide)){
			canMoveLeft = true;
			canMoveRight = true;
			canMoveForward = true;
			canMoveBack = false;
		}
		else{
			canMoveBack = true;
		}
			
	}

	private void UpdateHUD(){
		healthSlider.value = health;
	}

	public void TakeDamage(float damage){
		health -= damage;
	}
}
