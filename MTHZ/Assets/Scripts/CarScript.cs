using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class CarScript : MonoBehaviour {

	public XboxController controller;

	public float powerThrust = 5f;
	public float sideThrust = 5f;
	public float rotationSpeed = 50f;
	public float moveSpeed = 50f;

	public Rigidbody rb;
	public Vector3 moveX;
	public Vector3 moveY;
	public Vector3 combinedMove;

	private bool canMoveLeft = true;
	private bool canMoveRight = true;
	private bool canMoveForward = true;
	private bool canMoveBack = true;
	private float rayLength = 3f;
	public LayerMask rayMask;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		Movement ();
		RayChecks ();

	}


	private void Movement(){

		moveX = Vector3.zero;
		if (canMoveRight && XCI.GetAxis (XboxAxis.LeftStickX, controller) > 0) {
			moveX = Vector3.right * XCI.GetAxis (XboxAxis.LeftStickX, controller);
		}

		if (canMoveLeft && XCI.GetAxis (XboxAxis.LeftStickX, controller) < 0) {
			moveX = Vector3.right * XCI.GetAxis (XboxAxis.LeftStickX, controller);
		}
			
		moveY = Vector3.zero;
		if (canMoveForward && XCI.GetAxis (XboxAxis.LeftStickY, controller) > 0){
			moveY = Vector3.forward * XCI.GetAxis (XboxAxis.LeftStickY, controller);
		}

		if (canMoveBack && XCI.GetAxis (XboxAxis.LeftStickY, controller) < 0){
			moveY = Vector3.forward * XCI.GetAxis (XboxAxis.LeftStickY, controller);
		}
			
		combinedMove = moveX + moveY;

		rb.MovePosition (transform.position + combinedMove * moveSpeed * Time.deltaTime);
	}


	private void RayChecks(){

		//RayCheck Left
		RaycastHit hitLeft;

		if (Physics.Raycast(transform.position, Vector3.left, out hitLeft, rayLength,rayMask, QueryTriggerInteraction.Collide )){
			canMoveLeft = false;
			canMoveRight = true;
			canMoveForward = true;
			canMoveBack = true;
		}
		else{
			canMoveLeft = true;
		}
			
		//RayCheck Right
		RaycastHit hitRight;

		if (Physics.Raycast(transform.position, Vector3.right, out hitRight, rayLength,rayMask, QueryTriggerInteraction.Collide)){
			canMoveLeft = true;
			canMoveRight = false;
			canMoveForward = true;
			canMoveBack = true;
		}
		else{
			canMoveRight = true;
		}

		//RayCheck Forward
		RaycastHit hitForward;

		if (Physics.Raycast(transform.position, Vector3.forward, out hitForward, rayLength,rayMask, QueryTriggerInteraction.Collide)){
			canMoveLeft = true;
			canMoveRight = true;
			canMoveForward = false;
			canMoveBack = true;
		}
		else{
			canMoveForward = true;
		}

		//RayCheck Back
		RaycastHit hitBack;

		if (Physics.Raycast(transform.position, Vector3.back, out hitBack, rayLength,rayMask, QueryTriggerInteraction.Collide)){
			canMoveLeft = true;
			canMoveRight = true;
			canMoveForward = true;
			canMoveBack = false;
		}
		else{
			canMoveBack = true;
		}

	}
}
