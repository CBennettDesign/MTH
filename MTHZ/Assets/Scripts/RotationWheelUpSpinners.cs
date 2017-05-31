using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationWheelUpSpinners : MonoBehaviour {

	//The axis on which to rotate
	private Vector3 rotationAxis = Vector3.forward;
	//The speed at which to rotate
	public float rotationSpeed = 220;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		//Rotate the wheels based on the speed and axis
		transform.Rotate(rotationAxis * Time.deltaTime * rotationSpeed * -1);
	}
}
