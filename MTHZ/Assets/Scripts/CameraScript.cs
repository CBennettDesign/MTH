using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	//An open Transform variable that defines which object will be faced
	public Transform objectToFace;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Changes the transform of the object the script is attached to so that it faces objectToFace
		transform.LookAt (objectToFace);
	}
}
