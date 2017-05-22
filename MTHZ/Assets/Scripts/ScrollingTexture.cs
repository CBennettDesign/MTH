using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour {

	public float scrollSpeed = 0.01F;
	public Renderer rend;

	void Start() {
		rend = GetComponent<Renderer>();
	}
	void Update() {
		float offset = Time.deltaTime * scrollSpeed;
		rend.material.mainTextureOffset -= new Vector2 (0, offset);
	}
}
