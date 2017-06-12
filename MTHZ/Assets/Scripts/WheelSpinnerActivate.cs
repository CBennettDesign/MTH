using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpinnerActivate : MonoBehaviour {

	//The raycasts beginning position
	public Transform rayStart;
	//The pivot on the truck's right side
	public Transform rightPivot;
	//The pivot on the truck's left side
	public Transform leftPivot;
	//The mask that allows only players to be hit
	public LayerMask rayMask;

	//The list of right Saw objects while on
	public GameObject[] rightSawsOn;
	//The list of right Saw objects while off
	public GameObject[] rightSawsOff;

	//The list of left Saw objects while on
	public GameObject[] leftSawsOn;
	//The list of left Saw objects while off
	public GameObject[] leftSawsOff;
	//Whether the saws can be toggled
	public bool canToggleRightSaws = true;
	//Whether the saws can be toggled
	public bool canToggleLeftSaws = true;

	private Vector3 raystartPositionForward;
	private Vector3 raystartPositionBackward;

	private Vector3 raystartPositionLeftForward;
	private Vector3 raystartPositionLeftBackward;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Rays ();
			
	}

	//---------------------------------------------------------------
	//	Rays()
	// Shoots rays to the left and right and switches on and off the appropriate saws as needed
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	private void Rays(){
		
		RaycastHit hitRight;
		raystartPositionForward = new Vector3 (rayStart.position.x, rayStart.position.y, rayStart.position.z + 7);
		raystartPositionBackward = new Vector3 (rayStart.position.x, rayStart.position.y, rayStart.position.z - 7);
		if (Physics.Raycast (raystartPositionForward, rightPivot.position, out hitRight, 100, rayMask, QueryTriggerInteraction.Collide)) {
			
			//Set Saws Active
			if (canToggleRightSaws) {
				Invoke ("RightSawsOn", 0.1f);
				canToggleRightSaws = false;
			}
		}
		if (Physics.Raycast (raystartPositionBackward, rightPivot.position, out hitRight, 100, rayMask, QueryTriggerInteraction.Collide)) {

			//Set Saws Active
			if (canToggleRightSaws) {
				Invoke ("RightSawsOn", 0.1f);
				canToggleRightSaws = false;
			}
		}

		RaycastHit hitLeft;
		raystartPositionLeftForward = new Vector3 (rayStart.position.x, rayStart.position.y, rayStart.position.z + 7);
		raystartPositionLeftBackward = new Vector3 (rayStart.position.x, rayStart.position.y, rayStart.position.z - 7);
		if (Physics.Raycast (raystartPositionLeftForward, leftPivot.position, out hitLeft, 100, rayMask, QueryTriggerInteraction.Collide)) {

			//Set Saws Active
			if (canToggleLeftSaws) {
				Invoke ("LeftSawsOn", 0.1f);
				canToggleLeftSaws = false;
			}
		}
		if (Physics.Raycast (raystartPositionLeftBackward, leftPivot.position, out hitLeft, 100, rayMask, QueryTriggerInteraction.Collide)) {

			//Set Saws Active
			if (canToggleLeftSaws) {
				Invoke ("LeftSawsOn", 0.1f);
				canToggleLeftSaws = false;
			}
		}
	}

	//---------------------------------------------------------------
	//	RightSawsOn()
	// Turns the Right Side Saws into attack mode
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	private void RightSawsOn(){
		for (int y = 0; y < 3; y++) {
			rightSawsOff [y].SetActive (false);
			rightSawsOn [y].SetActive (true);
		}
		Invoke ("RightSawsOff", 2.5f);
	}

	//---------------------------------------------------------------
	//	RightSawsOff()
	// Turns the Right Side Saws into passive mode
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	private void RightSawsOff(){
		for (int y = 0; y < 3; y++) {
			rightSawsOff [y].SetActive (true);
			rightSawsOn [y].SetActive (false);
		}
		canToggleRightSaws = true;
	}

	//---------------------------------------------------------------
	//	LeftSawsOn()
	// Turns the Left Side Saws into attack mode
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	private void LeftSawsOn(){
		for (int i = 0; i < 3; i++) {
			leftSawsOff [i].SetActive (false);
			leftSawsOn [i].SetActive (true);
		}
		Invoke ("LeftSawsOff", 2.5f);
	}

	//---------------------------------------------------------------
	//	LeftSawsOff()
	// Turns the Left Side Saws into passive mode
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	private void LeftSawsOff(){
		for (int y = 0; y < 3; y++) {
			leftSawsOff [y].SetActive (true);
			leftSawsOn [y].SetActive (false);
		}
		canToggleLeftSaws = true;
	}

}
