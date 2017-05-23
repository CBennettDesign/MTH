using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class MonsterTruck : MonoBehaviour {
	public Slider healthSlider;
	public float health = 10000;
	public Slider armourSlider;
	public int armour = 0;
	public Slider progressSlider;
	private float progress = 0;

	private NavMeshAgent navAgent;
	public Component[] armourPieces;

	public float timeBetweenMovements = 6f;
	private float moveTimer;

	public List<Transform> calm;
	public List<Transform> mellow;

	public Transform pivot;

	void Start () {
		GetArmour();
		navAgent = GetComponent<NavMeshAgent>();
	}

	void Update () {
		
		SetSliders();
		GetArmour();
		GetProgress ();
		MoveAround();
	}

	private void MoveAround(){

		if (Time.time - moveTimer > timeBetweenMovements){
			
			int i = Random.Range(0,1);

			if (i == 0){
				Debug.Log("Calm");
				int r = Random.Range(0,calm.Count);
				navAgent.destination = calm[r].position;
			}

			if (i == 1){
				Debug.Log("Mellow");
				int t = Random.Range(0,mellow.Count);
				navAgent.destination = mellow[t].position;
			}

			timeBetweenMovements += Random.Range(-0.5f,0.5f);
			Mathf.Clamp(timeBetweenMovements, 4, 8);
			moveTimer = Time.time;
		}
		transform.LookAt(pivot);
	}

	private void SetSliders(){
		healthSlider.value = health;
		armourSlider.value = armour;
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
		if ((damage - armour) > 0){
			health = health - (damage - armour);
		}
	}
}
