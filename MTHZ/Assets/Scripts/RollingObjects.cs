using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingObjects : MonoBehaviour {

	//The default movement speed of the scenery objects
	public float moveSpeed = 45f;
	//The default timer after which objects are destroyed
	public int destroyTimer = 10;

	// Use this for initialization
	void Start () {
		//Begin a countdown to activate DestroySelf
		Invoke("DestroySelf", destroyTimer);
	}
	
	// Update is called once per frame
	void Update () {
		//Moves towards the back of the scene
		transform.position += Vector3.back * moveSpeed * Time.deltaTime;
	}

	//---------------------------------------------------------------
	//	DestroySelf()
	// Destroys this gameobject
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	public void DestroySelf(){
		Destroy(this.gameObject);
	}
}
