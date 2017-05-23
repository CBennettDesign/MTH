using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class Turret : MonoBehaviour {

	public XboxController controller;
	private Vector3 rotationAxis = Vector3.right;
	public float rotationSpeed = 50f;
	public Transform spawnPoint;

	private bool canShoot = true;
	public float timeBetweenShots = 0.4f;
	private int turretDamage = 80;

	public Camera carCamera;
	public GameObject hitMarker;

	public LayerMask whatCanBeShot;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		RayChecking();
		RotatingTurret();
	}

	private void ResetShootBool(){
		
		canShoot = true;
	}

	private void RayChecking(){
		
		RaycastHit hit;
		float distanceOfRay = 100;

		if (Physics.Raycast(spawnPoint.position,transform.forward, out hit, distanceOfRay, whatCanBeShot, QueryTriggerInteraction.Collide)){
			Debug.DrawLine (spawnPoint.position, hit.point);
			hitMarker.transform.position = hit.point;

			if (XCI.GetAxis(XboxAxis.RightTrigger, controller) != 0 && canShoot == true){
				GetComponentInChildren<ParticleSystem>().Play();

				if (hit.transform.tag == "Enemy"){
					//hit.transform.GetComponent<MonsterTruck>().TakeDamage(turretDamage);
					hit.collider.GetComponentInParent<MonsterTruck>().TakeDamage(turretDamage);
				}
				if (hit.transform.tag == "Armour"){
					//Do Damage to Enemy
					hit.transform.GetComponent<Armour>().TakeDamage(turretDamage);
				}

				canShoot = false;
				Invoke("ResetShootBool",timeBetweenShots);

			}
		}
	}

	private void RotatingTurret(){
		
		if (XCI.GetAxis(XboxAxis.RightStickY, controller) != 0){

			transform.Rotate (rotationAxis * Time.deltaTime * XCI.GetAxis(XboxAxis.RightStickY, controller) * rotationSpeed * -1);
		}
	}
}
