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
	private Vector3 rotationAxis = Vector3.up;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (XCI.GetAxis(XboxAxis.LeftStickX, controller) != 0) 
		{
			transform.position += transform.right * XCI.GetAxis(XboxAxis.LeftStickX, controller) * moveSpeed * -1 * Time.deltaTime;
		}

		if (XCI.GetAxis(XboxAxis.LeftStickY, controller) != 0) 
		{
			transform.position += transform.forward * XCI.GetAxis(XboxAxis.LeftStickY, controller) * moveSpeed * -1 * Time.deltaTime;
		}

		if (XCI.GetAxis(XboxAxis.RightStickX, controller) != 0){
			transform.Rotate (rotationAxis * Time.deltaTime * XCI.GetAxis(XboxAxis.RightStickX,controller) * rotationSpeed);
		}

	}
}
