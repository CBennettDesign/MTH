using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armour : MonoBehaviour {

	private int health = 200;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TakeDamage(int damageToTake){
		GetComponentInChildren<ParticleSystem>().Play();
		health = health - damageToTake;
		if (health <= 0){
			Destroy(this.gameObject);
		}
	}
}
