using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingObjects : MonoBehaviour {

	public float moveSpeed = 45f;
	public int destroyTimer = 10;

	// Use this for initialization
	void Start () {
		Invoke("DestroySelf", destroyTimer);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += Vector3.back * moveSpeed * Time.deltaTime;
	}

	public void DestroySelf(){
		Destroy(this.gameObject);
	}
}
