using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedState : MonoBehaviour {

	//Holds the particle components that visualise a damaged state
	public ParticleSystem[] damagedStates;

	// Use this for initialization
	void Start () {
		damagedStates = GetComponentsInChildren<ParticleSystem> ();
		TurnOff ();
	}
	
	// Update is called once per frame
	void Update () {


	}

	//---------------------------------------------------------------
	//	TurnOn()
	// Turns on the damaged state particles
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	public void TurnOn(){
		for (int i = 0; i < 25; i++) {
			damagedStates [i].gameObject.SetActive (true);
		}
	}

	//---------------------------------------------------------------
	//	TurnOff()
	// Turns off the damaged state particles
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	public void TurnOff(){
		for (int i = 0; i < 25; i++) {
			damagedStates [i].gameObject.SetActive (false);
		}
	}
}
