using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterTruck : MonoBehaviour {
	public Slider healthSlider;
	public float health = 10000;
	public Slider armourSlider;
	public int armour = 0;
	public Slider progressSlider;
	private float progress = 0;

	public Component[] armourPieces;

	void Start () {
		GetArmour();

	}

	void Update () {
		GetArmour();
		healthSlider.value = health;
		armourSlider.value = armour;
		GetProgress ();
		Debug.Log(health);
	}

	private void GetArmour(){
		armourPieces = GetComponentsInChildren<ParticleSystem>();
		armour = 0;
		foreach (ParticleSystem Particle in armourPieces){
			armour += 1;
		}
	}

	private void GetProgress(){
		progress = Time.time;
		progressSlider.value = progress;
	}

	public void TakeDamage(float damage){
		Debug.Log("WE MADE IT");
		if ((damage - armour) > 0){
			health = health - (damage - armour);
			Debug.Log("WE MADE IT DEEP");
		}
	}
}
