using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour {

	//The speed at which the texture scrolls
	public float scrollSpeed = 0.01F;
	//The renderer attached to the gameobject
	public Renderer rend;

	// Use this for initialization
	void Start() {
		rend = GetComponent<Renderer>();
	}

	// Update is called once per frame
	void Update() {
		//Scrolls the texture relative to the time passed
		float offset = Time.deltaTime * scrollSpeed;
		rend.material.mainTextureOffset -= new Vector2 (0, offset);
	}
}
