using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationWheels : MonoBehaviour {

	//The axis on which to rotate
	private Vector3 rotationAxis = Vector3.right;
	//The speed at which to rotate
	private float rotationSpeed = 220;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Rotate the wheels based on the speed and axis
		transform.Rotate(rotationAxis * Time.deltaTime * rotationSpeed * -1);
	}
}
