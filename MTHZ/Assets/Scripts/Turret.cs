using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class Turret : MonoBehaviour {

	//The controller assigned within the inspector
	public XboxController controller;
	//Defined rotationAxis for the turret Right
	private Vector3 rotationAxisRight = Vector3.right;
	//Defined rotationAxis for the turret Up
	private Vector3 rotationAxisUp = Vector3.up;
	//Speed at whcih the turret rotates
	public float rotationSpeed = 50f;

	//The point at which projectiles will spawn
	public Transform spawnPoint;

	//Whether the turret can shoot
	private bool canShoot = true;
	//The time between shots
	public float timeBetweenShots = 0.4f;
	//Damage done by the turret, this is applied to armour, health, etc
	private int turretDamage = 80;
	//The current Weapon selected
	public int weapon = 1;
	//Weapon 1 = Gatling Gun
	//Weapon 2 = Tether
	//Weapon 3 = Grenade Launcher
	//Weapon 4 = Missile Launcher

	//The object to show where a weapon will hit
	public GameObject hitMarker;
	//What objects are available to be shot depending on their layers
	public LayerMask whatCanBeShot;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		WeaponSwitching();
		Shooting();
		RotatingTurret();
	}

	//---------------------------------------------------------------
	//	ResetShootBool()
	// Called to reset the shooting ability of the turret
	//
	// Param:
	//		Vector3 resetPosition - The position to reset the player to
	//		Vector3 resetRotation - The rotation to reset the player to
	//		int playerLives - The amount of lives to reset the player to
	// Return:
	//		Void
	//---------------------------------------------------------------
	private void ResetShootBool(){
		
		canShoot = true;
	}

	//---------------------------------------------------------------
	//	WeaponSwitching()
	// Allows for weapon switching based on controller input 
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	private void WeaponSwitching(){
		
		if ((XCI.GetButtonDown(XboxButton.RightBumper, controller)) == true){
			if (weapon != 4){
				weapon += 1;
			}
			else {
				weapon = 1;
			}
		}
		if ((XCI.GetButtonDown(XboxButton.LeftBumper, controller)) == true) {
			if (weapon != 1){
				weapon -= 1;
			}
			else {
				weapon = 4;
			}
		}
		//Debug.Log(weapon);
	}

	//---------------------------------------------------------------
	//	Shooting()
	// Shoots out a raycast and sets the hitmarker position, then based on bumper input and current weapon applies appropriate damage
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	private void Shooting(){
		
		RaycastHit hit;
		float distanceOfRay = 100;
		if (Physics.Raycast(spawnPoint.position,transform.forward, out hit, distanceOfRay, whatCanBeShot, QueryTriggerInteraction.Collide)){
			Debug.DrawLine (spawnPoint.position, hit.point);
			hitMarker.transform.position = hit.point;

			//Weapon 1 - Gatling Gun
			if (XCI.GetAxis(XboxAxis.RightTrigger, controller) != 0 && canShoot == true && weapon == 1){
				GetComponentInChildren<ParticleSystem>().Play();

				if (hit.transform.tag == "Enemy"){
					hit.collider.GetComponentInParent<MonsterTruck>().TakeDamage(turretDamage);
				}
				if (hit.transform.tag == "Armour"){
					//Do Damage to Enemy
					hit.transform.GetComponent<Armour>().TakeDamage(turretDamage);
				}
				canShoot = false;
				Invoke("ResetShootBool",timeBetweenShots);
			}

			//Weapon 2 - Tether
			if (XCI.GetAxis(XboxAxis.RightTrigger, controller) != 0 && canShoot == true && weapon == 2){
			
			}
			//Weapon 3 - Grenade Launcher
			if (XCI.GetAxis(XboxAxis.RightTrigger, controller) != 0 && canShoot == true && weapon == 3){

			}
			//Weapon 4 - Missile Launcher
			if (XCI.GetAxis(XboxAxis.RightTrigger, controller) != 0 && canShoot == true && weapon == 4){

			}
		}
	}

	//---------------------------------------------------------------
	//	RotatingTurret()
	// Rotates the turret based on right joystick input
	//
	// Param:
	//		Void
	// Return:
	//		Void
	//---------------------------------------------------------------
	private void RotatingTurret(){
		
		if (XCI.GetAxis(XboxAxis.RightStickY, controller) != 0){

			transform.Rotate (rotationAxisRight * Time.deltaTime * XCI.GetAxis(XboxAxis.RightStickY, controller) * rotationSpeed * -1);
		}

		if (XCI.GetAxis(XboxAxis.RightStickX, controller) != 0){

			transform.Rotate (rotationAxisUp * Time.deltaTime * XCI.GetAxis(XboxAxis.RightStickX, controller) * rotationSpeed);
		}
	}
}
