using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSpawnerRandomRotation : MonoBehaviour {

	//Slot for a spawnable object to be placed
	public GameObject objectToBeSpawned;
	//The position of the spawn point
	public Transform spawn;
	//The default time between spawns
	public float objectSpawnTimer = 1.5f;
	//Timer variable made to hold time between ticks
	public float timeTimer = 0;
	//The spawned objects rotation
	public Vector3 rotation = Vector3.zero;
	//The parent Transform
	public Transform spawnedObjects;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Run a spawn when the timer ticks
		if (Time.time - timeTimer > objectSpawnTimer) {
			timeTimer = Time.time;
			SpawnObject();
		}		
	}

	//---------------------------------------------------------------
	//	SpawnObject()
	// Spawns an object with a random rotation and sets its parent
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	private void SpawnObject(){
		GameObject GO = Instantiate(objectToBeSpawned,spawn.position,Random.rotation);
		GO.transform.SetParent (spawnedObjects);
	}
}
