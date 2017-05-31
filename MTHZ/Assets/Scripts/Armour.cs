using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armour : MonoBehaviour {

	//health indicates the health value of an armour piece
	private int health = 160;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//---------------------------------------------------------------
	//	TakeDamage()
	// Called when the armour piece is shot and the player sends the armour a message
	// Destroys Armour piece if health is below 0
	//
	// Param:
	//		Int damageToTake - The damage that is being done to the armour
	// Return:
	//		Void
	//---------------------------------------------------------------
	public void TakeDamage(int damageToTake){
		GetComponentInChildren<ParticleSystem>().Play();
		health = health - damageToTake;
		if (health <= 0){
			Destroy(this.gameObject);
		}
	}
}
