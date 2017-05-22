using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSpawner : MonoBehaviour {

	public GameObject tree;
	public Transform spawn;
	public float treeSpawnTimer = 1.5f;
	public float treeTimer = 0;
	public Vector3 rotation = Vector3.zero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - treeTimer > treeSpawnTimer) {
			treeTimer = Time.time;
			SpawnTree();
		}		
	}

	private void SpawnTree(){
		Instantiate(tree,spawn.position,Quaternion.Euler(rotation));
	}
}
