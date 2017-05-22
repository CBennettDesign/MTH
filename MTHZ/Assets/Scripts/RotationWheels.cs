using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationWheels : MonoBehaviour {

	private Vector3 rotationAxis = Vector3.right;
	private float rotationSpeed = 220;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(rotationAxis * Time.deltaTime * rotationSpeed * -1);
	}
}
